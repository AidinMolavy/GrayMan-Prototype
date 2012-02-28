using UnityEngine;
using System.Collections;

public class STT_Public_Enable : MonoBehaviour,IState,IMessageObsever {


	public static STT_Public_Enable Instance;
	
	STT_Public_Enable()
	{
		
		Instance = this;
	}
	
	void Awake()
	{
		//register itself to owner class
		CBahram.Instance.RegisterState(this);
	}
	
	public int  Execute()
	{
		//CMessageManager.Instance.SendMessage(CMessages.eMessages.ChairActive,null);
		return 1;
	}
	
	public void MessageReceived(CMessages.eMessages m,object data)
	{
		if (m == CMessages.eMessages.ActionPressed )
		{
			print("Enable state receive Action pressed message");
		}
	}

}
