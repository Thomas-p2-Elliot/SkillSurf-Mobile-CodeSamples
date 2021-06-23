
//============== Occlusion Zone Object Queue System ================
// This system controls object enabling/disabling by getting object lists from the
// occlusion system and spreading out their enabling/disabling over multiple frames
// Obj Queues needed: SpriteRenderers, MeshRenderers, ParticleSystemRenderers, Renderers, FogVolumes
// Each queue needs a bool array for render states & a bool for needsProcessing
// System needs an overall bool for procesingNeeded
// When processingNeeded is set, check needsProcessing on each queue
// and process 1 of the objects in the queue, then remove it from the queue
// To process an obj we run a specific function for that queue type

//Queues are singly linked lists with their type values
//public QueueManager PlyrQueueManager_Enable = new QueueManager();
//public QueueManager PlyrQueueManager_Disable = new QueueManager();

//============== Occlusion Zone Trigger System ================
private static int OcclusionZoneCount = 0;
private static readonly int MaxOcclusionTriggers = 63;
private static readonly string OcclusionZoneTagStr = "OcclusionZone";
private static OcclusionZone[] OcclusionZones = new OcclusionZone[PlayerController.MaxOcclusionTriggers];
private static OcclusionZone CurrentZone;
private static CustomBounds CurrentZoneBoxColBounds;
private static int CurrentOczId = 0;
private static bool[] ZoneStates = new bool[PlayerController.MaxOcclusionTriggers];

