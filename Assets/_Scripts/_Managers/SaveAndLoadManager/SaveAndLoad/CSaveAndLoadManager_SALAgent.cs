using UnityEngine;
using System.Collections.Generic;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
  
using stSaveInfo  = CSaveAndLoadTypes.stSaveInfo;
using stInfo      = CSaveAndLoadTypes.stInfo;

#pragma warning disable 0414
[System.Serializable]
public class CSaveAndLoadManager_SALAgent : MonoBehaviour,ISerializable,ISaveAndLoadAgent {
 
    public static CSaveAndLoadManager_SALAgent Instance;
    private List<stSaveInfo> _saves;
    //private stInfo           _currentInfo;
    private   stSaveInfo       _currentSaveInfo; 
    private List<ISaveAndloadClient> _clientsInstances;
    public List<ISaveAndloadClient> ClientsInstances {
        get {
            return _clientsInstances;
        }
    }    
    
    public  CSaveAndLoadManager_SALAgent (SerializationInfo info, StreamingContext context)
    {
       
        //_saves = (List<stSaveInfo>)info.GetValue("_saves", typeof(List<stSaveInfo>));
        //_currentInfo = (stInfo)info.GetValue("_currentInfo", typeof(stInfo));
        //CSaveAndLoadManager.Instance.Saves = _saves;
        //CSaveAndLoadManager.Instance.CurrentInfo = _currentInfo;
        _currentSaveInfo = (stSaveInfo)info.GetValue("_currentSaveInfo",typeof(stSaveInfo));
        CSaveAndLoadManager.Instance.CurrentSaveInfo = _currentSaveInfo;
    }
    
    void Awake(){
        
        _clientsInstances = new List<ISaveAndloadClient>();
        _saves = new List<stSaveInfo>();
        _currentSaveInfo = new stSaveInfo();
        Instance = this;
        
    }
    
	// Use this for initialization
	void Start () {
        
       CSaveAndLoadManager.Instance.RegisterAgent(this);
       _clientsInstances.Add((ISaveAndloadClient)CSaveAndLoadManager.Instance);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void GetObjectData (SerializationInfo info, StreamingContext context)
    {
       
        //_saves = CSaveAndLoadManager.Instance.Saves;
        //_currentInfo = CSaveAndLoadManager.Instance.CurrentInfo;        
        //info.AddValue("_saves",_saves);
        //info.AddValue("_currentInfo",_currentInfo);
        _currentSaveInfo =  CSaveAndLoadManager.Instance.CurrentSaveInfo;
        info.AddValue("_currentSaveInfo",_currentSaveInfo);
    }
    
    public bool SaveToFile (ref Stream s, CSaveAndLoadTypes.eFormatters format)
    {
         BinaryFormatter bFormatter;
         if (s == null) {Debug.LogError("Null refrence."); return false;}
         if (format == CSaveAndLoadTypes.eFormatters.Binary){       
             bFormatter = new BinaryFormatter();
             bFormatter.Serialize(s,this);
         }       
         return true;      
    }

    public bool LoadFromFile (ref Stream s, CSaveAndLoadTypes.eFormatters format)
    {
        BinaryFormatter bFormatter;
        if (format== CSaveAndLoadTypes.eFormatters.Binary){        
             bFormatter = new BinaryFormatter();
            CSaveAndLoadManager_SALAgent tmpInstance = (CSaveAndLoadManager_SALAgent)bFormatter.Deserialize(s);
            
        }
        return true;        
    }



    
}
