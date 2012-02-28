using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
///  message manager.
///  must have just one instace in entire program.
///  can not be a base class.
/// </summary>
 public sealed class  CMessageManager : MonoBehaviour,IMessageSubject {
	
	public static CMessageManager Instance;  //class instance.through this can access to public methods.
	
    private List<IMessageObsever> _observers;// list of observers.used by NotifyObservers().
	private CMessages.eMessages   _message;  // message itself.
	private object                _data;     // message's data.	
	
	CMessageManager()
	{
		Instance = this;
		_observers = new List<IMessageObsever>();
		
	}
	
	void Awake()
	{
		
	}
	
	void Start()
	{
		//just for testing
		CChair.Instance.SetState(STT_CChair_Second.Instance);
		CChair.Instance.ExecuteState();
		CMessageManager.Instance.SendMessage(CMessages.eMessages.ChairActive,null);
		CChair.Instance.SetState(STT_CChair_Second.Instance);
		CChair.Instance.ExecuteState();
		CMessageManager.Instance.SendMessage(CMessages.eMessages.ChairActive,null);
	}
	
	/// <summary>
	/// Registers the observer.
	/// </summary>
	/// <param name='o'>
	/// o object will recive messages.
	/// </param>
	public void RegisterObserver(IMessageObsever o)
	{
		if (_observers.Contains(o) == false)
			_observers.Add(o);
		
	}
	
	/// <summary>
	/// Removes the observer.
	/// </summary>
	/// <param name='o'>
	/// object that do not want to receive messages.
	/// </param>
	public bool RemoveObserver(IMessageObsever o)
	{
		if (_observers.Count > 0)
			return _observers.Remove(o);
		return true;
	}
	
	/// <summary>
	/// Notifies the obsevers.
	/// This function called when message was sent.
	/// must be private.
	/// </summary>
	public void NotifyObsevers()
	{
		
		for(int i = 0; i < _observers.Count ; i++)
		{
			_observers[i].MessageReceived(_message,_data);
		}
	}
	
	/// <summary>
	/// Sends the message to all observers.
	/// </summary>
	/// <param name='m'>
	/// Message
	/// </param>
	/// <param name='data'>
	/// Data that message carry.
	/// In case message has no data set it to null.
	/// </param>
	public void SendMessage(CMessages.eMessages m,object data)
	{
		_message = m;
		_data    = data;
		NotifyObsevers();
	}
	
	

}
