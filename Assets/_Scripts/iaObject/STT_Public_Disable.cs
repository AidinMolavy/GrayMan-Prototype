using UnityEngine;
using System.Collections;

public class STT_Public_Disable : MonoBehaviour,IState,IMessageObsever {
	
	public static STT_Public_Disable Instance;
	private bool _actionPress;
	
	STT_Public_Disable()
	{
		_actionPress = false;
		Instance = this;
	}
	
	void Awake()
	{
		//register itself to owner class	
		CBahram.Instance.RegisterState(this);
	}
	
	public int  Execute()
	{
		if (_actionPress)
			print("Disable state is executing...");
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