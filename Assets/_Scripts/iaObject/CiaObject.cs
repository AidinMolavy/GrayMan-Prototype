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
	
	protected int    ExecuteState()
	{
		int retVal = 0;
		//debug need null refrence
    	retVal =  _state.Execute();
		EnableState(_state);
		return retVal;
	}
	
	protected bool   SetState(IState state)
	{
		_preState = _state;
		_state = state;
		if (DisableStates() == false)
			return false;
		return true;
	}
	
	protected IState GetState()
	{
		
		return _state;
		
	}
	
	protected IState GetPreState()
	{
		
		return _preState;
		
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
			CMessageManager.Instance.RegisterObserver((IMessageObsever)s);
		}
	#endregion
}
