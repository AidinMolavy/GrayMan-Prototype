using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;


[System.Serializable]
public class CBahram_Save : ISerializable  {
	
	
	public  int var1 ;
	public  int var2 ;
	public static CBahram_Save Instance;
	public CBahram_Save()
	{
		Instance = this;
	}
	
	public CBahram_Save(SerializationInfo info,StreamingContext context)
	{
		var1 = (int)info.GetValue("var1",typeof(int));
		var2 = (int)info.GetValue("var2",typeof(int));
		CBahram.Instance.var1 = var1;
		CBahram.Instance.var2 = var2;
	}
	
	
	public void GetObjectData (SerializationInfo info, StreamingContext context)
	{
		var1 = CBahram.Instance.var1;
		var2 = CBahram.Instance.var2;
		info.AddValue("var1",var1);
		info.AddValue("var2",var2);
	}
	
}
