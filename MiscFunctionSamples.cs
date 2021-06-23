//Sample code
namespace SkillSurf.DevTools {
    //Extension methods must be defined in a static class
    public static class StringExtension {
        
        //    Illegal way to prevent strings from allocating garbage when they're changed:
        //Sets a char within string without allocating any garbage (aka without destroying and recreating the string)
        public static unsafe void SetChar(this string s, int index, char ch) {
            if (index < 0 || index >= s.Length) { System.Console.WriteLine("Error: Out of bounds exception"); return; }
            fixed(System.Char *p = s) { p[index] = ch; }
        }
        
        //    Illegal way to prevent strings from allocating garbage when they're changed:
        //Sets multiple chars within string without allocating any garbage (aka without destroying and recreating the string)
        public static unsafe void SetChars(this string s, ref string chars, int startIndex = 0, int endIndex = 0) {
            if (endIndex == 0) { endIndex = chars.Length - 1 - startIndex;}
            for (int index = startIndex; index < endIndex; index++) {
                if (index < 0 || index >= s.Length) { System.Console.WriteLine("Error: Out of bounds exception"); return; }
                fixed(System.Char *p = s) { p[index] = chars[index]; }
            }
        }
    }
}

//Sample code
namespace SkillSurf.UI {
    public class InGameUI : MonoBehaviour {
        //Updates the best time to the given new value in milliseconds in the class data and player preferences, then updates the string value for display
        public void ChangeBestTimeMs(short newTime_s, short newTime_ms) {
            //Set ints in obj data
            this.plyrPrefs_bestLvlTime_s  = newTime_s;
            this.plyrPrefs_bestLvlTime_ms = newTime_ms;
                        
            //Set string
            this.BestLvlTime_str.SetChar(0, IntStrings_Triple0[newTime_s][0]);
            this.BestLvlTime_str.SetChar(1, IntStrings_Triple0[newTime_s][1]);
            this.BestLvlTime_str.SetChar(2, IntStrings_Triple0[newTime_s][2]);
            this.BestLvlTime_str.SetChar(3, '.');
            this.BestLvlTime_str.SetChar(4, IntStrings_Triple0[newTime_ms][0]);
            this.BestLvlTime_str.SetChar(5, IntStrings_Triple0[newTime_ms][1]);
            this.BestLvlTime_str.SetChar(6, IntStrings_Triple0[newTime_ms][2]);
                        
            //Set player prefs data
            PlayerPrefs.SetInt(PlyrPrefsKey_NewBestLvlTime_s,  newTime_s);
            PlayerPrefs.SetInt(PlyrPrefsKey_NewBestLvlTime_ms, newTime_ms);
            }
        }

        //Updated when stage ID is updated, called by stats system
        public void UpdateBestStageTime(ref System.String timeStr, bool forceDirty = false) {
            this.BestStageTime_Text_TimeStr.text = timeStr;
            if (!forceDirty) { return; }
            this.BestStageTime_Text_TimeStr.SetVerticesDirty();
            this.BestStageTime_Text_TimeStr.cachedTextGenerator.Invalidate();
            this.BestStageTime_Text_TimeStr.cachedTextGenerator.Populate(timeStr, this._textGenSettings_stg);
            UnityEngine.UI.LayoutRebuilder.MarkLayoutForRebuild(this.BestStageTime_Text_TimeStr.rectTransform);
        }
    }
}

