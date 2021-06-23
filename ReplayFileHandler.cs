//#define ProfileLog
//#undef ProfileLog

namespace SkillSurf.Replays {
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using static System.Text.Encoding;
    using UnityEngine;
    using System.Threading;
    
    public struct ReplayFileHandler {
                
        private static readonly System.String[] IntStrings_Triple0 = new System.String[] {
            "000","001","002","003","004","005","006","007","008","009","010","011","012","013","014","015","016","017","018","019","020", "021","022","023","024","025","026","027","028","029","030","031","032","033","034","035","036","037","038","039","040", "041","042","043","044","045","046","047","048","049","050","051","052","053","054","055","056","057","058","059","060", "061","062","063","064","065","066","067","068","069","070","071","072","073","074","075","076","077","078","079","080", "081","082","083","084","085","086","087","088","089","090","091","092","093","094","095","096","097","098","099","100", "101","102","103","104","105","106","107","108","109","110","111","112","113","114","115","116","117","118","119","120", "121","122","123","124","125","126","127","128","129","130","131","132","133","134","135","136","137","138","139","140", "141","142","143","144","145","146","147","148","149","150","151","152","153","154","155","156","157","158","159","160", "161","162","163","164","165","166","167","168","169","170","171","172","173","174","175","176","177","178","179","180", "181","182","183","184","185","186","187","188","189","190","191","192","193","194","195","196","197","198","199","200", "201","202","203","204","205","206","207","208","209","210","211","212","213","214","215","216","217","218","219","220", "221","222","223","224","225","226","227","228","229","230","231","232","233","234","235","236","237","238","239","240", "241","242","243","244","245","246","247","248","249","250","251","252","253","254","255","256","257","258","259","260", "261","262","263","264","265","266","267","268","269","270","271","272","273","274","275","276","277","278","279","280", "281","282","283","284","285","286","287","288","289","290","291","292","293","294","295","296","297","298","299","300", "301","302","303","304","305","306","307","308","309","310","311","312","313","314","315","316","317","318","319","320", "321","322","323","324","325","326","327","328","329","330","331","332","333","334","335","336","337","338","339","340", "341","342","343","344","345","346","347","348","349","350","351","352","353","354","355","356","357","358","359","360", "361","362","363","364","365","366","367","368","369","370","371","372","373","374","375","376","377","378","379","380", "381","382","383","384","385","386","387","388","389","390","391","392","393","394","395","396","397","398","399","400", "401","402","403","404","405","406","407","408","409","410","411","412","413","414","415","416","417","418","419","420", "421","422","423","424","425","426","427","428","429","430","431","432","433","434","435","436","437","438","439","440", "441","442","443","444","445","446","447","448","449","450","451","452","453","454","455","456","457","458","459","460", "461","462","463","464","465","466","467","468","469","470","471","472","473","474","475","476","477","478","479","480","481","482","483","484","485","486","487","488","489","490","491","492","493","494","495","496","497","498","499",
            "500", "501","502","503","504","505","506","507","508","509","510","511","512","513","514","515","516","517","518","519","520", "521","522","523","524","525","526","527","528","529","530","531","532","533","534","535","536","537","538","539","540", "541","542","543","544","545","546","547","548","549","550","551","552","553","554","555","556","557","558","559","560", "561","562","563","564","565","566","567","568","569","570","571","572","573","574","575","576","577","578","579","580", "581","582","583","584","585","586","587","588","589","590","591","592","593","594","595","596","597","598","599","600", "601","602","603","604","605","606","607","608","609","610","611","612","613","614","615","616","617","618","619","620", "621","622","623","624","625","626","627","628","629","630","631","632","633","634","635","636","637","638","639","640", "641","642","643","644","645","646","647","648","649","650","651","652","653","654","655","656","657","658","659","660", "661","662","663","664","665","666","667","668","669","670","671","672","673","674","675","676","677","678","679","680", "681","682","683","684","685","686","687","688","689","690","691","692","693","694","695","696","697","698","699","700", "701","702","703","704","705","706","707","708","709","710","711","712","713","714","715","716","717","718","719","720", "721","722","723","724","725","726","727","728","729","730","731","732","733","734","735","736","737","738","739","740", "741","742","743","744","745","746","747","748","749","750","751","752","753","754","755","756","757","758","759","760", "761","762","763","764","765","766","767","768","769","770","771","772","773","774","775","776","777","778","779","780", "781","782","783","784","785","786","787","788","789","790","791","792","793","794","795","796","797","798","799","800", "801","802","803","804","805","806","807","808","809","810","811","812","813","814","815","816","817","818","819","820", "821","822","823","824","825","826","827","828","829","830","831","832","833","834","835","836","837","838","839","840", "841","842","843","844","845","846","847","848","849","850","851","852","853","854","855","856","857","858","859","860", "861","862","863","864","865","866","867","868","869","870","871","872","873","874","875","876","877","878","879","880", "881","882","883","884","885","886","887","888","889","890","891","892","893","894","895","896","897","898","899","900", "901","902","903","904","905","906","907","908","909","910","911","912","913","914","915","916","917","918","919","920", "921","922","923","924","925","926","927","928","929","930","931","932","933","934","935","936","937","938","939","940", "941","942","943","944","945","946","947","948","949","950","951","952","953","954","955","956","957","958","959","960", "961","962","963","964","965","966","967","968","969","970","971","972","973","974","975","976","977","978","979","980", "981","982","983","984","985","986","987","988","989","990","991","992","993","994","995","996","997","998","999"
        };
        
