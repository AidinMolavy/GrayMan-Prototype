using UnityEngine;
using System.Collections;

public class CChair : CiaObject {

	// Use this for initialization
	public static CChair Instance;
	public int var1,var2;
	
	private CChair_SAL _chair_sal;
	
	CChair()
	{
		var1 = 3;
		var2 = 4;
		Instance = this;
		_chair_sal = new CChair_SAL();
	}
	
	void Awake()
	{
		
	}

}
