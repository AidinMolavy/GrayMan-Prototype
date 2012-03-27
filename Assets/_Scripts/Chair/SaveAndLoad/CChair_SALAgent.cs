using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#pragma warning disable 0414 
[System.Serializable]
public  class CChair_SALAgent : MonoBehaviour,ISaveAndLoadAgent {
	
#region Private Fields

    private static ArrayList                _instances = new ArrayList();//Part of singleton system.
    private        List<ISaveAndloadClient> _clientsInstances;    
#endregion

#region Public Fields
    
	public         int var1;
	public         int var2;
    
#endregion

#region Properties
    
    public static CChair_SALAgent Instance{
        get{
            return (CChair_SALAgent)CSingleton.GetSingletonInstance(
                ref _instances,
                typeof(CChair_SALAgent),
                CGlobalInfo.stSaveAndLoad.TagName,
                CGlobalInfo.stSaveAndLoad.GameObjectName);

        }
    } 
	public List<ISaveAndloadClient> ClientsInstances {
		get {
			return this._clientsInstances;
		}
	}
    
#endregion	
    
#region MonoBehaviour
    
	void        Awake(){
        

        //singleton functionality
        _instances.Add(this) ;
        CSingleton.DestroyExtraInstances(_instances);  
            
		_clientsInstances = new List<ISaveAndloadClient>();//just init.     
	}
	
	void        Start()
	{
		
        CSaveAndLoadManager.Instance.RegisterAgent((ISaveAndLoadAgent)this);
        _clientsInstances.Add((ISaveAndloadClient)CChair.Instance);
        
		
	}
    
#endregion  	

#region ISaveAndLoadAgent Implementation
    
	public bool SaveToFile (ref System.IO.Stream s,CSaveAndLoadTypes.eFormatters format)
	{
		BinaryFormatter bFormatter;
		//if (s== null) return false; //need debug
		if (format== CSaveAndLoadTypes.eFormatters.Binary){
			
			bFormatter = new BinaryFormatter();
			bFormatter.Serialize(s,CChair_SALContainer.Instance_Save);
		}
		
		return true;
	}

	public bool LoadFromFile (ref Stream s, CSaveAndLoadTypes.eFormatters format){
		
		BinaryFormatter bFormatter;
		if (format== CSaveAndLoadTypes.eFormatters.Binary){
			
			bFormatter = new BinaryFormatter();
	        CChair_SALContainer.Instance_Load = (CChair_SALContainer)bFormatter.Deserialize(s);
		}
		return true;
		
	}
    
#endregion
}