        public const string ReplayFileExtension = ".replay";
        public const string ReplayFolderName = "replays";
        //Set by ReplaySystem in Awake (via IO.Path.Combine(Application.persistentDataPath, ReplayFolderName)
        public static string ReplayFolderPath; 
        
        private static int[] LoadedFilesHashes = new System.Int32[1024];
        private static int LoadedFileCount = 0;

        //Markers used for custom file format
        private static readonly string
            c_openBracketStr = "[",        //Opens a section
            c_closeBracketStr = "]",       //Closes a section
            c_arrDelimiterStr = ",",       //Delimiter between sections & float values in a vec3
            c_closeBracketDelimStr = "],", //Created for optimization & readability purposes, it's used a lot
            c_sameAsLastValStr = "#",      //Marker to tell decompressor to replace the value with the previous frames value
            c_addToLastValStr = ">",       //Marker to tell decompressor to add this value to the previous frames value
            c_takeFromLastValStr = "<";    //Marker to tell decompressor to take this value from the previous frames value
        
        private static readonly byte c_openBracketByte = ASCII.GetBytes(c_openBracketStr)[0];
        private static readonly byte[] c_openBracketBytes  = ASCII.GetBytes(c_openBracketStr);

        private static readonly byte c_closeBracketByte = ASCII.GetBytes(c_closeBracketStr)[0];
        private static readonly byte[] c_closeBracketBytes = ASCII.GetBytes(c_closeBracketStr);

        private static readonly byte c_arrDelimiterByte = ASCII.GetBytes(c_arrDelimiterStr)[0];
        private static readonly byte[] c_arrDelimiterBytes = ASCII.GetBytes(c_arrDelimiterStr);

        private static readonly byte[] c_closeBracketDelimBytes = ASCII.GetBytes(c_closeBracketDelimStr);

        private static readonly byte c_sameAsLastValByte = ASCII.GetBytes(c_sameAsLastValStr)[0];
        private static readonly byte[] c_sameAsLastValBytes = ASCII.GetBytes(c_sameAsLastValStr);

        private static readonly byte c_addToLastValByte = ASCII.GetBytes(c_addToLastValStr)[0];
        private static readonly byte[] c_addToLastValBytes = ASCII.GetBytes(c_addToLastValStr);

        private static readonly byte c_takeFromLastValByte = ASCII.GetBytes(c_takeFromLastValStr)[0];
        private static readonly byte[] c_takeFromLastValBytes = ASCII.GetBytes(c_takeFromLastValStr);
        
        //Defualt values to assign during file load
        private const string Def_UserName = "NoName";
        private const string Def_LevelName = "NoLevel";
        private const string Def_TimeFloatStr = "999.999";

        //Capture group names
        private const string CGN_User = "username";
        private const string CGN_Level = "levelname";
        private const string CGN_TimeStr = "timestr";
        //private const string CGN_TimeFL = "timefl";
        private const string CGN_Pos = "pos", CGN_Vel = "vel", CGN_Dir = "dir";
        private const string CGN_Pos_X = "posx", CGN_Pos_Y = "posy", CGN_Pos_Z = "posz";
        private const string CGN_Vel_X = "velx", CGN_Vel_Y = "vely", CGN_Vel_Z = "velz";
        private const string CGN_Dir_X = "dirx", CGN_Dir_Y = "diry", CGN_Dir_Z = "dirz";
        private const string CGN_FrameLen = "flen";
        
