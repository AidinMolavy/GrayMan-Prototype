using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using stSaveInfo    = CSaveAndLoadTypes.stSaveInfo;
using stInfo        = CSaveAndLoadTypes.stInfo;
using eFormatters   = CSaveAndLoadTypes.eFormatters;
using eInOrExclude  = CSaveAndLoadTypes.eInOrExclude;
using stSAL         = CSaveAndLoadTypes.stSAL;

public class CSaveAndLoadManager : MonoBehaviour,ISaveAndloadClient {

    private static ArrayList _instances = new ArrayList();
    
    public static CSaveAndLoadManager Instance{
        
        get{
            return (CSaveAndLoadManager)CSingleton.GetSingletonInstance( 
                ref _instances,
                typeof(CSaveAndLoadManager),
                CGlobalInfo.stSaveAndLoad.TagName,
                CGlobalInfo.stSaveAndLoad.GameObjectName);
        }
    }
     
    private stSaveInfo  _currentSave;	

    public stSaveInfo CurrentSave {
        get {
            return this._currentSave;
        }
    }

	private List<ISaveAndLoadAgent> _agents;//list of agents(Classes that implement ISaveAndLoadAgent).
	//private List<ISaveAndLoadAgent> _agents_Exclude;//list of object that dont need to save or load.
	private Stream      		    _fileStream;     
	private  List<stSaveInfo>  		_saves;//List of all saved file and theire informations.  
	private const string			_defualtFilePath  = "\\Save";  
	private const string      		_defualtFileName  = "save";
	private const eFormatters       _defualtFormatter = eFormatters.Binary;
    
    #region SaveFile
    private int                     _saveCount;
    private string                  _elapsedTime;
    private string                  _dateAndTime;
    private string                  _scene;

    public int    SaveCount {
        get {
            return this._saveCount;
        }
        set{
            _saveCount = value;
        }
    }    
    public string ElapsedTime {
        get {
            return Time.timeSinceLevelLoad.ToString();
        }
    }
    public string DateAndTime {
        get {
            return Time.time.ToString();
        }
    }    
    public string Scene {
        get {
            return Application.loadedLevelName;
        }

    }
    #endregion
    
    #region FileAddress
    private string[]     _existingSaveAddress;//Contain address of files that exist in default save path.
    private string       _currentDirectory;    
    private string       _saveDirectory;//Default directory path of save file.(not contain save folder name)
    private string       _saveFolderName;//Default folder name.
    private string       _saveName;//Default file name.
    private string       _savePath;// Equale to : _SaveDirectory + _SaveFolderName
    private string       _saveFileFormat = "D3";//format of save file.(like Save001)
    private char[]       _invalideFileNameChars;
    private char[]       _invalidePathChars;
    private const char   _dirSeperator = '\\';
    
    public string SaveDirectoy {
        get {
            return this._saveDirectory;
        }
        set {
            
             if( string.IsNullOrEmpty(value)){
                CDebug.LogError("File name can not be empty.");
                return;
             }                
             
             if (value.IndexOfAny(_invalidePathChars) != -1){
                CDebug.LogError("File name you set contain invalide characters.");  
                return;
             }                        
            _saveDirectory = value;
            if (string.IsNullOrEmpty(_saveFolderName) != true)
                _savePath  = Path.Combine(_saveDirectory,_saveFolderName);
        }
    }    
    
    public string SaveFolderName {
        get {
            return this._saveFolderName;
        }
        set {
            
            if(string.IsNullOrEmpty(value) == true){
                CDebug.LogError(CDebug.eMessageTemplate.EmptyOrNullString);
                return;
            }                
            _saveFolderName = value;
            if(string.IsNullOrEmpty(_saveDirectory) != true)
                _savePath  = Path.Combine(_saveDirectory,_saveFolderName);
        }
    }

    public string SavePath {
        get {
            return this._savePath;
        }
        set {
            if (string.IsNullOrEmpty(value)){
                CDebug.LogError("path can not be empty or null.");
                return;                      
            }
            if (value.IndexOfAny(_invalidePathChars) != -1){
                CDebug.LogError("Path contain invalid characters.");
                return;
            }
            _savePath   = value;       
            _saveDirectory  = Path.GetDirectoryName(_savePath);
            string[] tmp    = _savePath.Split('\\');             
            _saveFolderName = tmp[tmp.Length-1];           
            
          
        }
    }

