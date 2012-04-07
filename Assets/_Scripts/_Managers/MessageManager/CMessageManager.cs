using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
///  message manager.
/// </summary>
 public static  class  CMessageManager  {
    
#region Types
    
    public delegate void MessageDelegate(CMessages.eMessages m,object data);
    
#endregion
    
#region Public Fields
    public  static MessageDelegate       MessageEvent;
#endregion
    
#region Private Fields
    
    private static List<IMessageObsever> _observers;// list of observers.used by NotifyObservers().
	private static CMessages.eMessages   _message;  // message itself.
	private static object                _data;     // message's data.	
    
#endregion

#region Conusructors
    
    static  CMessageManager(){
        
        _observers = new List<IMessageObsever>();        
        
    }
    
#endregion
	
#region Public Methods
    
	/// <summary>
	/// Registers the observer.
	/// </summary>
	/// <param name='o'>
	/// o object will recive messages.
	/// </param>
	public static void RegisterObserver(IMessageObsever o)
	{
		if (_observers.Contains(o) == false)
			_observers.Add(o);
		
	}
    
    public static void RegisterObserver(MessageDelegate o)
    {
        
        MessageEvent += o;
    }	
    
	/// <summary>
	/// Removes the observer.
	/// </summary>
	/// <param name='o'>
	/// object that do not want to receive messages.
	/// </param>
	public static bool RemoveObserver(IMessageObsever o){
		
		if (_observers.Count > 0)
			return _observers.Remove(o);
		return true;
	}
    
    public static void RemoveObserver(MessageDelegate o){
     
        MessageEvent -= o;
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
	public static void SendMessage(CMessages.eMessages m,object data)
	{
		_message = m;
		_data    = data;
		NotifyObsevers();
	}
	
#endregion
 
#region Private Methods    
    
/// <summary>
 /// Notifies the obsevers.
 /// This function called when message was sent.
 /// </summary>
 private static void NotifyObsevers(){
        
     MessageEvent(_message, _data);
     for(int i = 0; i < _observers.Count ; i++)
     {
         _observers[i].OnMessage(_message,_data);
     }
        
 }
    
#endregion    
    
}
