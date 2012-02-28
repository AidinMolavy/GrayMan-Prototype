using UnityEngine;
using System.Collections;

public class STT_CChair_First : MonoBehaviour,IState {
	
	static public STT_CChair_First Instance;
	
	STT_CChair_First()
	{

		Instance = this;
	}
	
	
	void Awake()
	{
		CChair.Instance.RegisterState(this);
	}
	public int Execute()
	{
		//print ("STT_CChair_First activated and done its job.");
		//CBahram.Instance.SetState(STT_Public_Enable.Instance);
		//CChair.Instance.SetState(STT_CChair_Second.Instance);
		//CChair.Instance.ExecuteState();
		//CBahram.Instance.ExecuteState();
		
		print ("STT_CChair_First Executed.");
		return 1;
	}
	
	
}
