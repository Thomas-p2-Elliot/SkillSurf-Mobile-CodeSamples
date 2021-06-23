/*
 *     Code by Thomas Elliott Golbourn - Thomas.Golbourn@Gmail.com - For SkillSurf & University Dissertation
 */

/*===============================================================================================*/
/*  Replay Data Structures                                                                       */
/*    This is kept Outside of Any NameSpaces to Avoid DeSerialization Issues on Version Change   */
/*    But also kept here as this is where it is referenced most-often                            */
/*===============================================================================================*/

//Normal vec3 isn't serializable, this class implicitly converts when used in place of vec3 or with it

using SkillSurf.DevTools;

[System.Serializable] public struct Vec3_Serializable {

    //Conversion   FROM   Vec3_Serializable    TO   UnityEngine.Vector3
    public static implicit operator UnityEngine.Vector3(Vec3_Serializable serializableVector3) {
        serializableVector3._UnityVec3.x = serializableVector3.x; serializableVector3._UnityVec3.y = serializableVector3.y;
        serializableVector3._UnityVec3.z = serializableVector3.z; 
        return serializableVector3._UnityVec3;
    }

    //Conversion   FROM   UnityEngine.Vector3    TO   Vec3_Serializable
    public static implicit operator Vec3_Serializable(UnityEngine.Vector3 inputVec3) {
        return new Vec3_Serializable() {x = inputVec3.x, y = inputVec3.y, z = inputVec3.z};
    }
    
    //Conversion from Vector3 to Vec3 Serializable without making a new Vec3 Serializable
    public void CopyFromUnityVec3(UnityEngine.Vector3 inputVec3) {
        this._UnityVec3.x = inputVec3.x; this._UnityVec3.y = inputVec3.y; this._UnityVec3.z = inputVec3.z; 
        this.x = inputVec3.x; this.y = inputVec3.y; this.z = inputVec3.z; 
    }
    
    //Conversion   FROM   Vec3_Serializable    TO   UnityEngine.Vector3
    public UnityEngine.Vector3 ToUnityVec3 () {
        this._UnityVec3.x = this.x; this._UnityVec3.y = this.y; this._UnityVec3.z = this.z; 
        return this._UnityVec3;
    }

    //Used for implicit conversion
    private UnityEngine.Vector3 _UnityVec3;

    //X Y & Z Data, Simple floats
    public System.Single x, y, z;

    //Default Constructor
    public Vec3_Serializable(System.Single x = 0f, System.Single y = 0f, System.Single z = 0f) {
        this._UnityVec3.x = this.x = x; 
        this._UnityVec3.y = this.y = y;
        this._UnityVec3.z = this.z = z;
    }

    //Indexer allows us to access XYZ via [0],[1],[2], anything other than 0, 1 or 2 will produce MaxValue as error
    public System.Single this[System.Int32 i] {
        get {
            switch (i) {
                case 0:  return this.x;
                case 1:  return this.y;
                case 2:  return this.z;
                default: return System.Single.MaxValue;
            }
        } 
        set {
            switch (i) {
                case 0:  this.x = value; return;
                case 1:  this.y = value; return;
                case 2:  this.z = value; return;
                default: return;
            }
        } 
    }
}

//~44 bytes per frame, 100,000 frames = 27min @ 60fps & Only 4.4mb
[System.Serializable] public struct ReplayFrame {
    public Vec3_Serializable position, velocity, direction;
    public System.Single frameLength;
    
    //Copy To operation
    public void CopyTo(ref ReplayFrame frameToCopyDataInto) {
        for (System.Int32 i = 0; i < 3; i++) {
            frameToCopyDataInto.direction[i] = this.direction[i];
            frameToCopyDataInto.velocity[i] = this.velocity[i];
            frameToCopyDataInto.position[i] = this.position[i];
            frameToCopyDataInto.frameLength = this.frameLength;
        }
    }
}

