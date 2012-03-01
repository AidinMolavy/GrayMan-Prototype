using UnityEngine;
using System.Collections;
using System;
public class CBahram : CiaObject{
	
	public static CBahram Instance;
	
	public int var1,var2;
	
	CBahram()
	{
		var1 = 1;
		var2 = 2;
		Instance = this;
	}
	
	#region Mono Behavior
		void Awake()
		{
			
		}
		
		void Start()
		{
			

		}
	#endregion	
	


}
