using UnityEngine;
using System.Collections;

using System;
using System.Security;

using System.IO;
using System.Runtime.Serialization;

using stSaveFileInfo = CSaveAndLoadTypes.stSaveFileInfo;

[System.Serializable]
public  class CBahram_SALContainer : ISaveAndLoadContainer  {
 
#region Private fileds    
    private static ArrayList _instances = new ArrayList();
    private static CBahram_SALContainer _instance_load;
#endregion
    
#region Public Fields
    public int var1,var2,var3,var4;
#endregion
    
#region Properties
    public new static CBahram_SALContainer Instance_Save{
        get{
            return (CBahram_SALContainer)CSingleton.GetSingletonInstance(ref _instances, typeof(CBahram_SALContainer));
        }
    } 
    public new static CBahram_SALContainer Instance_Load{
        get{
            return _instance_load;
        }
        set{
            _instance_load = value;
        }
    } 
#endregion
    
#region Constructors
    
    public CBahram_SALContainer(){  
        
        //singleton system
        _instances.Add(this);
        CSingleton.DestroyExtraInstances(_instances); 
        
        var1 = -1;
        var2 = -2;
        var3 = -3;
        var4 = -4;
        
    }
    
    //Serializatoin counstructor. call when Deserialize function called.
    public CBahram_SALContainer(SerializationInfo info, StreamingContext context){
        
        var1 = (int)info.GetValue("var1",typeof(int));
        var2 = (int)info.GetValue("var2",typeof(int));
        var3 = (int)info.GetValue("var3",typeof(int));        
        var4 = (int)info.GetValue("var4",typeof(int));
        
    }
    
#endregion    
    
#region Public Methods
    
    //This method will call when save event happend.
    //All data that need to be save must store here.
    public override void GetObjectData (SerializationInfo info, StreamingContext context){
        var1 = CBahram.Instance.var1 ;
        var2 = CBahram.Instance.var2;
        var3 = CBahram_Temp.Instance.var1;
        var4 = CBahram_Temp.Instance.var2;
        
        info.AddValue("var1",(int)var1);
        info.AddValue("var2",(int)var2);
        info.AddValue("var3",(int)var3);        
        info.AddValue("var4",(int)var4);
    }
    
#endregion
    
}