[System.Serializable] public struct TimeBitVec32 {
	private System.Collections.Specialized.BitVector32 bv;
	public System.Collections.Specialized.BitVector32 GetBitVec() { return bv; }
	private static readonly System.Collections.Specialized.BitVector32.Section secs1 = System.Collections.Specialized.BitVector32.CreateSection(9);
	private static readonly System.Collections.Specialized.BitVector32.Section secs2 = System.Collections.Specialized.BitVector32.CreateSection(9, secs1);
	private static readonly System.Collections.Specialized.BitVector32.Section secs3 = System.Collections.Specialized.BitVector32.CreateSection(9, secs2);
	private static readonly System.Collections.Specialized.BitVector32.Section gap   = System.Collections.Specialized.BitVector32.CreateSection(9, secs3);
	private static readonly System.Collections.Specialized.BitVector32.Section ms1   = System.Collections.Specialized.BitVector32.CreateSection(9, gap);
	private static readonly System.Collections.Specialized.BitVector32.Section ms2   = System.Collections.Specialized.BitVector32.CreateSection(9, ms1);
	private static readonly System.Collections.Specialized.BitVector32.Section ms3   = System.Collections.Specialized.BitVector32.CreateSection(9, ms2);
	private static readonly System.Collections.Specialized.BitVector32.Section end   = System.Collections.Specialized.BitVector32.CreateSection(9, ms3);
	
	public TimeBitVec32(ref short secs, ref short msecs) {
		bv = new System.Collections.Specialized.BitVector32(0);
		SetSecs(ref secs);
		SetMs(ref msecs);
	}

	public string GetStr() {
		string str = "000.000";
		str.SetChar(0, (bv[secs1] * 100).ToString()[0]);
		str.SetChar(1, (bv[secs2] * 10).ToString()[0]);
		str.SetChar(2, (bv[secs3]).ToString()[0]);
		str.SetChar(3, '.');
		str.SetChar(4, (bv[ms1] * 100).ToString()[0]);
		str.SetChar(5, (bv[ms2] * 10).ToString()[0]);
		str.SetChar(6, (bv[ms3]).ToString()[0]);

		return str;
	}
	
	public void GetStr(ref string str) {
		str.SetChar(0, (bv[secs1] * 100).ToString()[0]);
		str.SetChar(1, (bv[secs2] * 10).ToString()[0]);
		str.SetChar(2, (bv[secs3]).ToString()[0]);
		str.SetChar(3, '.');
		str.SetChar(4, (bv[ms1] * 100).ToString()[0]);
		str.SetChar(5, (bv[ms2] * 10).ToString()[0]);
		str.SetChar(6, (bv[ms3]).ToString()[0]);
	}


	public void SetSecs(ref short secs) {
		bv[secs1] = (short) secs / 100;		 //If < 100 it will be set to 0
		bv[secs2] = (short) secs / 10 % 10;  //If < 10 it will be 0
		bv[secs3] = (short) secs % 10; 		 //If < 10 will return self
	}
	
	public void SetSecs(short secs) {
		bv[secs1] = (short) secs / 100;		 //If < 100 it will be set to 0
		bv[secs2] = (short) secs / 10 % 10;  //If < 10 it will be 0
		bv[secs3] = (short) secs % 10; 		 //If < 10 will return self
	}
	
	public void GetSecs(ref short secs) {
		secs = (short) (bv[secs1] * 100); 			//hundreds
		secs = (short) (secs + (bv[secs2] * 10)); 	//tens
		secs = (short) (secs + bv[secs3]); 			//ones??
	}
	
	public short GetSecs() {
		short secs = 0;
		secs = (short) (bv[secs1] * 100); 			//hundreds
		secs = (short) (secs + (bv[secs2] * 10)); 	//tens
		secs = (short) (secs + bv[secs3]); 			//ones??
		return secs;
	}

	public void SetMs(ref short ms) {
		bv[gap] = -1; bv[end] = -1; 	//Sets gap & end to all 1's
		bv[ms1] = (short) ms / 100;	 	//If < 100 it will be set to 0
		bv[ms2] = (short) ms / 10 % 10; //If < 10 it will be 0
		bv[ms3] = (short) ms % 10; 		//If < 10 will return self
	}
	
	public void SetMs(short ms) {
		bv[gap] = -1; bv[end] = -1; 	//Sets gap & end to all 1's
		bv[ms1] = (short) ms / 100;	 	//If < 100 it will be set to 0
		bv[ms2] = (short) ms / 10 % 10; //If < 10 it will be 0
		bv[ms3] = (short) ms % 10; 		//If < 10 will return self
	}
	
	public void GetMs(ref short ms) {
		ms = (short) (bv[ms1] * 100);
		ms = (short) (ms + (bv[ms2] * 10));
		ms = (short) (ms + bv[ms3]);
	}
	
	public short GetMs() {
		short ms = 0;
		ms = (short) (bv[ms1] * 100);
		ms = (short) (ms + (bv[ms2] * 10));
		ms = (short) (ms + bv[ms3]);
		return ms;
	}
}

public struct DefaultReplayData {
	public static readonly System.Char[] DefaultLevelName  = new char[] {'N','o','L','e','v','e','l'};
	public static readonly System.Char[] DefaultUserName   = new char[] {'N','o','N','a','m','e'};
	public static readonly System.Char[] DefaultDateString = new char[] {'3','1','/','0','2','/','2','0'};
}

[System.Serializable] public struct ReplayData {
    //The replay data itself
    public System.String LevelName; // = "NoLevel";
    public ReplayFrame[] ReplayFrames; //= new ReplayFrame[SkillSurf.Replays.ReplaySystem.MaxRecordingLength];
    public System.Int32 FrameCount; //= 0;
    public TimeBitVec32 LevelCompletionTimeBv32; //= new TimeBitVec32();
    public System.String UserName; //= SkillSurf.SettingsManagement.GameSettings.DefaultPlayerNameSetting;
    public System.String DateString; //= "31/02/20";

    //Constructor
    public void CreateNew() {
	    FrameCount = 0;
	    ReplayFrames = new ReplayFrame[SkillSurf.Replays.ReplaySystem.MaxRecordingLength]; ReplayFrames.Initialize(); 
	    LevelCompletionTimeBv32 = new TimeBitVec32();
	    LevelCompletionTimeBv32.SetSecs(0); LevelCompletionTimeBv32.SetMs(0);
	    LevelName = new System.String(DefaultReplayData.DefaultLevelName);
	    UserName = new System.String(DefaultReplayData.DefaultUserName);
	    DateString = new System.String(DefaultReplayData.DefaultDateString);
    }
    
    public void EmptyData() {
        //Cant empty ReplayFrames really...just set framecount to 0 so only frames that have data are included in file
        FrameCount = 0; ReplayFrames.Initialize();
        LevelCompletionTimeBv32.SetSecs(0); LevelCompletionTimeBv32.SetMs(0);
        UserName = "NoName"; LevelName = "NoLevel"; DateString = "31/02/20";
    }
}

