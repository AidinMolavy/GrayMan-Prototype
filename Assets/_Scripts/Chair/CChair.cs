using UnityEngine;
using System.Collections;

public class CChair : CiaObject,ISaveAndloadClient {

	// Use this for initialization
	public static CChair Instance;
	//private CChair_SALAgent salAgent;
	public int var1,var2;
    public string[] str1;
		
	void Awake(){
		
		
		var1 = 3;
		var2 = 4;
        str1 = new string[3];
        str1[0] = "chari1";
        str1[1] = "chari2";
        str1[2] = "chari3";
        Instance = this;

        
	}
    
	public void OnSave ()
	{
        CChair_SALContainer.Instance_Save.var1 = var1;
        CChair_SALContainer.Instance_Save.var2 = var2;
		print("CChair is notifyed about save.");
	}

	public void OnLoad ()
	{
        var1 =  CChair_SALContainer.Instance_Load.var1;
        var2 =  CChair_SALContainer.Instance_Load.var2;
        
		print("CChair is notifyed about Load.");
        print ("CChair data values are : var1 : " + var1 + " var2 : " + var2);

	}

}
