using UnityEngine;
using System.Collections;

public class CManagers : MonoBehaviour {

	// Use this for initialization
    public static CMessageManager     CMessageManagerInstance;
    public static CSaveAndLoadManager CSaveAndLoadManagerInstance;
    
    // All instance that initialized here just can be use in Start() and after(in term of calling time).
    //Do not use this instances in Awake().
    void Awake(){
        CSaveAndLoadManagerInstance = 
            (CSaveAndLoadManager)GameObject.FindGameObjectWithTag("SaveAndLoadManager")
            .GetComponent(typeof(CSaveAndLoadManager)); 
        
        CMessageManagerInstance = 
            (CMessageManager)GameObject.FindGameObjectWithTag("SaveAndLoadManager")
            .GetComponent(typeof(CSaveAndLoadManager));     
        
        if (CSaveAndLoadManagerInstance == null || CMessageManagerInstance ==  null)
            Debug.LogError("Somthing is wrong. null refrences.");
    }
	void Start () {
	
	}
	

}
