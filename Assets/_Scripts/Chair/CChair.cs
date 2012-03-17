using UnityEngine;
using System.Collections;

public class CChair : CiaObject,ISaveAndloadClient {

	// Use this for initialization
	public static CChair Instance;
	//private CChair_SALAgent salAgent;
	public int var1,var2;
		
	void Awake(){
		
		
		var1 = 3;
		var2 = 4;
        Instance = this;

        
	}
	public void OnSave ()
	{
		print("CChair is notifyed about save.");
	}

	public void OnLoad ()
	{
		print("CChair is notifyed about Load.");
		
	}

}
