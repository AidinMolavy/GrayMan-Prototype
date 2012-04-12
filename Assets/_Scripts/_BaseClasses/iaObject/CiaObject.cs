using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Base class for all interactive objects.
/// </summary>
public abstract class CiaObject : MonoBehaviour {
    
#region Fields
    
	private    IState       _currnetState;
	private    IState       _preState;
    private    bool         _updateCurrnetState;//specify that current state need to be update or not.
    protected  List<IState> States;//list of all states that sub class created.
    
#endregion

#region MonoBehaviour
    
    void Awake(){
        
        States = new List<IState>();    
        _updateCurrnetState = false;//"GoToState()" function manage this value.
    }
    
    void Update(){
        
        if(_updateCurrnetState)
            _currnetState.OnUpdate();
    }
    
#endregion
    
#region Public Methods    
	
	public    bool   GoToState(ref IState state)
	{
        if(state != null){//Return false if "state" is null.
            if(_currnetState == state) return true;//Allready is in state. so do nothing and return true;
            _updateCurrnetState = false;//Do not call "OnUpdate()" event on current state anymore.
            if(_currnetState != null)//Do not call "OnExit()" event when no previous state is exist.
                StartCoroutine(_currnetState.OnExit());            
    		_preState = _currnetState;
    		_currnetState = state;
            StartCoroutine(_currnetState.OnBegin());
            _updateCurrnetState = true;//Current state changed and need to start "OnUpdate()" envent again.;
            return true;
        }
        CDebug.LogError("Can not change state.");
		return false;
	}
	
	public    IState GetCurrentState()
	{
		
		return _currnetState;
		
	}
	
	public    IState GetPreState()
	{
		
		return _preState;
		
	}
	
#endregion
    
}