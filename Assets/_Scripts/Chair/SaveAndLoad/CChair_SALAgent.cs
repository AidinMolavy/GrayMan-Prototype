using UnityEngine;
using System.Collections.Generic;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class CChair_SALAgent : MonoBehaviour, ISerializable,ISaveAndLoadAgent {
	
	private static CChair_SALAgent  Instance;
	private 	   CChair_SALAgent _instance;//temp
	private        List<ISaveAndloadClient> _clientInstance;
	public int var1;
	public int var2;
	
	public List<ISaveAndloadClient> ClientInstance {
		get {
			return this._clientInstance;
		}
	}
	
	public CChair_SALAgent(){
		
	}
	
	void Awake()
	{
		_clientInstance = new List<ISaveAndloadClient>();
		
	}
	
	void Start()
	{
		_clientInstance.Add((ISaveAndloadClient)CChair.Instance); //Add clients instances.
		CSaveAndLoadManager.Instance.RegisterAgent(this);
	}
	public CChair_SALAgent(SerializationInfo info,StreamingContext context)
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
	
	public bool SaveToFile (ref System.IO.Stream s,CSaveAndLoadTypes.eFormatters format)
	{
		BinaryFormatter bFormatter;
		//if (s== null) return false; //need debug
		if (format== CSaveAndLoadTypes.eFormatters.Binary){
			
			bFormatter = new BinaryFormatter();
			bFormatter.Serialize(s,this);
		}
		
		return true;
	}

	public bool LoadFromFile (ref Stream s, CSaveAndLoadTypes.eFormatters format){
		
		BinaryFormatter bFormatter;
		if (format== CSaveAndLoadTypes.eFormatters.Binary){
			
			bFormatter = new BinaryFormatter();
	    	_instance =  (CChair_SALAgent)bFormatter.Deserialize(s);
		}
		return true;
		
	}
}
