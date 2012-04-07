using UnityEngine;
using System.Collections;

public interface IState {
       
	
    /// <summary>
    /// This is the first event that will call when state start.
    /// </summary>
    IEnumerator OnBegin();
    
    /// <summary>
    /// OnUpdate is called every frame.
    /// It is just like the "Update" of "MonoBehaviour".
    /// </summary>
    void OnUpdate();
   
    IEnumerator OnExit();
    
    /// <summary>
    /// When one state done all its dusties this event will raise.
    /// </summary>
    IEnumerator OnEnd();
    
}
