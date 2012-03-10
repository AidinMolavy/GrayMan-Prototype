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
		

	private List<ISaveAndLoadAgent> _agents;//list of agents(Classes that implement ISaveAndLoadAgent).
	//private List<ISaveAndLoadAgent> _agents_Exclude;//list of object that dont need to save or load.
	private Stream      		    _fileStream; 
    
	private  List<stSaveInfo>  		_saves;//List of all saved file and theire informations.
    //private  stInfo 			    _currentInfo;//hold current informatin that save file need.
    private  stSaveInfo             _currentSaveInfo;
    private string                  _currentDirectory;
	private const string			_defualtFilePath  = "\\Save";  
	private const string      		_defualtFileName  = "save";
	private const eFormatters       _defualtFormatter = eFormatters.Binary;

    public stSaveInfo CurrentSaveInfo {
        get {
            return this._currentSaveInfo;
        }
        set {
            _currentSaveInfo = value;
        }
    } 
   
	void               Awake(){
		
        _currentDirectory = Directory.GetCurrentDirectory();
		_saves = new List<stSaveInfo>();
		_agents = new List<ISaveAndLoadAgent>();
        _currentSaveInfo = new stSaveInfo();
		Instance = this;	
	}
	
    IEnumerator 	   Start()
	{
                
//		CChair.Instance.var1 = 0;
//		CChair.Instance.var2 = 1;
//		
//        CBahram.Instance.var1 = 2;
//        CBahram.Instance.var2 = 3;
//        
//		stSaveInfo saveInfo = new stSaveInfo();
//		saveInfo.filePath = GeneratePath(0);
//		saveInfo.info.dateAndTime = "2102-03-07 -- 8:22PM";
//		saveInfo.info.elapsedTime = "0:0 Min";
//		saveInfo.info.saveCounts = 0;
//        saveInfo.info.index = 0;
//		_saves.Add(saveInfo);
//		
//	    saveInfo = new stSaveInfo();
//		saveInfo.filePath = GeneratePath(1);
//		saveInfo.info.dateAndTime = "2102-03-07 -- 8:24PM";
//		saveInfo.info.elapsedTime = "0:1 Min";
//		saveInfo.info.saveCounts = 1;
//        saveInfo.info.index = 1;
//		_saves.Add(saveInfo);
//
//	    saveInfo = new stSaveInfo();
//		saveInfo.filePath = GeneratePath(2);
//		saveInfo.info.dateAndTime = "2102-03-07 -- 8:30PM";
//		saveInfo.info.elapsedTime = "0:25 Min";
//		saveInfo.info.saveCounts = 2;		
//        saveInfo.info.index = 2;
//		_saves.Add(saveInfo);
//		
       yield return StartCoroutine(JustWait(1.0f));//wait unitl another Start() functions done.
//        
//        _currentSaveInfo.filePath = GeneratePath(2);
//        _currentSaveInfo.info.dateAndTime = "2102-03-07 -- 8:30PM";
//        _currentSaveInfo.info.elapsedTime = "0:1 Min";
//        _currentSaveInfo.info.index       = 2;
//        _currentSaveInfo.info.saveCounts =  2;
//        Save(_saves[2]);
        
        GetSaves();
        
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
            print("Save File Path = " + _saves[i].filePath);
            print("Save Date and Time = " + _saves[i].info.dateAndTime);
            print("Save Elapsed Time = " + _saves[i].info.elapsedTime);
            print("Save Counts = " + _saves[i].info.saveCounts.ToString()); 
        }
            
     
          
    }
	
	public bool        Save(stSaveInfo saveInfo){
		
		int index = GetSaveIndex(saveInfo);
		_fileStream = File.Open(_saves[index].filePath,FileMode.Create);
		_currentSaveInfo.info.saveCounts += 1;
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
	
	public bool        exLoad(stSAL loadInfo){
        
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
		newSave.filePath = _defualtFilePath + _defualtFileName +  _currentSaveInfo.info.saveCounts.ToString("D3");
		_saves.Add(newSave);
		return newSave;
						
	}
	
	public string      GetElapsedTime()
	{
		return "elapsed time UPDATED";
	}
	
	public string      GetDateAndTime()
	{
		return "Date and time UPDATED";
	}
	
	public int         GetSaveCounts()
	{
		return _currentSaveInfo.info.saveCounts;
	}
	
	private int        GetSaveIndex(stSaveInfo si)
	{
		return _saves.IndexOf(si);
	}
	
	private bool       UpdateInfo(stSaveInfo info){
		
		int i = _saves.IndexOf(info);
		if (i == -1) return false;

		_saves[i].info.elapsedTime = GetElapsedTime();
		_saves[i].info.dateAndTime = GetDateAndTime();
		_saves[i].info.saveCounts  = GetSaveCounts();
			
		return true;
	}
    
    private bool       SaveDirExist(){
          
        return Directory.Exists(_currentDirectory + _defualtFilePath); 
        
    }
    
    private bool       GetSaves(){
        
        if (SaveDirExist() == false) return false;
        string path = _currentDirectory + _defualtFilePath;
        string[] files = Directory.GetFiles(path);
        foreach (string file in files)
        {
            stSAL tmpSAL =  new stSAL();
            tmpSAL.path = file;
            tmpSAL.agents.Add (CSaveAndLoadManager_SALAgent.Instance);
            tmpSAL.eAgent = eInOrExclude.Include;
            exLoad(tmpSAL);
            _saves.Add(_currentSaveInfo);
        }
        //people.Sort(delegate(Person p1, Person p2) { return p1.age.CompareTo(p2.age); })
        
//        _saves.Sort(
//            delegate(stSaveInfo si1,stSaveInfo si2)
//            {
//                return si1.info.index.CompareTo(si2.info.index);
//            } 
//         );
        
        return true;
    }
        
	private string	   GeneratePath(int i)
	{
		
		return _currentDirectory + _defualtFilePath +"\\"+ _defualtFileName + i.ToString("D3");
	}
    
    private IEnumerator JustWait(float time){
        
        
        yield return new WaitForSeconds(time);
        
    }
		
}
