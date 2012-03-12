using UnityEngine;
using System.Collections.Generic;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
  
using stSaveInfo  = CSaveAndLoadTypes.stSaveInfo;
using stInfo      = CSaveAndLoadTypes.stInfo;
using stSaveFileInfo = CSaveAndLoadTypes.stSaveFileInfo;

#pragma warning disable 0414
[System.Serializable]
public class CSaveFileInfo_SALAgent : MonoBehaviour,ISaveAndLoadAgent {
 
    public static CSaveFileInfo_SALAgent Instance; 
    public static CSaveFileInfo_SALAgent Instance_Temp; 
    public stSaveFileInfo SaveFileInfo;
        
    private List<ISaveAndloadClient> _clientsInstances;
    
    public List<ISaveAndloadClient> ClientsInstances {
        get {
            return _clientsInstances;
        }
    }    
    
    public CSaveFileInfo_SALAgent(){}
    
    public  CSaveFileInfo_SALAgent (SerializationInfo info, StreamingContext context)
    {
        SaveFileInfo = new stSaveFileInfo();
        SaveFileInfo.DateAndTime = (string)info.GetValue("DateAndTime",  typeof(string));
        SaveFileInfo.ElapsedTime = (string)info.GetValue("ElapsedTime",  typeof(string));        
        SaveFileInfo.Scene       = (string)info.GetValue("Scene",        typeof(string));
        SaveFileInfo.Index       = (int)   info.GetValue("Index",        typeof(int));
        SaveFileInfo.SaveCount   = (int)   info.GetValue("SaveCount",    typeof(int));        
    }
    
    void Awake(){
        
        _clientsInstances = new List<ISaveAndloadClient>();
        SaveFileInfo = new stSaveFileInfo();  
        Instance = this;
        
    }
    
    void Start () {
        
       CSaveAndLoadManager.Instance.RegisterAgent(this);
      
    }

    public void GetObjectData (SerializationInfo info, StreamingContext context)
    {
     
        SaveFileInfo.DateAndTime = CSaveAndLoadManager.Instance.DateAndTime;
        SaveFileInfo.ElapsedTime = CSaveAndLoadManager.Instance.ElapsedTime;       
        SaveFileInfo.Scene       = CSaveAndLoadManager.Instance.Scene;
        SaveFileInfo.Index       = CSaveAndLoadManager.Instance.GetSaveIndex(CSaveAndLoadManager.Instance.CurrentSave);
        SaveFileInfo.SaveCount   = CSaveAndLoadManager.Instance.SaveCount;
        
        info.AddValue("DateAndTime", SaveFileInfo.DateAndTime, typeof(string));
        info.AddValue("ElapsedTime", SaveFileInfo.ElapsedTime, typeof(string));        
        info.AddValue("Scene",       SaveFileInfo.Scene,       typeof(string));
        info.AddValue("Index",       SaveFileInfo.Index,       typeof(int));
        info.AddValue("SaveCount",   SaveFileInfo.SaveCount,   typeof(int));
        
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
        if (format == CSaveAndLoadTypes.eFormatters.Binary){        
            bFormatter = new BinaryFormatter();
            Instance_Temp = (CSaveFileInfo_SALAgent)bFormatter.Deserialize(s);
            
        }
        return true;        
    }



    
}
