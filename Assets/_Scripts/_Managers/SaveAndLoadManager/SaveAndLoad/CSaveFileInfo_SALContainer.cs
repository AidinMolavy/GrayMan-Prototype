using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;
using System.Security;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
  
using stSaveInfo  = CSaveAndLoadTypes.stSaveInfo;
using stInfo      = CSaveAndLoadTypes.stInfo;
using stSaveFileInfo = CSaveAndLoadTypes.stSaveFileInfo;

[System.Serializable]
public sealed class CSaveFileInfo_SALContainer : ISerializable  {
    
#region Private Fields
    private static ArrayList _instances = new ArrayList();
#endregion
    
#region Public Fields
    public stSaveFileInfo SaveFileInfo;
#endregion
    
#region Properties
    public static CSaveFileInfo_SALContainer Instance{
    get{
        return (CSaveFileInfo_SALContainer)CSingleton.GetSingletonInstance(
            ref _instances, 
            typeof(CSaveFileInfo_SALContainer));
    }
}
#endregion
    
#region Constructors
    public CSaveFileInfo_SALContainer(){
        //singleton system
        _instances.Add(this);
        CSingleton.DestroyExtraInstances(_instances);
        
        SaveFileInfo = new stSaveFileInfo();//init
    }

    //Serializatoin counstructor
    public  CSaveFileInfo_SALContainer (SerializationInfo info, StreamingContext context){      
    SaveFileInfo = new stSaveFileInfo();
    SaveFileInfo.DateAndTime = (string)info.GetValue("DateAndTime",  typeof(string));
    SaveFileInfo.ElapsedTime = (string)info.GetValue("ElapsedTime",  typeof(string));        
    SaveFileInfo.Scene       = (string)info.GetValue("Scene",        typeof(string));
    SaveFileInfo.Index       = (int)   info.GetValue("Index",        typeof(int));
    SaveFileInfo.SaveCount   = (int)   info.GetValue("SaveCount",    typeof(int));        
}
#endregion
    
    //serialiazation method call when save happend
    public void GetObjectData (SerializationInfo info, StreamingContext context){
     
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
}
