using UnityEngine;
using System.Collections.Generic;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#pragma warning disable 0414 
[System.Serializable]
public class CBahram_SALAgent : MonoBehaviour,ISaveAndLoadAgent ,ISerializable {
	
	
    private CBahram_SALAgent _instance;
    private List<ISaveAndloadClient> _clientsInstances;
	public  int var1 ;
	public  int var2 ;
    public  int var3 ;
    public  int var4 ;
    
    public List<ISaveAndloadClient> ClientsInstances {
        get {
            return _clientsInstances;
        }
    }
    
	public CBahram_SALAgent()
	{
		
	}
    
	public CBahram_SALAgent(SerializationInfo info,StreamingContext context)
	{
		var1 = (int)info.GetValue("var1",typeof(int));
		var2 = (int)info.GetValue("var2",typeof(int));
        var3 = (int)info.GetValue("var3",typeof(int));
        var4 = (int)info.GetValue("var4",typeof(int));
        
		CBahram.Instance.var1 = var1;
		CBahram.Instance.var2 = var2;
        CBahram_Temp.Instance.var1 =  var3;
        CBahram_Temp.Instance.var2 =  var4;
	}
	
    void Awake(){
        
      
        _clientsInstances = new List<ISaveAndloadClient>();
            
    }
    
    void Start(){
        
        
        //Registr this agent to the list of "CSaveAndLoadManager" agents.        
        //CSaveAndLoadManager.Instance.RegisterAgent((ISaveAndLoadAgent)this);
        
        //this agent Save its clients.
        //will use for "OnSave()" and "OnLoad()" calling from "CSaveAndLoadManager"        
        _clientsInstances.Add((ISaveAndloadClient)CBahram.Instance);//add CBahram
        _clientsInstances.Add((ISaveAndloadClient)CBahram_Temp.Instance);//add CBahram_Temp
        
    }       
    
	public void GetObjectData (SerializationInfo info, StreamingContext context)
	{
		var1 = CBahram.Instance.var1;
		var2 = CBahram.Instance.var2;
        var3 = CBahram_Temp.Instance.var1;
        var4 = CBahram_Temp.Instance.var2;
        
		info.AddValue("var1",var1);
		info.AddValue("var2",var2);
        info.AddValue("var3",var3);
        info.AddValue("var4",var4);        
	}
	
    public bool SaveToFile (ref System.IO.Stream s,CSaveAndLoadTypes.eFormatters format){
        
         BinaryFormatter bFormatter;
         if (s == null) {Debug.LogError("Null refrence."); return false;}
         if (format == CSaveAndLoadTypes.eFormatters.Binary){       
             bFormatter = new BinaryFormatter();
             bFormatter.Serialize(s,this);
         }       
         return true;
     }

    public bool LoadFromFile (ref Stream s, CSaveAndLoadTypes.eFormatters format){
     
        BinaryFormatter bFormatter;
        if (format== CSaveAndLoadTypes.eFormatters.Binary){        
             bFormatter = new BinaryFormatter();
            _instance = (CBahram_SALAgent)bFormatter.Deserialize(s);
        }
        return true;
     
    }
}

