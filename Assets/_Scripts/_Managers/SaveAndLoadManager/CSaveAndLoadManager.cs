using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using stSaveInfo    = CSaveAndLoadTypes.stSaveInfo;
using stInfo        = CSaveAndLoadTypes.stInfo;
using eFormatters   = CSaveAndLoadTypes.eFormatters;
using eInOrExclude  = CSaveAndLoadTypes.eInOrExclude;
using stSAL         = CSaveAndLoadTypes.stSAL;

public class CSaveAndLoadManager : MonoBehaviour,ISaveAndloadClient {

	public static CSaveAndLoadManager Instance ;
    
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
            return this._scene;
        }
        set {
            _scene = value;
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
		
        _invalideFileNameChars = Path.GetInvalidFileNameChars();
        _invalidePathChars     = Path.GetInvalidPathChars();
        _currentDirectory = Directory.GetCurrentDirectory();
        SaveName       =   "sadf";
        SaveFolderName = "TmpSave";  
        SaveDirectoy = _currentDirectory;      
        SavePath = SaveDirectoy + _dirSeperator + SaveFolderName;
		_saves = new List<stSaveInfo>();
		_agents = new List<ISaveAndLoadAgent>();        
		Instance = this;	
	}
	
    IEnumerator 	   Start()
	{
                
  

		stSaveInfo saveInfo = new stSaveInfo();
		saveInfo.filePath = GeneratePath(0);
        print(saveInfo.filePath);
		saveInfo.fileInfo.DateAndTime = "2102-03-07 -- 8:01PM";
		saveInfo.fileInfo.ElapsedTime = "0:0 Min";
		saveInfo.fileInfo.SaveCount = 0;
        saveInfo.fileInfo.Index = 0;
        saveInfo.fileInfo.Scene = "First Scene";
		_saves.Add(saveInfo);
		
	    saveInfo = new stSaveInfo();
		saveInfo.filePath = GeneratePath(1);
		saveInfo.fileInfo.DateAndTime = "2102-03-07 -- 8:15PM";
		saveInfo.fileInfo.ElapsedTime = "0:10 Min";
		saveInfo.fileInfo.SaveCount = 1;
        saveInfo.fileInfo.Index = 1;
        saveInfo.fileInfo.Scene = "Sec Scene";
		_saves.Add(saveInfo);

	    saveInfo = new stSaveInfo();
		saveInfo.filePath = GeneratePath(2);
		saveInfo.fileInfo.DateAndTime = "2102-03-07 -- 8:30PM";
		saveInfo.fileInfo.ElapsedTime = "0:25 Min";
		saveInfo.fileInfo.SaveCount = 2;		
        saveInfo.fileInfo.Index = 2;
        saveInfo.fileInfo.Scene = "Third Scene";
		_saves.Add(saveInfo);

       
        yield return StartCoroutine(JustWait(1.0f));//wait unitl another Start() functions done.
      
       // Save(_saves[2]);
        GetFilesAddress();
        GetSaves();
        print(_saves.Count);
        print(_saves[0].filePath);
        print(_saves[1].filePath);
        print(_saves[2].filePath);
        
//        _currentSaveInfo.filePath = GeneratePath(2);
//        _currentSaveInfo.info.dateAndTime = "2102-03-07 -- 8:30PM";
//        _currentSaveInfo.info.elapsedTime = "0:1 Min";
//        _currentSaveInfo.info.index       = 2;
//        _currentSaveInfo.info.saveCounts =  2;
//        Save(_saves[2]);
        
        
       
        
        
       // GetSaves();
        
       // Load(_saves[2]);
	   //Save(_saves[0]);
        
		
	}
    
    public void        OnSave ()
    {
        print("CsaveAndLoadManager notify about save.");
            
    }

    public void        OnLoad (){
        
       print("CsaveAndLoadManager notify about load.");
       for (int i = 0; i <_saves.Count; i++ ){
            
        }
            
     
          
    }
	
	public bool        Save(stSaveInfo saveInfo){
		
		int index = GetSaveIndex(saveInfo);
        
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
    
    public bool        Load(string path,ISaveAndLoadAgent agent){
        
        string fileName        = Path.GetFileName(path);
        string pathWithoutFile = Path.GetDirectoryName(path); 
        
        if (string.IsNullOrEmpty(path) == true) {
            CDebug.LogError(CDebug.eMessageTemplate.NullRefrences);
            return false;
        }
        
        if (fileName.IndexOfAny(_invalideFileNameChars) != -1){
            CDebug.LogError(CDebug.eMessageTemplate.InvalideFileName);
            return false;
        }
        if(pathWithoutFile.IndexOfAny(_invalidePathChars) != -1){
            CDebug.LogError(CDebug.eMessageTemplate.InvalidePathAddress);
            return false;
        }
        
        if (agent == null) return false;
        
        _fileStream = File.Open(path,FileMode.Open);//Open a file stream
        agent.LoadFromFile(ref _fileStream, _defualtFormatter);
        for(int i =0; i < agent.ClientsInstances.Count; i++)// call OnLoad() functions
            agent.ClientsInstances[i].OnLoad();
        
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
    
    public string      GetSavePath(stSaveInfo  si){
        if (si != null)
            return si.filePath;
        CDebug.LogError("Null refrence.");
        return null;
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
          
        return Directory.Exists(_savePath); 
        
    }
    
    private bool       GetSaves(){
        
        stSaveInfo tmpSaveInfo;
        
        if (SaveDirExist() == false) return false;   
        
        _saves = new List<stSaveInfo>();
        
        for (int i = 0; i < _existingSaveAddress.Length ; i++){
            tmpSaveInfo = new stSaveInfo();
            Load(_existingSaveAddress[i],(ISaveAndLoadAgent)CSaveFileInfo_SALAgent.Instance);
            tmpSaveInfo.fileInfo.DateAndTime = CSaveFileInfo_SALAgent.Instance_Temp.SaveFileInfo.DateAndTime;
            tmpSaveInfo.fileInfo.ElapsedTime = CSaveFileInfo_SALAgent.Instance_Temp.SaveFileInfo.ElapsedTime;
            tmpSaveInfo.fileInfo.Index       = CSaveFileInfo_SALAgent.Instance_Temp.SaveFileInfo.Index;
            tmpSaveInfo.fileInfo.SaveCount   = CSaveFileInfo_SALAgent.Instance_Temp.SaveFileInfo.SaveCount;
            tmpSaveInfo.fileInfo.Scene       = CSaveFileInfo_SALAgent.Instance_Temp.SaveFileInfo.Scene;
            tmpSaveInfo.filePath             = _existingSaveAddress[i];
            _saves.Add(tmpSaveInfo);
        }        
       
        return true;

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
    /// The file address.
    /// </returns>
    private void GetFilesAddress()
    {
        if (string.IsNullOrEmpty(_savePath)){
            CDebug.LogError("Path is empty.");
            return ;
        }
        if(_savePath.LastIndexOfAny(_invalidePathChars) != -1){
            CDebug.LogError("Path contain invalid characters.");
            return ;
        }
        if (SaveDirExist() == false) return ;
        _existingSaveAddress = Directory.GetFiles(SavePath);
        
    }
		
}