[System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
public static void InterpretInputValues(ref Vector3 playerVelocity) {

    //Grab properties and cache them for quicker access
    Vector3 lastPlaneNormalVec = PlayerCollisionHandler.LastPlaneNormal;
    float moveAcceleration = PlayerVelocityHandler.MoveAcceleration;
    bool autoRampStick = PlayerInputHandler.EnableAutoRampStick;
    bool autoTurnInAir = PlayerInputHandler.EnableAutoTurnInAir;
    bool mouseEnabled = PlayerInputHandler.MouseEnabled;
    float gyroSens_x = PlayerInputHandler.MobileSens_X;
    float gyroSens_y = PlayerInputHandler.MobileSens_Y;
    float autoTurnSensOnRamp = PlayerInputHandler.AutoTurnSensOnRamp;

    //Reset input values 
    bool inputLeft = false,
        inputRight = false;

    //Reset world-state values
    bool onRamp, onGround,
        onLeftSideOfRamp = false,
        onRightSideOfRamp = false;

    //Set ground/ramp world state values
    onGround           = lastPlaneNormalVec.y > 0.99f;
    onRamp             = lastPlaneNormalVec.y < 0.7f  && lastPlaneNormalVec.y > 0f;

    //If auto-sticking enabled we need to set the other world-state values
    if (autoRampStick) {
        //Convert velocity & plane normal to local values (local to the player view transform orientation not player transform)
        Vector3 localVelocity = CameraTransform.InverseTransformVector(playerVelocity);
        Vector3 localPlaneNormal = CameraTransform.InverseTransformVector(lastPlaneNormalVec);
    
        //Get forwards/backwards values
        bool goingForwards = localVelocity.z > 0f; bool goingBackwards = localVelocity.z < 0f;

        //Use forwards/backwards values to set left or right side of ramp values
        if (goingForwards) {
            onLeftSideOfRamp   =  localPlaneNormal.x < 0f    && onRamp;
            onRightSideOfRamp  =  localPlaneNormal.x > 0f    && onRamp;
        } else if (goingBackwards) {
            onLeftSideOfRamp   =  localPlaneNormal.x > 0f    && onRamp;
            onRightSideOfRamp  =  localPlaneNormal.x < 0f    && onRamp;
        }
    }

    //If auto-turning enabled we need to set a turnLeftRight value
    float turnLeftRight = 0f;

    //Auto-turn auto-presses Left/Right movement keys when turning the camera via gyro/mouse
    if (autoTurnInAir) {
        //Gyro processed before touch so that it is less important if both are enabled
        if (PlayerInputHandler.GyroEnabled) {
            //Hand-tuned values for auto-turn AI with Gyroscope
            turnLeftRight = 
                Input.gyro.rotationRateUnbiased.y * 2 * gyroSens_x +
                -Input.gyro.rotationRateUnbiased.z * 8 * gyroSens_y;
        } else if (PlayerInputHandler.TouchEnabled) {
            //Touch processing
            turnLeftRight += PlayerInputHandler.LastTouchDeltaX;
        }

        //Mouse
        if (mouseEnabled) { turnLeftRight = Input.GetAxis("Mouse X"); };

    }

    /* Left / Right input key AI */

    //If on either side of ramp
    if (onRamp) {
        //Check MouseMovement with Ramp AutoTurn Sensitivity to allow for a Mouse only flick off a ramp
        if (turnLeftRight > autoTurnSensOnRamp) {
            inputRight = true;
        } else if (turnLeftRight < -autoTurnSensOnRamp) {
            inputLeft = true;
        } else {
            //Mouse hasnt overcome autoTurnSensOnRamp value, meaning user is not flicking off
            //So lets check which side of the ramp they are on and press the key to stick them to it
            if (onLeftSideOfRamp) {
                inputLeft = false;  inputRight = true;
            } else if (onRightSideOfRamp) {
                inputLeft = true;   inputRight = false; 
            }
        }
    
        //Else: Not on ramp or on ground
    } else if (!onGround) {
        //Check Mousemovement with Air AutoTurn Sensitivity
        //autoTurnSensInAir is 0, VERY easy to activate auto-turn with slight movement
        //this pretty much enables perfect air strafing with just the mouse!
        if (turnLeftRight > 0f) {
            inputRight = true;
        } else if (turnLeftRight < 0f) {
            inputLeft = true;
        };
    }
        
    /* Prepare move data for processing*/
        
    //Left/Right movement keys
    if (!inputLeft && !inputRight) {
        MoveSideways = 0;
    } else if (inputLeft) {
        MoveSideways = -moveAcceleration;
    } else if (inputRight) {
        MoveSideways =  moveAcceleration;
    }
}