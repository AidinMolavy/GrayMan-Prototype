using UnityEngine;
using System.Collections;

using System;
using System.Security;
using System.Security.Permissions;

using System.IO;
using System.Runtime.Serialization;

using stSaveFileInfo = CSaveAndLoadTypes.stSaveFileInfo;

[System.Serializable]
public sealed  class CChair_SALContainer : ISaveAndLoadContainer  {
 
#region Private fileds    
    private static ArrayList _instances = new ArrayList();
    private static CChair_SALContainer _instance_load;
#endregion
    
#region Public Fields
    public int var1,var2;
    public string[] str1;
#endregion
    
#region Properties
    public new static  CChair_SALContainer Instance_Save{
        get{
            return (CChair_SALContainer)CSingleton.GetSingletonInstance(
                ref _instances,
                typeof(CChair_SALContainer));
        }
    } 
    public new static  CChair_SALContainer Instance_Load{
        get{
            return _instance_load;
        }
        set{
            _instance_load = value;
        }
    }
    
#endregion
    
#region Constructors
    
    public CChair_SALContainer() : base(){
        
        //singleton system
        _instances.Add(this);
        CSingleton.DestroyExtraInstances(_instances);     
        
        var1 = -1;
        var2 = -2;
        str1= new string[3];
        str1[0]= "nothing";
        str1[1]= "nothing";
        str1[2]= "nothing";
    }
    
    //Serializatoin counstructor. call when Deserialize function called.
    private CChair_SALContainer(SerializationInfo info, StreamingContext context): base(info,context){
        var1 = info.GetInt32("var1");
        var2 = info.GetInt32("var2");
        str1 = (string[])info.GetValue("str1",typeof(string[]));
    }
    
#endregion    
    
#region Public Methods
    
    //This method will call when save event happend.
    //All data that need to be save must store here.
    public override void GetObjectData (SerializationInfo info, StreamingContext context){
        
        var1 = CChair.Instance.var1;
        var2 =  CChair.Instance.var2;
        str1 = CChair.Instance.str1;
        
        info.AddValue("var1",var1);
        info.AddValue("var2",var2);
        info.AddValue("str1",str1,typeof(string[]));
        
           
    }
    
#endregion
    
}