    public string SaveName {
        get {
            return this._saveName;
        }
        set {
            
             if( string.IsNullOrEmpty(value)){
                CDebug.LogError(CDebug.eMessageTemplate.EmptyOrNullString);
                return;
             }                
             
             if (value.IndexOfAny(_invalideFileNameChars) != -1){
                CDebug.LogError(CDebug.eMessageTemplate.InvalideFileName);  
                return;
             }
              _saveName = value;     
        }
    }
    #endregion
   
	void               Awake(){
        
        DontDestroyOnLoad(this);
        
        _instances.Add(this);// used for implement singleton pattern
        CSingleton.DestroyExtraInstances(_instances); // remove all of instances of this class except first one.
		
        _invalideFileNameChars = Path.GetInvalidFileNameChars();
        _invalidePathChars     = Path.GetInvalidPathChars();
        _currentDirectory = Directory.GetCurrentDirectory();             
        SaveName       =   "save";        
        SaveFolderName = "TmpSave";  
        SaveDirectoy = _currentDirectory;      
        SavePath = SaveDirectoy + _dirSeperator + SaveFolderName;
		_saves = new List<stSaveInfo>();
		_agents = new List<ISaveAndLoadAgent>();
        _currentSave = new stSaveInfo();
			
	}
	
    IEnumerator 	   Start()
	{
        _existingSaveAddress = GetFilesAddress();
        _saves = RetriveSaves(_existingSaveAddress);
        if(SaveExist() == false){
            CDebug.LogError("No Save File Founded.");
            yield break; 
        }
        _currentSave = _saves[0];
        
        print(_saves[2].fileInfo.SaveCount);
//        
//         CSaveFileInfo_SALContainer c = CSaveFileInfo_SALContainer.Instance;
//         c.SaveFileInfo.Index = 4;
//        print(c.SaveFileInfo.Index);
//		stSaveInfo saveInfo = new stSaveInfo();
//		saveInfo.filePath = GeneratePath(0);
//        print(saveInfo.filePath);
//		saveInfo.fileInfo.DateAndTime = "2102-03-07 -- 8:01PM";
//		saveInfo.fileInfo.ElapsedTime = "0:0 Min";
//		saveInfo.fileInfo.SaveCount = 0;
//        saveInfo.fileInfo.Index = 2;
//        saveInfo.fileInfo.Scene = "First Scene";
//		_saves.Add(saveInfo);
//		
//	    saveInfo = new stSaveInfo();
//		saveInfo.filePath = GeneratePath(1);
//		saveInfo.fileInfo.DateAndTime = "2102-03-07 -- 8:15PM";
//		saveInfo.fileInfo.ElapsedTime = "0:10 Min";
//		saveInfo.fileInfo.SaveCount = 1;
//        saveInfo.fileInfo.Index = 0;
//        saveInfo.fileInfo.Scene = "Sec Scene";
//		_saves.Add(saveInfo);
//
//	    saveInfo = new stSaveInfo();
//		saveInfo.filePath = GeneratePath(2);
//		saveInfo.fileInfo.DateAndTime = "2102-03-07 -- 8:30PM";
//		saveInfo.fileInfo.ElapsedTime = "0:25 Min";
//		saveInfo.fileInfo.SaveCount = 2;		
//        saveInfo.fileInfo.Index = 1;
//        saveInfo.fileInfo.Scene = "Third Scene";
//		_saves.Add(saveInfo);

       
        yield return StartCoroutine(JustWait(1.0f));//wait unitl other Start() functions done.
        
        
        
//        Save(_saves[0]);
//        Save(_saves[1]);
//        Save(_saves[2]);
        
//            Load (CurrentSave);
//        print(_saves.Count);
//        print(_saves[0].fileInfo.Index);
//        print(_saves[1].fileInfo.Index);
//        print(_saves[2].fileInfo.Index);
        
        
        
    }  
    
    public void        OnSave ()
    {
        print("CsaveAndLoadManager notify about save.");
            
    }

    public void        OnLoad (){
        
       print("CsaveAndLoadManager notify about load.");
       
            
     
          
    }
	
	public bool        Save(stSaveInfo saveInfo){
		
		int index = GetSaveIndex(saveInfo);
        _currentSave = saveInfo;// must changed
        SaveCount += 1;
		_fileStream = File.Open(_saves[index].filePath,FileMode.Create);
		UpdateInfo(saveInfo);
		
		for (int i = 0; i < _agents.Count; i++ ){
			for(int j = 0; j < _agents[i].ClientsInstances.Count; j++){//Notify all clients about save.
				 _agents[i].ClientsInstances[j].OnSave();
			}
			_agents[i].SaveToFile(ref _fileStream,_defualtFormatter);
		}
		
		_fileStream.Close();
		return true;
	}
	