        //Regex Capture Strings
        private const string Reg_ReplayInfo = 
            @"\[\"+
            @"[(?<"+CGN_User+@">[0-9\da-z_\-A-Z]*)\]\"+
            @"[(?<"+CGN_Level+@">[0-9\da-z_\-A-Z]*)\]\"+
            @"[(?<"+CGN_TimeStr+@">[\d\.]*)\]";
        
        private const string Reg_ReplayFrames = 
            @"\[\["+
                @"(?<"+CGN_Pos+@">"+
                    @"(?<"+CGN_Pos_X+@">[0-9#\-\<\>]+[\.]*[0-9]*),"+
                    @"(?<"+CGN_Pos_Y+@">[0-9#\-\<\>]+[\.]*[0-9]*),"+
                    @"(?<"+CGN_Pos_Z+@">[0-9#\-\<\>]+[\.]*[0-9]*)"+
                @")\],\["+
                @"(?<"+CGN_Vel+@">"+
                    @"(?<"+CGN_Vel_X+@">[0-9#\-\<\>]+[\.]*[0-9]*),"+
                    @"(?<"+CGN_Vel_Y+@">[0-9#\-\<\>]+[\.]*[0-9]*),"+
                    @"(?<"+CGN_Vel_Z+@">[0-9#\-\<\>]+[\.]*[0-9]*)"+
                @")\],\["+   
                @"(?<"+CGN_Dir+@">"+
                    @"(?<"+CGN_Dir_X+@">[0-9#\-\<\>]+[\.]*[0-9]*),"+
                    @"(?<"+CGN_Dir_Y+@">[0-9#\-\<\>]+[\.]*[0-9]*),"+
                    @"(?<"+CGN_Dir_Z+@">[0-9#\-\<\>]+[\.]*[0-9]*)\]),"+
                    @"\[(?<"+CGN_FrameLen+@">[0-9#\-\<\>]+[\.]*[0-9]*)"+
                @"\]\]\,";

        
        //Load replay into ReplayDataList from given replay file name
        public static  void LoadReplayFromFile(ref List<ReplayData> replayDataList, string replayFileName = "EmptyFileName") {
        
            //Create file path
            string fpath = ReplayFileHandler.ReplayFolderPath + "/" + replayFileName;
            int hashCode = fpath.GetHashCode();
            bool alreadyLoaded = false;
            for (int i = 0; i < LoadedFileCount; i++) {
                //File has been loaded already
                if (ReplayFileHandler.LoadedFilesHashes[i] == hashCode) {
                    alreadyLoaded = true;
                }
            }

            if (alreadyLoaded) { return; }
            
            //Add hash code
            ReplayFileHandler.LoadedFilesHashes[ReplayFileHandler.LoadedFileCount] = fpath.GetHashCode();
            
            //Increase loaded file count
            LoadedFileCount++;
            
            //Check file exists
            if (System.IO.File.Exists(fpath)) {
                
                //Create object to output to & run decompression func on file
                ReplayData outputData = new ReplayData(); outputData.CreateNew();
                
                try {
                    //Decompress data from file into outputData replayData object
                    if (!ReplayFileHandler.DecompressReplayFile(fpath, ref outputData)) {
                        //Log the error
                        DevTools.Logger.Log("Replay could not be decompressed: " + fpath,
                            DevTools.Logger.LogLevel.Exception, DevTools.Logger.Sources.Replays);
                        //empty replay data
                        outputData.EmptyData();
                        //Move on to next replay file
                        return;
                    }

                    //Catch exceptions (common with file system)
                } catch (System.Exception e) {
                    //Log Exception details
                    DevTools.Logger.Log("LoadReplayFromFile: Failed to Decompress: " + fpath,
                        DevTools.Logger.LogLevel.Exception, DevTools.Logger.Sources.Replays);
                    DevTools.Logger.Log("Exception Message: " + e.Message,
                        DevTools.Logger.LogLevel.Exception, DevTools.Logger.Sources.Replays);
                    DevTools.Logger.Log("Exception Data: " + e.Data,
                        DevTools.Logger.LogLevel.Exception, DevTools.Logger.Sources.Replays);
                    DevTools.Logger.Log("Exception Source: " + e.Source,
                        DevTools.Logger.LogLevel.Exception, DevTools.Logger.Sources.Replays);
                    //empty replay data
                    outputData.EmptyData();
                    return;
                }

                //Add replay data to our referenced list of replay data once we have checked it isnt already there
                int rCount = replayDataList.Count;
                ReplayData rData; Vector3 newVec; Vector3 oldVec; 
                for (int i = 0; i < rCount; i++) {
                    //Grab data
                    rData = (replayDataList[i]);
                    //If the day, username, framecount, completion time string, and level name are all equal
                    if (rData.DateString == outputData.DateString
                        && rData.UserName == outputData.UserName
                        && rData.FrameCount == outputData.FrameCount
                        && rData.LevelCompletionTimeBv32.GetStr() == outputData.LevelCompletionTimeBv32.GetStr()
                        && rData.LevelName == outputData.LevelName) 
                    {
                        //Check if the frames are equal by starting from 50% through the replay frames data
                        //and comparing position with the existing replay data, if we get <minMatches> in a row
                        //then we declare it to be already-loaded and skip adding it to the replay data list
                        //by setting the already loaded flag to true (it must be false to reach here in the code)
                        int fCount = rData.FrameCount, matchCount = 0; const ushort minMatches = 6;
                        for (int f = (int) fCount/2; f < fCount; ++f) {     //Starting at 50% in, loop from fc/2 to fc
                            oldVec = rData.ReplayFrames[f].position.ToUnityVec3();      //Get vec from replay data list
                            newVec = outputData.ReplayFrames[f].position.ToUnityVec3(); //Get vec from loaded file
                            if (newVec != oldVec) { break; }                            //Exit loop if no match found, (move on to next replay data in replayDataList via outerloop (for i))
                            matchCount++;                                               //Increase match count by 1
                            if (matchCount < minMatches) { continue; }                  //If we havent reached minMatches, check next frame
                            alreadyLoaded = true;                                       //We passed minMatches, declare alreadyLoaded
                        }
                    };
                };
                
                if (alreadyLoaded) { return; }   //Final check before we add it to the list
                replayDataList.Add(outputData);  //Add to the list
                
                //Log loaded file
                #if DevLogging
                    DevTools.Logger.Log("LoadReplayFromFile: Loaded: " + "/replays/" + replayFileName,
                        DevTools.Logger.LogLevel.Info, DevTools.Logger.Sources.Replays);
                #endif
                
            } else {
                //This shouldn't happen...But I wanna know about if it does, warning only
                DevTools.Logger.Log("LoadReplayFromFile: No File Found: " + Application.persistentDataPath + "/replays/" + replayFileName,
                    DevTools.Logger.LogLevel.Info, DevTools.Logger.Sources.Replays);
                
            }
        }

