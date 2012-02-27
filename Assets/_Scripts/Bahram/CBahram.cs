using UnityEngine;
using System.Collections;
using System;
public class CBahram : CiaObject{
	
	public static CBahram Instance;
	
	CBahram()
	{
		Instance = this;
	}
	
	#region Mono Behavior
		void Awake()
		{
			
		}
		
		void Start()
		{
		
			SetState(STT_Public_Enable.Instance);
			SetState(STT_Public_Enable.Instance);
		    SetState(STT_Public_Disable.Instance);

			ExecuteState();
		CMessageManager.Instance.SendMessage(CMessages.eMessages.EnableAll,"enable + action press");
			 SetState(STT_Public_Disable.Instance);
		    ExecuteState();
			CMessageManager.Instance.SendMessage(CMessages.eMessages.ActionPressed,"enable + action press");
			

		}
	#endregion	
	
	public bool RegisterState(IState s)
	{
		if (s == null) return false;
		States.Add(s);
		return true;
	}

}
