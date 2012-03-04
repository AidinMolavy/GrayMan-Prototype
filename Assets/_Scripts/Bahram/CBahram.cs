using UnityEngine;
using System.Collections;

#pragma warning disable 0414 

public class CBahram : CiaObject{
	
	public static CBahram Instance;
	
	public int var1,var2;
	private CBahram_Save _bahramSave;//no use. just for creating instance;
	CBahram()
	{
		var1 = 1;
		var2 = 2;
		Instance = this;
		
	    _bahramSave = new CBahram_Save();//
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