        //private static ReplayFileHandlerThreader.SaveReplayToFile_threaded SaveThread = new ReplayFileHandlerThreader.SaveReplayToFile_threaded();
        public static ReplayData _internalReplayDataWhileSaving;
        public static void SaveReplayToFile(ReplayData replayData) {
            _internalReplayDataWhileSaving = replayData;
            ReplayFileHandlerThreader.ReplaySaveJob.StartThread();
        }
        
        //Adds bytes to list
        public static void AddBytes(ref List<byte> byteList, byte[] byteArr) {
            for (int i = 0; i < byteArr.Length; i++) { byteList.Add(byteArr[i]); }
        }
        
        //Decompress replay file from lz4 to custom format to replay data object
        private static System.IO.FileStream FileStream;
        
        #if UNITY_EDITOR && ProfileLog
            private static System.Diagnostics.Stopwatch sysStopWatch = new System.Diagnostics.Stopwatch();
        #endif
        
        private static bool DecompressReplayFile(string replayFilePath, ref ReplayData outputData) {
            
            #if UNITY_EDITOR && ProfileLog
                sysStopWatch.Start();
            #endif
            
            //Check file exists
            try {
                bool fileExists = System.IO.File.Exists(replayFilePath);
                if (!fileExists) { throw new System.Exception("File not Found: " + replayFilePath, null); };
            } catch (System.Exception e) {
                //Log Exception details
                DevTools.Logger.Log("DecompressReplayFile: Failed to Find File: " + replayFilePath,
                    DevTools.Logger.LogLevel.Exception, DevTools.Logger.Sources.Replays);
                DevTools.Logger.Log("Exception Message: " + e.Message,
                    DevTools.Logger.LogLevel.Exception, DevTools.Logger.Sources.Replays);
                DevTools.Logger.Log("Exception Data: " + e.Data,
                    DevTools.Logger.LogLevel.Exception, DevTools.Logger.Sources.Replays);
                DevTools.Logger.Log("Exception Source: " + e.Source,
                    DevTools.Logger.LogLevel.Exception, DevTools.Logger.Sources.Replays);
                //outputData = null;
                return false;
            }

            //Read all bytes from file
            byte[] _lz4bytes;
            try {
                _lz4bytes = System.IO.File.ReadAllBytes(replayFilePath);
            } catch (System.Exception e) {
                //Log Exception details
                DevTools.Logger.Log("DecompressReplayFile: Failed to Read Bytes from File, file Length: " + FileStream.Length+ ", FilePath: " + replayFilePath,
                    DevTools.Logger.LogLevel.Exception, DevTools.Logger.Sources.Replays);
                DevTools.Logger.Log("Exception Message: " + e.Message,
                    DevTools.Logger.LogLevel.Exception, DevTools.Logger.Sources.Replays);
                DevTools.Logger.Log("Exception Data: " + e.Data,
                    DevTools.Logger.LogLevel.Exception, DevTools.Logger.Sources.Replays);
                DevTools.Logger.Log("Exception Source: " + e.Source,
                    DevTools.Logger.LogLevel.Exception, DevTools.Logger.Sources.Replays);
                //outputData = null;
                return false;
            }
            
            #if UNITY_EDITOR && ProfileLog
                sysStopWatch.Stop();
                UnityEngine.Debug.Log("Decompression ReadBytes Time Ticks: " + sysStopWatch.ElapsedTicks + "   Ms: " + sysStopWatch.ElapsedMilliseconds);
                sysStopWatch.Reset();
                sysStopWatch.Start();
            #endif
            
            //Decompress bytes from lz4 format
            byte[] _ccBytes;
            try {
                _ccBytes = new System.Byte[_lz4bytes.Length];
                _ccBytes = lz4.Decompress(_lz4bytes);
            } catch (System.Exception e) {
                //Log Exception details
                DevTools.Logger.Log("DecompressReplayFile: Failed to Decompress with LZ4, FilePath: " + replayFilePath,
                    DevTools.Logger.LogLevel.Exception, DevTools.Logger.Sources.Replays);
                DevTools.Logger.Log("Exception Message: " + e.Message,
                    DevTools.Logger.LogLevel.Exception, DevTools.Logger.Sources.Replays);
                DevTools.Logger.Log("Exception Data: " + e.Data,
                    DevTools.Logger.LogLevel.Exception, DevTools.Logger.Sources.Replays);
                DevTools.Logger.Log("Exception Source: " + e.Source,
                    DevTools.Logger.LogLevel.Exception, DevTools.Logger.Sources.Replays);
                //outputData = null;
                return false;
            }
            
            #if UNITY_EDITOR && ProfileLog
                sysStopWatch.Stop();
                UnityEngine.Debug.Log("LZ4 Decompression Time Ticks: " + sysStopWatch.ElapsedTicks + "   Ms: " + sysStopWatch.ElapsedMilliseconds);
                sysStopWatch.Reset();
                sysStopWatch.Start();
            #endif
            
            //Convert decompressed (from lz4) bytes into string, string is still compressed with custom method
            string _ccByteStr = Encoding.ASCII.GetString(_ccBytes, 0, _ccBytes.Length);
            
            //Use smaller string for first regex set to improve speed
            string _startInfo;
            if (_ccByteStr.Length > 64 * 4) { _startInfo = _ccByteStr.Substring(0, 64 * 4); } else {
                _startInfo = _ccByteStr;
            }

            //Defaults & Checks for later
            bool   _gotUser = false,    _gotLevel = false,     _gotTime = false;
            string _userStr = Def_UserName, _levelStr = Def_LevelName, _timeStr = Def_TimeFloatStr;
            
            //Create regex to extract username _userStr, level name _levelStr, and time float _timeFloatStr
            System.Text.RegularExpressions.Regex replayInfoRegex = new System.Text.RegularExpressions.Regex(
                Reg_ReplayInfo,
                System.Text.RegularExpressions.RegexOptions.Compiled //Faster execution, slower start-up
            );
            
            //Get matches from regex when applied to _startInfo string
            System.Text.RegularExpressions.MatchCollection matchCollection = replayInfoRegex.Matches(_startInfo);
            
            //For each successful match found
            foreach (System.Text.RegularExpressions.Match matchInfo in matchCollection) {
                if (matchInfo.Success) {
                    //For each group (username, levelname, timestr, timefl)
                    for (int j = 0; j < matchInfo.Groups.Count; j++) {
                        //Get group & check if successfully found data for it
                        System.Text.RegularExpressions.Group matchGroup = matchInfo.Groups[j];
                        if (matchGroup.Success) {
                            //Store data found in each group in relevant string
                            switch (matchGroup.Name) {
                                default: break;
                                case (CGN_User):
                                    _userStr = matchGroup.Captures[0].Value; _gotUser = true;
                                    break;
                                case (CGN_Level):
                                    _levelStr = matchGroup.Captures[0].Value;  _gotLevel = true;
                                    break;
                                case (CGN_TimeStr):
                                    _timeStr = matchGroup.Captures[0].Value;  _gotTime = true;
                                    break;
                            }
                        }
                    }
                }
            }
            
            //Check to make sure we found all the data required
            if (!_gotUser || !_gotLevel || !_gotTime) { 
                DevTools.Logger.Log("DecompressReplayFile: Bad Replay Data, no username/levelname/timefloatstr found:GotUser:"
                    +_gotUser+":GotLevel:"+_gotLevel+":GotTime:"+_gotTime+":FilePath:" + replayFilePath,
                    DevTools.Logger.LogLevel.Error, DevTools.Logger.Sources.Replays);
                //outputData = null;
                return false;
            }
            
            //Stores the float value of _timeFloatStr
            float _timefloat;
            if (!System.Single.TryParse(_timeStr, out _timefloat)) {
                DevTools.Logger.Log("DecompressReplayFile: Bad Replay Data: time float str failed to convert to float:timefloatstr:"+ _timefloat+ ", FilePath: " + replayFilePath,
                    DevTools.Logger.LogLevel.Error, DevTools.Logger.Sources.Replays);
                //outputData = null;
                return false;
            }

            short _timeInt_s;
            short _timeInt_ms;
            _timeInt_s = (short) _timefloat;
            _timeInt_ms = (short) ((_timefloat - ((short) _timefloat)) * 1000);
            
            TimeBitVec32 _timeBitVec32 = new TimeBitVec32(ref _timeInt_s, ref _timeInt_ms);
            
            //    Get matches for frame data when regex is applied to entire byte array
            //PS: This is the biggest regex pattern I've ever made.
            replayInfoRegex = new System.Text.RegularExpressions.Regex(
                Reg_ReplayFrames,
                System.Text.RegularExpressions.RegexOptions.Compiled
            );
            
            //Create list of frames captured by regex
            List<ReplayFrame> rFrames = new List<ReplayFrame>();
            
            //Create dummy data for previous frame
            ReplayFrame prevFrame = new ReplayFrame();
            prevFrame.position = new Vec3_Serializable(-999.999f, -999.999f, -999.999f);
            prevFrame.velocity = new Vec3_Serializable(-999.999f, -999.999f, -999.999f);
            prevFrame.direction = new Vec3_Serializable(-999.999f, -999.999f, -999.999f);
            prevFrame.frameLength = 0.02f;
            
            //Apply regex pattern to full custom compressed (cc) byte string
            matchCollection = replayInfoRegex.Matches(_ccByteStr);

            //Iterate through matches (frames)
            foreach (System.Text.RegularExpressions.Match matchInfo in matchCollection) {
                if (!matchInfo.Success) { continue; }
                //Create current frame info to pass to frame decompressor
                ReplayFrame curFrame = new ReplayFrame();
                curFrame.position = new Vec3_Serializable();
                curFrame.velocity = new Vec3_Serializable();
                curFrame.direction = new Vec3_Serializable();
                curFrame.frameLength = 0f;
                //Fills in frame values from each match group (posx, posy, etc)
                DecompressFrameFromRegexMatch(matchInfo, ref curFrame, ref prevFrame);
                //Adds frame to list of frames
                rFrames.Add(curFrame);
                //Sets prev frame to current frame
                prevFrame = curFrame;
            }
            
            //Check we have matches (frames)
            if (rFrames.Count < 1) {
                DevTools.Logger.Log("DecompressReplayFile: Bad Replay Data: No frames found, file path: "+ replayFilePath,
                    DevTools.Logger.LogLevel.Error, DevTools.Logger.Sources.Replays);
                //outputData = null;
                return false;
            }
            
            //Get date
            string _dateString = new System.String(DefaultReplayData.DefaultDateString);
            try {
                System.DateTime fileLastWrite = new System.DateTime(2020,02,27,0,0,0,0);
                fileLastWrite = System.IO.File.GetCreationTime(replayFilePath);
                _dateString = $"{fileLastWrite.Day}/{fileLastWrite.Month}/{fileLastWrite.Year - 2000}";
            } catch (System.Exception e) {
                //Log Exception details
                DevTools.Logger.Log("DecompressReplayFile: Bad Replay Data, Get File Creation Time Failed, file path: "+ replayFilePath,
                    DevTools.Logger.LogLevel.Warning, DevTools.Logger.Sources.Replays);
                DevTools.Logger.Log("Exception Message: " + e.Message,
                    DevTools.Logger.LogLevel.Warning, DevTools.Logger.Sources.Replays);
                DevTools.Logger.Log("Exception Data: " + e.Data,
                    DevTools.Logger.LogLevel.Warning, DevTools.Logger.Sources.Replays);
                DevTools.Logger.Log("Exception Source: " + e.Source,
                    DevTools.Logger.LogLevel.Warning, DevTools.Logger.Sources.Replays);
            }
            
            #if UNITY_EDITOR && ProfileLog
                sysStopWatch.Stop();
                UnityEngine.Debug.Log("Custom Decompression Time Ticks: " + sysStopWatch.ElapsedTicks + "   Ms: " + sysStopWatch.ElapsedMilliseconds);
                sysStopWatch.Reset();
                sysStopWatch.Start();
            #endif
                        
            //Create replay data and fill with starting info
            outputData = new ReplayData();
            outputData.CreateNew();
            outputData.UserName = _userStr;
            outputData.LevelName = _levelStr;
            outputData.LevelCompletionTimeBv32 = _timeBitVec32;
            outputData.FrameCount = rFrames.Count;
            outputData.DateString = _dateString;
            outputData.ReplayFrames = new ReplayFrame[rFrames.Count];
  
            //Unpack list of frames into frame data array
            for (int j = 0; j <  rFrames.Count; j++) {
                outputData.ReplayFrames[j] = rFrames[j];
            }
            
            //Success, ReplayData created! Would have returned false if not.
            return true;
        }
        