	public bool        Load(stSAL loadInfo){
        
        _fileStream = File.Open(loadInfo.path,FileMode.Open);
        
        if (loadInfo.agents == null){            
            for (int i = 0; i < _agents.Count; i++ ){         
                _agents[i].LoadFromFile(ref _fileStream,_defualtFormatter);
                for(int j = 0; j < _agents[i].ClientsInstances.Count; j++){//Notify all clients about load.             
                    _agents[i].ClientsInstances[j].OnLoad();
                }
            }    
            _fileStream.Close();  
            return true;
        }    
        
        if(loadInfo.eAgent == eInOrExclude.Include){
                for (int i = 0; i < _agents.Count; i++ ){                               
                    for(int k =0; k < loadInfo.agents.Count; k++ ){
                        if(_agents[i] == loadInfo.agents[k]){
                            _agents[i].LoadFromFile(ref _fileStream,_defualtFormatter);                    
                            for(int j = 0; j < _agents[i].ClientsInstances.Count; j++){//Notify all clients about load.     
                                _agents[i].ClientsInstances[j].OnLoad();
                        }
                    }            
                }                       
            }                                    
		}
        
        if(loadInfo.eAgent == eInOrExclude.Exclude){
                for (int i = 0; i < _agents.Count; i++ ){                               
                    for(int k =0; k < loadInfo.agents.Count; k++ ){
                        if(_agents[i] != loadInfo.agents[k]){
                            _agents[i].LoadFromFile(ref _fileStream,_defualtFormatter);                    
                            for(int j = 0; j < _agents[i].ClientsInstances.Count; j++){//Notify all clients about load.     
                                _agents[i].ClientsInstances[j].OnLoad();
                        }
                    }            
                }                       
            }                                    
     }        
        
        _fileStream.Close();
		return true;
	
    }
	
    public bool        Load(stSaveInfo loadInfo){
                                 
        int index = GetSaveIndex(loadInfo);
        _fileStream = File.Open(_saves[index].filePath,FileMode.Open);     
        for (int i = 0; i < _agents.Count; i++ )
        {         
            _agents[i].LoadFromFile(ref _fileStream,_defualtFormatter);
            for(int j = 0; j < _agents[i].ClientsInstances.Count; j++)//Notify all clients about load.             
            {
                _agents[i].ClientsInstances[j].OnLoad();
            }
        }
        _fileStream.Close();
        
         return true;
    }
    
    /// <summary>
    /// Load just one agent and its clients in specefice save file.
    /// </summary>
    /// <param name='path'>
    /// Save file path.
    /// </param>
    /// <param name='agent'>
    /// Agent for retriving data from save file.
    /// </param>
    public bool        Load(string path,ISaveAndLoadAgent agent){
        
        string fileName        = Path.GetFileName(path);
        string pathWithoutFile = Path.GetDirectoryName(path); 
        
        if (agent == null){
            CDebug.LogError(CDebug.eMessageTemplate.NullParameter);
            return false;  
        } 
        
        //Check path to isure is not NULL or EMPTY.
        if (string.IsNullOrEmpty(path) == true) {
            CDebug.LogError(CDebug.eMessageTemplate.NullRefrences);
            return false;
        }
        //Check file name of given path to insure is not invalide.
        if (fileName.IndexOfAny(_invalideFileNameChars) != -1){
            CDebug.LogError(CDebug.eMessageTemplate.InvalideFileName);
            return false;
        }
        //Check path without file name to insure is not invalide.
        if(pathWithoutFile.IndexOfAny(_invalidePathChars) != -1){
            CDebug.LogError(CDebug.eMessageTemplate.InvalidePathAddress);
            return false;
        }
        
        Directory.CreateDirectory(pathWithoutFile);//First create directories cuz File.Open Can not create directories    
        _fileStream = File.Open(path,FileMode.Open);//Open a file stream
        bool res = agent.LoadFromFile(ref _fileStream, _defualtFormatter);//call agent's LoadFromFile() function.
        if (res == false) {
            _fileStream.Close();    
            return false;   
        }
        for(int i = 0; i < agent.ClientsInstances.Count; i++)// call OnLoad() functions
            agent.ClientsInstances[i].OnLoad();
        
        _fileStream.Close();
        return true;
        
        
    }
    
