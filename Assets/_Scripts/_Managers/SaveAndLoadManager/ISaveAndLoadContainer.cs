using UnityEngine;
using System.Collections;

using System.Runtime.Serialization;

public abstract class ISaveAndLoadContainer : ISerializable  {
 
   public static  object Instance_Save{//Re immplement this property with new keword in derived class.
        get{
            CDebug.LogError(CDebug.eMessageTemplate.NotImplemented);
            return null;
        }
    }
   public static  object Instance_Load{//Re implement this property with new keword in derived class.
        get{
            CDebug.LogError(CDebug.eMessageTemplate.NotImplemented);
            return null;
        }
        set{
           CDebug.LogError(CDebug.eMessageTemplate.NotImplemented); 
        }
    }
   
   public ISaveAndLoadContainer(){}
   public ISaveAndLoadContainer(SerializationInfo info, StreamingContext context){}
    
    //Serialization method. Override this method in derived function.
   public abstract void GetObjectData (SerializationInfo info, StreamingContext context);

    
}
