using UnityEngine;
using System.Collections;

public class CChair_STT_Second : MonoBehaviour,IState,IMessageObsever {
	
	static public CChair_STT_Second Instance;
	
	CChair_STT_Second()
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
	
	void OnPostRender()
	{
		print ("post render.");
	}
}