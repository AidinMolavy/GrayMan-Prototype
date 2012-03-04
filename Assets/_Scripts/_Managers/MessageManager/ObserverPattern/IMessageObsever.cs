using UnityEngine;
using System.Collections;
using System;
public interface IMessageObsever {
	

	  void  MessageReceived(CMessages.eMessages m,object data);

}
