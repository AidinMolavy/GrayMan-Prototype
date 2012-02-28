using UnityEngine;
using System.Collections;

public class STT_CChair_Second : MonoBehaviour,IState,IMessageObsever {
	
	static public STT_CChair_Second Instance;
	
	STT_CChair_Second()
	{

		Instance = this;
	}
	
	void Awake()
	{
		CChair.Instance.RegisterState(this);
	}
	public int Execute ()
	{
		print ("STT_CChair_Second Executed.");
		return 1;
	}
	public void MessageReceived (CMessages.eMessages m, object data)
	{
		if(m == CMessages.eMessages.ChairActive)
		{
			print("STT_CChair_Second state is activated by chariActive and done its job.");
		}
	}
}