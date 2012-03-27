using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#pragma warning disable 0414 
public  class _SALAgent : MonoBehaviour,ISaveAndLoadAgent {

#region Private Fields
    private static ArrayList        _instances = new ArrayList();//Part of singleton system.
    private List<ISaveAndloadClient> _clientsInstances;
#endregion

#region Public Fields

#endregion

#region Properties

    //Implementing Singleton pattern.
    //Every objectes that need access to class functionality most use this property.
    public static _SALAgent Instance{
        get{
            return (_SALAgent)CSingleton.GetSingletonInstance(
                ref _instances,
                typeof(_SALAgent),
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
   	
        //Init variable to prevent null refrences exeption.
        _clientsInstances = new List<ISaveAndloadClient>();
            
    }
    
    void Start(){
        
        // Registr agent to the list of "CSaveAndLoadManager" agents.        
		CSaveAndLoadManager.Instance.RegisterAgent((ISaveAndLoadAgent)this);
        
        //this agent Save its clients.
        //will use for "OnSave()" and "OnLoad()" calling from "CSaveAndLoadManager"        
        //_clientsInstances.Add((ISaveAndloadClient));

        
    }  
#endregion

#region ISaveAndLoadAgent Implementation
    public bool SaveToFile   (ref Stream s, CSaveAndLoadTypes.eFormatters format){
        
         return false;
     }

    public bool LoadFromFile (ref Stream s, CSaveAndLoadTypes.eFormatters format){
     
        return false;
    }
#endregion

}