using UnityEngine;
using System.Collections;

using System;
using System.Security;
using System.Security.Permissions;

using System.IO;
using System.Runtime.Serialization;

using stSaveFileInfo = CSaveAndLoadTypes.stSaveFileInfo;

[System.Serializable]
public sealed  class _SALContainer : ISaveAndLoadContainer  {
 
#region Private fileds    
    private static ArrayList _instances = new ArrayList();
	private static _SALContainer _instance_load;
#endregion
    
#region Public Fields
    
#endregion
    
#region Properties
    public new static  _SALContainer Instance_Save{
        get{
            return (_SALContainer)CSingleton.GetSingletonInstance(
                ref _instances,
                typeof(_SALContainer)
				);
        }
	}
	public new static  _SALContainer Instance_Load{
		get{
			return _instance_load;
		}
		set{
			_instance_load = value;
		}
	}
     
#endregion
    
#region Constructors
    
    public _SALContainer() base(){
		//singleton system
        _instances.Add(this);
        CSingleton.DestroyExtraInstances(_instances);
    }
    
    //Serializatoin counstructor. call when Deserialize function called.
	//Must be private and call base constructor.
    private _SALContainer(SerializationInfo info, StreamingContext context) : base(info, context){
        
    }
    
#endregion    
    
#region Public Methods
    
    //This method will call when save event happend.
    //All data that need to be save must store here.
    public override void GetObjectData (SerializationInfo info, StreamingContext context){
        throw new System.NotImplementedException ();
    }
    
#endregion
    
}
