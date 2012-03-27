using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#pragma warning disable 0414 
public  class CBahram_SALAgent : MonoBehaviour,ISaveAndLoadAgent {
	
#region Private Fields
    private static ArrayList        _instances = new ArrayList();//Part of singleton system.
    private List<ISaveAndloadClient> _clientsInstances;
#endregion
    
#region Public Fields   
    
	public  int var1 ;
	public  int var2 ;
    public  int var3 ;
    public  int var4 ;
    
#endregion 
    

#region Properties
    
    //Implementing Singleton pattern.
    //Every objectes that need access to class functionality most use this property.
    public static CBahram_SALAgent Instance{
        get{
            return (CBahram_SALAgent)CSingleton.GetSingletonInstance(
                ref _instances,
                typeof(CBahram_SALAgent),
                CGlobalInfo.stSaveAndLoad.TagName,
                CGlobalInfo.stSaveAndLoad.GameObjectName);

        }
    }
    
    public List<ISaveAndloadClient> ClientsInstances {
        get {
            return _clientsInstances;
        }
    }
    
#endregion  
    
#region MonoBehaviour
	
    void Awake(){
        
        //singleton functionality
        _instances.Add(this) ;
        CSingleton.DestroyExtraInstances(_instances);          
      
        _clientsInstances = new List<ISaveAndloadClient>();
            
    }
    
    void Start(){
        
        
       // Registr this agent to the list of "CSaveAndLoadManager" agents.        
        CSaveAndLoadManager.Instance.RegisterAgent((ISaveAndLoadAgent)this);
        
        //this agent Save its clients.
        //will use for "OnSave()" and "OnLoad()" calling from "CSaveAndLoadManager"        
        _clientsInstances.Add((ISaveAndloadClient)CBahram.Instance);//add CBahram
        _clientsInstances.Add((ISaveAndloadClient)CBahram_Temp.Instance);//add CBahram_Temp
        
    }  
    
#endregion
    
#region ISaveAndLoadAgent Implementation    
    
    public bool SaveToFile (ref System.IO.Stream s,CSaveAndLoadTypes.eFormatters format){
        
         BinaryFormatter bFormatter;
         if (s == null) {CDebug.LogError(CDebug.eMessageTemplate.NullRefrences); return false;}
         if (format == CSaveAndLoadTypes.eFormatters.Binary){       
             bFormatter = new BinaryFormatter();
             bFormatter.Serialize(s,CBahram_SALContainer.Instance_Save);
         }       
         return true;
     }

    public bool LoadFromFile (ref Stream s, CSaveAndLoadTypes.eFormatters format){
     
        BinaryFormatter bFormatter;
        if (format== CSaveAndLoadTypes.eFormatters.Binary){        
             bFormatter = new BinaryFormatter();
            CBahram_SALContainer.Instance_Load = (CBahram_SALContainer)bFormatter.Deserialize(s);
        }
        return true;
     
    }
    
#endregion    
}

