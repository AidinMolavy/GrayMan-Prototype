using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using stSaveInfo  = CSaveAndLoadTypes.stSaveInfo;
using stInfo      = CSaveAndLoadTypes.stInfo;
using eFormatters = CSaveAndLoadTypes.eFormatters;

public class CSaveAndLoadManager : MonoBehaviour {

	public static CSaveAndLoadManager Instance ;
		

	private List<ISaveAndLoadAgent> Agents;//list of object(Classes that implement ISaveAndLoadAgent).
	private List<ISaveAndLoadAgent> Agents_Exclude;//list of object that dont need to save or load.
	private Stream      		    _fileStream;
	//public  List<string>       		Paths;//Path of actual save file on storage device.include name of file.
	
	public  List<stSaveInfo>  		_saves;//List of all saved file and theire informations.
	public  stInfo 					_currentInfo;//hold current informatin that save file need.
	
	private const string			_defualtFilePath  = "\\Save";  
	private const string      		_defualtFileName  = "save";
	private const eFormatters       _defualtFormatter = eFormatters.Binary;
	
	void               Awake(){
		
		_currentInfo = new stInfo();
		_saves = new List<stSaveInfo>();
		Agents = new List<ISaveAndLoadAgent>();
		Agents_Exclude = new List<ISaveAndLoadAgent>();
		Instance = this;	
	}
	
	void Start()
	{
		
		CChair.Instance.var1 = 0;
		CChair.Instance.var2 = 1;
		
		stSaveInfo saveInfo = new stSaveInfo();
		saveInfo.filePath = GeneratePath(0);
		saveInfo.info.dateAndTime = "2102-03-07 -- 8:22PM";
		saveInfo.info.elapsedTime = "0:0 Min";
		saveInfo.info.saveCounts = 0;
		_saves.Add(saveInfo);
		
	    saveInfo = new stSaveInfo();
		saveInfo.filePath = GeneratePath(1);
		saveInfo.info.dateAndTime = "2102-03-07 -- 8:24PM";
		saveInfo.info.elapsedTime = "0:1 Min";
		saveInfo.info.saveCounts = 1;
		_saves.Add(saveInfo);

	    saveInfo = new stSaveInfo();
		saveInfo.filePath = GeneratePath(3);
		saveInfo.info.dateAndTime = "2102-03-07 -- 8:30PM";
		saveInfo.info.elapsedTime = "0:25 Min";
		saveInfo.info.saveCounts = 2;		
		_saves.Add(saveInfo);
			
		Save(_saves[1]);
		CChair.Instance.var1 = 10;
		CChair.Instance.var2 = 20;
		Load(_saves[1]);
		
	}
	
	public bool        Save(stSaveInfo saveInfo){
		
		int index = GetSaveIndex(saveInfo);
		_fileStream = File.Open(_saves[index].filePath,FileMode.Create);
		_currentInfo.saveCounts += 1;
		UpdateInfo(saveInfo);
		
		for (int i = 0; i < Agents.Count; i++ ){
			for(int j = 0; j < Agents[i].ClientInstance.Count; j++){//Notify all clients about save.
				 Agents[i].ClientInstance[j].OnSave();
			}
			Agents[i].SaveToFile(ref _fileStream,_defualtFormatter);
		}
		
		_fileStream.Close();
		return true;
	}
	
	public bool        Load(stSaveInfo loadInfo){
		
		int index = GetSaveIndex(loadInfo);
		_fileStream = File.Open(_saves[index].filePath,FileMode.Open);
		
		for (int i = 0; i < Agents.Count; i++ ){
			
			Agents[i].LoadFromFile(ref _fileStream,_defualtFormatter);
			for(int j = 0; j < Agents[i].ClientInstance.Count; j++){//Notify all clients about load.
				
				 Agents[i].ClientInstance[j].OnLoad();
			}
		}
		_fileStream.Close();
		return true;
	}
	
	public void        RegisterAgent (ISaveAndLoadAgent agent)
	{
		//need debug "agent!= null"
		if (Agents.Contains(agent) == false)//we dont want to have multiple of same instance of agent.
			Agents.Add(agent);
			
	}
				
	public bool    	   RemoveAgent (ISaveAndLoadAgent agent){
		
		//need debug "agent!= null"
		return Agents.Remove(agent);
	}
	
	public stSaveInfo  AddNewSave(){
		
	    stSaveInfo newSave = new stSaveInfo();
		newSave.filePath = _defualtFilePath + _defualtFileName +  _currentInfo.saveCounts.ToString("D3");
		_saves.Add(newSave);
		return newSave;
						
	}
	
	public string      GetElapsedTime()
	{
		return "elapsed time";
	}
	
	public string      GetDateAndTime()
	{
		return "Date and time";
	}
	
	public int         GetSaveCounts()
	{
		return _currentInfo.saveCounts;
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
	
	private string GeneratePath(int i)
	{
		
		return /*_defualtFilePath */ _defualtFileName + i.ToString("D3");
	}
		
}
