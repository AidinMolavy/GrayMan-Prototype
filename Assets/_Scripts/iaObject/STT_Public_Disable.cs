using UnityEngine;
using System.Collections;

public class STT_Public_Disable : MonoBehaviour,IState,IMessageObsever {
	
	public static STT_Public_Disable Instance;

	
	STT_Public_Disable()
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
		return 1;
	}
	
	public void MessageReceived(CMessages.eMessages m,object data)
	{
		if (m == CMessages.eMessages.ActionPressed )
		{
			print("Disable state receive Action pressed message");
		}
	}
}