	public void        RegisterAgent (ISaveAndLoadAgent agent)
	{
		if (agent == null) Debug.LogError("Null refrence here.");
		if (_agents.Contains(agent) == false)//we dont want to have multiple of same instance of agent.
			_agents.Add(agent);
			
	}
				
	public bool    	   RemoveAgent (ISaveAndLoadAgent agent){
		
		//need debug "agent!= null"
		return _agents.Remove(agent);
	}
	
	public stSaveInfo  AddNewSave(){
		
	    stSaveInfo newSave = new stSaveInfo();
		//newSave.filePath = _defualtFilePath + _defualtFileName +  _currentSaveInfo.info.saveCounts.ToString("D3");
		_saves.Add(newSave);
		return newSave;
						
	}				
	
	public int         GetSaveIndex(stSaveInfo si)
	{
		return _saves.IndexOf(si);
	}
    
    public List<stSaveInfo> GetSaves()  {
        
        return _saves;
        
    }
    
    public bool SaveExist(){
        if (_saves.Count <= 0)
            return false;
        return true;
    }
    
	private bool       UpdateInfo(stSaveInfo info){
		
		int i = _saves.IndexOf(info);
		if (i == -1) return false;

		_saves[i].fileInfo.ElapsedTime = ElapsedTime;
		_saves[i].fileInfo.DateAndTime = DateAndTime;
		_saves[i].fileInfo.SaveCount   = SaveCount;
        _saves[i].fileInfo.Scene       = Scene;
        
		return true;
	}
    
    private bool       SaveDirExist(){
          
        return Directory.Exists(SavePath); 
        
    }
    
    /// <summary>
    /// Iterate through files from given "path" and fill "_saves" field with informations from files.
    /// This function will call when the application start.
    /// </summary>
    /// <returns>
    /// The saves.
    /// </returns>
    /// <param name='addresses'>
    /// If set to <c>true</c> addresses.
    /// </param>
    private List<stSaveInfo>  RetriveSaves(string[] addresses){
        
        stSaveInfo tmpSaveInfo;
        List<stSaveInfo> tmpSaves = new List<stSaveInfo>();
        
        if (SaveDirExist() == false){
            CDebug.LogError("Save directory not exist.");   
            return null ;
        }        
        
        for (int i = 0; i < addresses.Length ; i++){           
            tmpSaveInfo = new stSaveInfo();            
            bool res = Load(addresses[i], (ISaveAndLoadAgent)CSaveFileInfo_SALAgent.Instance);
            if (res == false)
                continue;
            tmpSaveInfo.fileInfo.DateAndTime = CSaveFileInfo_SALAgent.Instance_Container.SaveFileInfo.DateAndTime;
            tmpSaveInfo.fileInfo.ElapsedTime = CSaveFileInfo_SALAgent.Instance_Container.SaveFileInfo.ElapsedTime;
            tmpSaveInfo.fileInfo.Index       = CSaveFileInfo_SALAgent.Instance_Container.SaveFileInfo.Index;
            tmpSaveInfo.fileInfo.SaveCount   = CSaveFileInfo_SALAgent.Instance_Container.SaveFileInfo.SaveCount;
            tmpSaveInfo.fileInfo.Scene       = CSaveFileInfo_SALAgent.Instance_Container.SaveFileInfo.Scene;
            tmpSaveInfo.filePath             = addresses[i];
            tmpSaves.Add(tmpSaveInfo);
        }        
        tmpSaves.Sort(delegate(stSaveInfo s1,stSaveInfo s2){return s1.fileInfo.Index.CompareTo(s2.fileInfo.Index);});
        return tmpSaves;

    }
        
	private string	   GeneratePath(int i)
	{
		
		return SavePath + _dirSeperator + SaveName + i.ToString(_saveFileFormat);
	}
    
    private IEnumerator JustWait(float time){
        
        
        yield return new WaitForSeconds(time);
        
    }          
    
    /// <summary>
    /// return all files address that in defualt save location.
    /// </summary>
    /// <returns>
    /// Return files addresses. return Null if somthing wrong.
    /// </returns>
    private string[]  GetFilesAddress()
    {
        if (string.IsNullOrEmpty(SavePath)){
            CDebug.LogError("Path is empty.");
            return null ;
        }
        if(_savePath.LastIndexOfAny(_invalidePathChars) != -1){
            CDebug.LogError("Path contain invalid characters.");
            return null ;
        }
        if (SaveDirExist() == false){
            CDebug.LogError("Save directory not exist.");   
            return null ;
        }
        
        return  Directory.GetFiles(SavePath);
    }
	

}
