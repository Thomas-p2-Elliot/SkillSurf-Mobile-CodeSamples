/*
 *     Code by Thomas Elliott Golbourn - Thomas.Golbourn@Gmail.com - For SkillSurf & University Dissertation
 */

namespace SkillSurf.Player {
    using UnityEngine;

    public struct PlayerVelocityHandler {
        public static readonly bool     ClampGroundSpeed = true;
        public static readonly float    MaxSpeed = 7f;
        public static readonly float    AirAcceleration = 150f; //was 50,000, now 150, dec2020
        public static readonly float    AirSpeedCap = 0.5f; //0.8f -> 0.5f Dec 14th 2020
        public static readonly float    MoveAcceleration = 7f;

        public struct MoveData {
            public float MoveForwards, MoveSideways;
            public Vector3 ViewTransformForwardVec, ViewTransformRightVec, CurrentVelocity, CurPos;
            public bool OnGround;
        }
        
        [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void GetMovementAcceleration(ref MoveData moveData, ref float dTime, ref bool isFixedUpdate) {
            //this.PlayerTransform.position = this.PlayerPos;         //Update players actual position
            //Apply gravity if not on ground for previous update
            if (PlayerCollisionHandler.OnGround == false) { moveData.CurrentVelocity.y -= (PlayerController.Gravity * dTime); }
            //If fixed update do ground trace Trace towards the ground to see if we are grounded or in the air...
            //...otherwise we use previous result again without gravity though
            if (isFixedUpdate) { PlayerCollisionHandler.PlayerGroundTrace(moveData.CurPos, moveData.CurrentVelocity); }
            //Create move data to pass to velocity handler
            
            /* Calculate Intended Direction & Speed */
            Vector3 wishVel = Vector3.zero, wishDir;
            float wishSpeed = 0f;
            
            //Create forward & right vector to know which way player is facing (removing any Y component, can't fly up!)
            Vector3 forward = moveData.ViewTransformForwardVec, right = moveData.ViewTransformRightVec;
            forward.y = 0; right.y = 0; 
            //old: //forward.Normalize(); right.Normalize(); //new:
            forward /= Mathf.Sqrt(forward.x * forward.x + forward.y * forward.y +  forward.z *  forward.z);
            right /= Mathf.Sqrt(right.x * right.x + right.y * right.y +  right.z *  right.z);
            //For X & Z Axis create the intended direction via left & right movement keys
            wishVel.x = forward.x * moveData.MoveForwards + right.x * moveData.MoveSideways;
            wishVel.z = forward.z * moveData.MoveForwards + right.z * moveData.MoveSideways;

            //Get speed from magnitude then normalize to get wish dir //was: wishSpeed = wishVel.magnitude;
            wishSpeed = Mathf.Sqrt( wishVel.x * wishVel.x + wishVel.y *  wishVel.y +  wishVel.z * wishVel.z);
            //Was: wishDir = wishVel.normalized, replaced with fast normalizer below using calc from above
            wishDir = wishVel / wishSpeed;

            //Clamp Wish Speed & Velocity if enabled
            if ((wishSpeed > 0.01f || wishSpeed < 0.01f) && (wishSpeed > PlayerVelocityHandler.MaxSpeed) && PlayerVelocityHandler.ClampGroundSpeed) {
                //wishVel     = wishVel * (PlayerVelocityHandler.MaxSpeed / wishSpeed); (Dec 2020)
                wishSpeed   = PlayerVelocityHandler.MaxSpeed;
            }
            
            // Cap wish speed if in air
            if (!moveData.OnGround) {
                //Dec 2020, was mathf.min before
                if (PlayerVelocityHandler.AirSpeedCap < wishSpeed) { wishSpeed = PlayerVelocityHandler.AirSpeedCap; }
            }

            //Calculate acceleration reduction based on deviation from straight-forwards
            //float deviationAmnt = Vector3.Dot(moveData.CurrentVelocity, wishDir);
            float deviationAmnt = (moveData.CurrentVelocity.x * wishDir.x + moveData.CurrentVelocity.y * wishDir.y + moveData.CurrentVelocity.z * wishDir.z);
            
            //Get acceleration amount in wish speed reduction from the amount of deviation
            float accelAmnt = wishSpeed - deviationAmnt;

            //If acceleration > 0
            if (accelAmnt > 0) {
                
                // Determine speed after acceleration
                float newSpeed = 0f;
                if (!moveData.OnGround) {
                    newSpeed = PlayerVelocityHandler.AirAcceleration * wishSpeed * dTime;
                } else {
                    //1f instead of deltaTime = Stand in for surface friction
                    newSpeed = PlayerVelocityHandler.MoveAcceleration * wishSpeed; // * 1f;
                }
                
                //Cap the new speed to the lowest of the two values
                if (newSpeed < accelAmnt) { newSpeed = accelAmnt; } //dec2020
                //newSpeed = Mathf.Min(newSpeed, accelAmnt); //old
                
                //Create amount of velocity to add value with the newspeed and direction
                for (int i = 0; i < 3; i++) { moveData.CurrentVelocity[i] += newSpeed * wishDir[i]; }
            };
        }
    }
}
