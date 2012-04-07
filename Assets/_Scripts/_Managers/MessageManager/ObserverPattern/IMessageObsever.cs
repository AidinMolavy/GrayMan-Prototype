using UnityEngine;
using System.Collections;
using System;
public interface IMessageObsever {
	

	 void  OnMessage(CMessages.eMessages m,object data);
     
        
}
