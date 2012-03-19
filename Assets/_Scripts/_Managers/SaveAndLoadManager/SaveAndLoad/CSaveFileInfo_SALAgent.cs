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

#pragma warning disable 0414

//programmer and designer : Aidin Molavy Nejad

/// <summary>
/// This class is an agetn of the SaveAndLoad system and has no client.
/// Never instantiate this class. just use "Instance" property.
/// No need to attached to a GameObject.
/// This class contain all data that save file need to present.
/// </summary>
public class CSaveFileInfo_SALAgent : MonoBehaviour,ISaveAndLoadAgent {
    
#region Private fields
    private static ArrayList _instances = new ArrayList();
    private List<ISaveAndloadClient> _clientsInstances;
    #endregion
    
#region Public Fields
    public static CSaveFileInfo_SALContainer Instance_Container;
    #endregion
    
#region Properties
    //This property use for implement Singleton pattern.
    //Every objectes that need access to class functionality most use the property.
    public static CSaveFileInfo_SALAgent Instance{
        get{
            return (CSaveFileInfo_SALAgent)CSingleton.GetSingletonInstance(
                ref _instances,
                typeof(CSaveFileInfo_SALAgent),
                CGlobalInfo.stSaveAndLoad.TagName,
                CGlobalInfo.stSaveAndLoad.GameObjectName);

        }
    }

    public  List<ISaveAndloadClient> ClientsInstances {
        get {
            return _clientsInstances;
        }
    }
#endregion
  
#region MonoBehaviour Events
    
    void Awake(){
        //singleton functionality
        _instances.Add(this) ;
        CSingleton.DestroyExtraInstances(_instances);        
      
        _clientsInstances = new List<ISaveAndloadClient>();        
    }
    
    void Start(){
        CSaveAndLoadManager.Instance.RegisterAgent((ISaveAndLoadAgent)this); 
    }
    
#endregion
    
#region SaveAndLoad Methods

    public bool SaveToFile (ref Stream s, CSaveAndLoadTypes.eFormatters format)
    {
         BinaryFormatter bFormatter;
         if (s == null) {Debug.LogError("Null refrence."); return false;}
         if (format == CSaveAndLoadTypes.eFormatters.Binary){       
             bFormatter = new BinaryFormatter();
             bFormatter.Binder = new CSerialiazatoin.CVersionDeserializationBinder();
             bFormatter.Serialize(s,CSaveFileInfo_SALContainer.Instance);
         }       
         return true;      
    }

    public bool LoadFromFile (ref Stream s, CSaveAndLoadTypes.eFormatters format)
    {
        BinaryFormatter bFormatter;
        try{       
            if (format == CSaveAndLoadTypes.eFormatters.Binary){        
                bFormatter = new BinaryFormatter();
                bFormatter.Binder = new CSerialiazatoin.CVersionDeserializationBinder();
                Instance_Container = (CSaveFileInfo_SALContainer)bFormatter.Deserialize(s);
            }
        }
        catch(SerializationException e){
            
            CDebug.LogExWarning(e.Message);
            return false;
        }
        catch (SecurityException e){
            
            CDebug.LogExError(e.Message);
            return false;
        }
        catch (ArgumentNullException e){
            
            CDebug.LogExError(e.Message);
            return false;            
            
        }
            

        return true;        
        }
    
#endregion
    

}