        //Decompress frame from regex match capture
        private static void DecompressFrameFromRegexMatch(
            System.Text.RegularExpressions.Match match,
            ref ReplayFrame currentFrame,
            ref ReplayFrame previousFrame
        ) {
            for (int j = 0; j < match.Groups.Count; j++) {
                System.Text.RegularExpressions.Group matchGroup = match.Groups[j];
                if (matchGroup.Success) {
                    string mStr = matchGroup.Value;
                    switch (matchGroup.Name) {
                        //Position
                        case (CGN_Pos_X): { currentFrame.position.x  = DecompressFloat(mStr, ref previousFrame.position.x); break; }
                        case (CGN_Pos_Y): { currentFrame.position.y  = DecompressFloat(mStr, ref previousFrame.position.y); break; }
                        case (CGN_Pos_Z): { currentFrame.position.z  = DecompressFloat(mStr, ref previousFrame.position.z); break; }
                        //Velocity
                        case (CGN_Vel_X): { currentFrame.velocity.x  = DecompressFloat(mStr, ref previousFrame.velocity.x); break; }
                        case (CGN_Vel_Y): { currentFrame.velocity.y  = DecompressFloat(mStr, ref previousFrame.velocity.y); break; }
                        case (CGN_Vel_Z): { currentFrame.velocity.z  = DecompressFloat(mStr, ref previousFrame.velocity.z); break; }
                        //Direction
                        case (CGN_Dir_X): { currentFrame.direction.x = DecompressFloat(mStr, ref previousFrame.direction.x); break; }
                        case (CGN_Dir_Y): { currentFrame.direction.y = DecompressFloat(mStr, ref previousFrame.direction.y); break; }
                        case (CGN_Dir_Z): { currentFrame.direction.z = DecompressFloat(mStr, ref previousFrame.direction.z); break; }
                        //FrameLength
                        case (CGN_FrameLen): { currentFrame.frameLength = DecompressFloat(mStr, ref previousFrame.frameLength); break; }
                    }
                }
            }
        }