[System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
private void GetOcclusionZones() {
    //Reset count
    PlayerController.OcclusionZoneCount = 0;
    
    //Reset list
    for (int i = 0; i < PlayerController.MaxOcclusionTriggers; i++) { ZoneStates[i] = false; }

    //Loop through all touchable floor objects in scene
    // this returns a new array every time, so no need to create one
    GameObject[] objArr = GameObject.FindGameObjectsWithTag(PlayerController.OcclusionZoneTagStr);

    //Store count
    PlayerController.OcclusionZoneCount = objArr.Length;

    //For each with iterator value
    int listId = 0; foreach (GameObject occlusionObj in objArr) {
    
        //If list overlfows exit with warning
        if (listId >= PlayerController.MaxOcclusionTriggers) {
            PlayerController.OcclusionZoneCount = PlayerController.MaxOcclusionTriggers;
            DevTools.Logger.Log("OcclusionTriggers > MaxOcclusionTriggers", DevTools.Logger.LogLevel.Error, DevTools.Logger.Sources.Player, null);
            break;
        }
    
        //Add Object Instance ID to list
        PlayerController.OcclusionZones[listId] = occlusionObj.GetComponent<OcclusionZone>();
    
        //Iterate list
        listId++;
    }
    
    ResetActiveZoneStates();
}

[System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
private void ResetActiveZoneStates() {
    //Set current zone to 0
    PlayerController.CurrentOczId = 0;
    //Give all zones false state
    for (int i = 0; i < PlayerController.MaxOcclusionTriggers; i++) { ZoneStates[i] = false; }
    //Finds zone player is in and gives it true state
    OcclusionZone _currentZone = PlayerController.OcclusionZones[0];
    bool _playerInZone = false;
    for (int i = 0; i < PlayerController.OcclusionZoneCount; i++) {
        _currentZone = PlayerController.OcclusionZones[i]; _playerInZone = false;
        _currentZone.BoxBounds.CheckIfPointInside(ref this.PlayerPos, ref _playerInZone);
        if (_playerInZone) {
            ZoneStates[i] = true; PlayerController.CurrentOczId = i; 
            PlayerController.CurrentZone = _currentZone; PlayerController.CurrentZoneBoxColBounds = _currentZone.BoxBounds;
            _currentZone.IsVisible = true; _currentZone.UpdateState();
            break;
        }
    }

    //Finds all zones visible from player zone and gives them true state
    //Loops through main zones list so that zonestate bool is in right place
    for (int i = 0; i < PlayerController.OcclusionZoneCount; i++) {
        //Check if zone in state-list is in zones visible from here list
        int _zonesVisibleCount = _currentZone.ZonesVisibleFromHere.Count;
        //instance id of zone
        int _zoneInstanceId = PlayerController.OcclusionZones[i].InstanceId;
        //Loop through zones visible from current zone
        for (int j = 0; j < _zonesVisibleCount; j++) {
            //Found position, set zone state for this bool pos, and set zone visible and tell it to update its object states
            if (_zoneInstanceId == _currentZone.ZonesVisibleFromHere[j].InstanceId) {
                ZoneStates[i] = true; OcclusionZone _otherVisibleZone = PlayerController.OcclusionZones[i];
                _otherVisibleZone.IsVisible = true; _otherVisibleZone.UpdateState();
                break;
            }
        }
    }
    
    //loop through all zones and set non-visible state for those that aren't set to true
    OcclusionZone ocz; for (int i = 0; i < PlayerController.OcclusionZoneCount; i++) {
        ocz =  PlayerController.OcclusionZones[i];
        if (ZoneStates[i] == false) {
            ocz.IsVisible = false; ocz.UpdateState();
        } 
    }
}

[System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
private void UpdateOcclusionZoneStates() {
    //Process the queue 1-step
    OcclusionQueueManager.ProcessQueues();

    bool _foundZone = false; bool _plyrFoundInside = false;  //Reset foundzone & plyr found in zone to false
    //Check if player NOT inside active zone via bounds-check, so we only go looking if we need to...
    PlayerController.CurrentZoneBoxColBounds.CheckIfPointInside(ref this.PlayerPos, ref _plyrFoundInside); if (_plyrFoundInside) { return; }
    
    //Iterate over all occlusion zones looking for player, but skip those that aren't active (aka zones visible from the zone the player was last in)
    for (int i = 0; i < PlayerController.MaxOcclusionTriggers; i++) { if (PlayerController.ZoneStates[i]) {
            //Check if the player is inside this occlusion zone...
            bool _playerFoundInZone = false; PlayerController.OcclusionZones[i].BoxBounds.CheckIfPointInside(ref this.PlayerPos, ref _playerFoundInZone); if (_playerFoundInZone) {
                _foundZone = true; PlayerController.ZoneStates[i] = true;                                             //Set zone flag in proper spot, set foundzone flag
                PlayerController.CurrentOczId = i; PlayerController.CurrentZone = PlayerController.OcclusionZones[i]; //Set current zone id & current zone val
                PlayerController.CurrentZoneBoxColBounds = PlayerController.CurrentZone.BoxBounds;                    //Set current zone custom bounds property
                PlayerController.CurrentZone.IsVisible = true; PlayerController.CurrentZone.UpdateState();            //Set state to show & run UpdateState to enable the objects/renderers/etc
                break;                                                                                                //Exit for loop early, we found the player already
            }
        }
    }

    //If zone not found, go looking through all zones
    if (!_foundZone) {   for (int i = 0; i < PlayerController.MaxOcclusionTriggers; i++) {
            //Check if zone exists & check if player is inside zone...(Expensive!, logs a warning about having to do this...)
            if (PlayerController.OcclusionZones[i] == null) { Debug.LogWarning("Expensive: Player outside of occlusion zones!, nullcheck per-frame!"); continue; };
            bool _playerFoundInZone = false; PlayerController.OcclusionZones[i].BoxBounds.CheckIfPointInside(ref this.PlayerPos, ref _playerFoundInZone); if (_playerFoundInZone) {
                _foundZone = true; PlayerController.ZoneStates[i] = true;                                              //Player inside zone, set zone flag in proper spot and foundzone flag
                PlayerController.CurrentOczId = i; PlayerController.CurrentZone = PlayerController.OcclusionZones[i];  //Set current zone id & current zone val
                PlayerController.CurrentZoneBoxColBounds = PlayerController.CurrentZone.BoxBounds;                     //Set current zone custom bounds property
                PlayerController.CurrentZone.IsVisible = true; PlayerController.CurrentZone.UpdateState();             //Set state to show & run UpdateState to enable the objects/renderers/etc
                break;                                                                                                 //Exit for loop early, we found the player already
            } //End of if player found in zone
        } //End of for loop
    } //End of player zone not found
    
    if (!_foundZone) { return; } _foundZone = false;  //Exit if we stil havent found the zone, cuz what can we do? already logged a warn
    
    //Finds all zones visible from player zone and gives them true state, loops through all zones to find them
    for (int i = 0; i < PlayerController.OcclusionZoneCount; i++) {
        OcclusionZone _otherVisibleZone = PlayerController.OcclusionZones[i];
        int _zonesVisibleCount = PlayerController.CurrentZone.ZonesVisibleFromHere.Count;
        int _zoneInstanceId = _otherVisibleZone.InstanceId;
        //Loop through zones visible from current zone, If we find zone vis from here, set zone state bool, set zone visible tell it to update object state, then exit loop
        for (int j = 0; j < _zonesVisibleCount; j++) { if (_zoneInstanceId == PlayerController.CurrentZone.ZonesVisibleFromHere[j].InstanceId) {
            _foundZone = true; PlayerController.ZoneStates[i] = true; _otherVisibleZone.IsVisible = true; _otherVisibleZone.UpdateState();
            break;                                                                                 
        }    }
        //If zone was not in visible from here list, set it to hide
        if (!_foundZone) {
            PlayerController.ZoneStates[i] = false; _otherVisibleZone.IsVisible = false; _otherVisibleZone.UpdateState();
        }
    }
}

