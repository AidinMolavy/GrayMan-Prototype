using UnityEngine;
using System.Collections;

public class CBahram_STT_First : MonoBehaviour,IState,IMessageObsever {
	
	public static CBahram_STT_First Instance;

	
	CBahram_STT_First()
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