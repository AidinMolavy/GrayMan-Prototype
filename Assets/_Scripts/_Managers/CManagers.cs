using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CManagers : MonoBehaviour {

	// Use this for initialization
    //public static CMessageManager     CMessageManagerInstance;
    public static CSaveAndLoadManager CSaveAndLoadManagerInstance;
    
    // All instance that initialized here just can be use in Start() and after(in term of calling time).
    //Do not use this instances in Awake().
    void Awake(){

        DontDestroyOnLoad(this);
    }
	IEnumerator Start () {
      
	  yield return StartCoroutine(JustWait(1.0f));//wait unitl other Start() functions done.
      
      
      string scene = CSaveAndLoadManager.Instance.CurrentSave.fileInfo.Scene;//get scene name of saved file
      AsyncOperation op = Application.LoadLevelAsync(scene);//load save's file scene
      yield return op;//Wait until scene load complete.
      if(op.isDone)
        CSaveAndLoadManager.Instance.Load(CSaveAndLoadManager.Instance.CurrentSave);
	}
    
    private IEnumerator JustWait(float time){
        
        yield return new WaitForSeconds(time);
        
    }      
	

}
