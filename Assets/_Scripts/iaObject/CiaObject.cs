using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class CiaObject : MonoBehaviour {
	#region Fields
	private IState _state;
	private IState _preState;
    protected  List<IState> States;
	#endregion
	
	protected        CiaObject()
	{
		States = new List<IState>();	
	}
	
	public    int    ExecuteState()
	{
		int retVal = 0;
		//debug need. null refrence
    	retVal =  _state.Execute();
		EnableState(_state);
		return retVal;
	}
	
	public    bool   SetState(IState state)
	{
		_preState = _state;
		_state = state;
		if (DisableStates() == false)
			return false;
		return true;
	}
	
	public    IState GetState()
	{
		
		return _state;
		
	}
	
	public    IState GetPreState()
	{
		
		return _preState;
		
	}
	
	public    bool   RegisterState(IState s)
	{
		if (s == null) return false;
		States.Add(s);
		return true;
	}
	
	#region Private Methods
		/// <summary>
		/// Prevent all states to receive.
		/// </summary>
		/// <returns>
		/// Return false if can not disable at least one state.
		/// </returns>
		private bool DisableStates()
		{
			//disable message event	
			if (_preState != null)
				if(_preState is IMessageObsever)// If "s" is not subscribe for messages then dont add it to observer list
					CMessageManager.Instance.RemoveObserver((IMessageObsever)_preState); //debug need

			return true;
		}
	    /// <summary>
	    /// Enables the state to receive events.
	    /// </summary>
	    /// <param name='s'>
	    /// Estate to be Enable.
	    /// </param>
		private void EnableState(IState s)
		{	
			//Enable message event
			if (s is IMessageObsever)// If "s" is not subscribe for messages then dont add it to observer list
				CMessageManager.Instance.RegisterObserver((IMessageObsever)s);
		}
	#endregion
}
