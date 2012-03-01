using UnityEngine;
using System.Collections;

public class CBahram_STT_Second : MonoBehaviour,IState,IMessageObsever {


	public static CBahram_STT_Second Instance;
	
	CBahram_STT_Second()
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