        //Decompress Vec3 from custom format
        private static float DecompressFloat(string floatStr, ref float prevFrameVal) {
            
            //Same as last
            if (floatStr == "#") { return prevFrameVal; }
            
            //Used in both next checks
            float valFl = 0f;
            
            //Add to last
            if (floatStr.Contains(c_addToLastValStr)) {
                floatStr = floatStr.Substring(1, floatStr.Length - 1);
                System.Single.TryParse(floatStr, out valFl); 
                return prevFrameVal + valFl;
            }
            
            //take from last
            if (floatStr.Contains(c_takeFromLastValStr)) {
                floatStr = floatStr.Substring(2, floatStr.Length - 2);
                System.Single.TryParse(floatStr, out valFl); 
                return prevFrameVal - valFl;
            }

            //Normal digit, try parse
            if (!System.Single.TryParse(floatStr, out valFl)) {
                DevTools.Logger.Log("DecompressFloat: Parse failed on string-to-float: " + floatStr,
                    DevTools.Logger.LogLevel.Error, DevTools.Logger.Sources.Replays);
            }

            //Return the value, error or not
            return valFl;
        }
        
        //Tutorials MUST be in slot 0, 1 & 2 as TutorialController uses these indexes for the 3 tutorial replay names
        public static readonly string[] GuideReplayNames = {"Tutorial_BuildSpeed", "Tutorial_Jumping", "Tutorial_Flicking", "Egypt_Guide", "MesaTest_Guide"};

