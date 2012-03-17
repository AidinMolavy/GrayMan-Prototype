using UnityEngine;
using System.Collections;

public class CBahram_Temp : MonoBehaviour,ISaveAndloadClient {

	// Use this for initialization
    public int var1,var2;
    public static CBahram_Temp Instance;
    
    void Awake(){
        
        var1 = 4;
        var2 = 5;
        Instance = this;
        
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
       public void OnSave (){
        
         print("CBahram_Temp notify from save.");
    }

    public void OnLoad (){
        
        print("CBahram_Temp notify from load.");
    }
}
