using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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
		
		
		CBahram.Instance.var1 = 100;
		CBahram.Instance.var2 = 200;
		CChair.Instance.var1 = 300;
		CChair.Instance.var2 = 400;
		
		//Save 		
		Stream stream =  File.Open("Save",FileMode.Create);
		BinaryFormatter bFormatter = new BinaryFormatter();
		bFormatter.Serialize(stream,CBahram_Save.Instance);
		bFormatter.Serialize(stream,CChair_SAL.Instance);
		stream.Close();
		
		CBahram.Instance.var1 = 5;
		CBahram.Instance.var2 = 6;
		CChair.Instance.var1  = 7;
		CChair.Instance.var2  = 8;		
		
		//Load
		stream = File.Open("Save",FileMode.Open);
		bFormatter = new BinaryFormatter();
		CBahram_Save.Instance =  (CBahram_Save)bFormatter.Deserialize(stream);
		CChair_SAL.Instance =  (CChair_SAL)bFormatter.Deserialize(stream);
		stream.Close();
		
		//print
		print(CBahram.Instance.var1 + "    " + CBahram.Instance.var2);
		print(CChair.Instance.var1 + "    " + CChair.Instance.var2);
		
		
		
		CBahram.Instance.var1 = 10;
		CBahram.Instance.var2 = 20;
		CChair.Instance.var1 = 30;
		CChair.Instance.var2 = 40;
		
		
		//Save 2
	    stream =  File.Open("Save",FileMode.Create);
	    bFormatter = new BinaryFormatter();
		bFormatter.Serialize(stream,CBahram_Save.Instance);
		bFormatter.Serialize(stream,CChair_SAL.Instance);
		stream.Close();

		CBahram.Instance.var1 = 5;
		CBahram.Instance.var2 = 6;
		CChair.Instance.var1  = 7;
		CChair.Instance.var2  = 8;		

		
		//Load 2
		stream = File.Open("Save",FileMode.Open);
		bFormatter = new BinaryFormatter();
		CBahram_Save.Instance =  (CBahram_Save)bFormatter.Deserialize(stream);
		CChair_SAL.Instance =  (CChair_SAL)bFormatter.Deserialize(stream);
		stream.Close();
		
		
		//print
		print(CBahram.Instance.var1 + "    " + CBahram.Instance.var2);
		print(CChair.Instance.var1 + "    " + CChair.Instance.var2);
		
		
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
