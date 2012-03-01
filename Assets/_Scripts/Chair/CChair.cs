using UnityEngine;
using System.Collections;

public class CChair : CiaObject {

	// Use this for initialization
	public static CChair Instance;
	
	public int var3,var4;
	
	CChair()
	{
		var3 = 3;
		var4 = 4;
		Instance = this;
	}

}