        //Gets designated guide replays
        public static void GetGuideReplays() {
            #if DevLogging
                DevTools.Logger.Log("Getting Guide Replays: " + GuideReplayNames.ToString(),
                    DevTools.Logger.LogLevel.Info, DevTools.Logger.Sources.Replays);
            #endif
            
            //Check if replays path exists, if not, create it
            if (!System.IO.Directory.Exists(Application.persistentDataPath + "/"+ReplayFileHandler.ReplayFolderName)) {
                System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/" + ReplayFileHandler.ReplayFolderName);
            }
            
            //For each item in our list, get that replay and re-save it into the replays folder from the guides folder
            foreach (string guideReplayName in GuideReplayNames) {
                
                //Get file path to guide replay in streaming assets
                string guideReplayPath = System.IO.Path.Combine(Application.streamingAssetsPath, "GuideReplays", guideReplayName + ReplayFileHandler.ReplayFileExtension);
                //Debug.Log("GuideReplayPath: " + guideReplayPath);
                if (!System.IO.File.Exists(guideReplayPath)) {
                    DevTools.Logger.Log("GetGuideReplays: Guide Replay not Found:  " + guideReplayPath,
                        DevTools.Logger.LogLevel.Warning, DevTools.Logger.Sources.Replays);
                    continue;
                }

                //Create request to get file, Send it, then wait for it to finish
                UnityEngine.Networking.UnityWebRequest webReq = UnityEngine.Networking.UnityWebRequest.Get(guideReplayPath);
                webReq.SendWebRequest();
                while (!webReq.isDone) { continue; }

                //Got requested data, check for error response
                if (!(webReq.isNetworkError || webReq.isHttpError)) {

                    //Create target file path
                    string filePath = Application.persistentDataPath + "/"+ReplayFileHandler.ReplayFolderName+"/" + guideReplayName + ReplayFileExtension;
                    //Debug.Log("filePath: " + filePath);

                    //File exists already, move on to next replay file
                    if (System.IO.File.Exists(filePath)) { continue; }
                    
                    //File does not exist, create it
                    System.IO.FileStream fileStream = System.IO.File.Create(filePath);
                    
                    //Get data from web request
                    byte[] replayDataBytes = webReq.downloadHandler.data;
                    
                    //Copy data from webreq to file
                    for (int i = 0; i < replayDataBytes.Length; i++) { fileStream.WriteByte(replayDataBytes[i]); }
                    
                    //Close the file stream
                    fileStream.Close();
                    
                } else {
                    DevTools.Logger.Log("GetGuideReplays: Guide Replay Could not Copy:  " + guideReplayName 
                        + ", isNetworkError: " + webReq.isNetworkError + ", isHttpError: " + webReq.isHttpError 
                        + " Error Message: " + webReq.error,
                        DevTools.Logger.LogLevel.Warning, DevTools.Logger.Sources.Replays);
                }
            }
        }
    }
}
