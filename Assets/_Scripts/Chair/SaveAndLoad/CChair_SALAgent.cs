using UnityEngine;
using System.Collections.Generic;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#pragma warning disable 0414 
[System.Serializable]
public class CChair_SALAgent : MonoBehaviour,ISaveAndLoadAgent,ISerializable {
	
	private static CChair_SALAgent  Instance;
	private 	   CChair_SALAgent _instance;//temp
	private        List<ISaveAndloadClient> _clientsInstances;
    private        GameObject               _parent;
	public         int var1;
	public         int var2;
	
	public List<ISaveAndloadClient> ClientsInstances {
		get {
			return this._clientsInstances;
		}
	}
	
	public      CChair_SALAgent(){
		
	}
	
	void        Awake(){
    
        _parent = gameObject;//no use currently     
		_clientsInstances = new List<ISaveAndloadClient>();//just init.     
        _instance = null;//stop warrning
	}
	
	void        Start()
	{
		
        //CSaveAndLoadManager.Instance.RegisterAgent((ISaveAndLoadAgent)this);
        _clientsInstances.Add((ISaveAndloadClient)CChair.Instance);
        
		
	}
    
	public      CChair_SALAgent(SerializationInfo info,StreamingContext context)
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
	        _instance = (CChair_SALAgent)bFormatter.Deserialize(s);
		}
		return true;
		
	}
}
