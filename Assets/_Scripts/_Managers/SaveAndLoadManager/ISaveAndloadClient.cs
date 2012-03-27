using UnityEngine;
using System.Collections;

public interface ISaveAndloadClient {
	
	
	void OnSave();//this event will call just befor save operation start.
	void OnLoad();//this event will call just after load operation finished.

}
