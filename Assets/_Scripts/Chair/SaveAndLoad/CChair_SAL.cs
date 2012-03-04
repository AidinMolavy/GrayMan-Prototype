using UnityEngine;
using System.Collections;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class CChair_SAL : ISerializable,ISaveAndLoad  {
	
	public static CChair_SAL Instance;
	private CChair_SAL _instance;//temp
	public int var1;
	public int var2;
	
	public CChair_SAL()
	{
		var1 = 0;
		var2 = 0;
		Instance = this;
	}
	public CChair_SAL(SerializationInfo info,StreamingContext context)
	{
		var1 = (int)info.GetValue("var1",typeof(int));
		var2 = (int)info.GetValue("var2",typeof(int));
		CChair.Instance.var1 = var1;
		CChair.Instance.var2 = var2;
	}
	
	
	public void GetObjectData (SerializationInfo info, StreamingContext context)
	{
		var1 = CChair.Instance.var1;
		var2 = CChair.Instance.var2;
		info.AddValue("var1",var1);
		info.AddValue("var2",var2);
	}
	
	public bool SaveToFile (ref System.IO.Stream s,ref object formatter)
	{

		if (formatter is BinaryFormatter)
		{
			formatter = new BinaryFormatter();
			formatter.Serialize(s,this);
		}
		return true;
	}

	public bool LoadFromFile (ref Stream s,ref object formatter)
	{
		formatter = new BinaryFormatter();
		_instance =  (CChair_SAL)Formatter.Deserialize();
		
	}
}
