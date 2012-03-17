using UnityEngine;
using System.Collections;

#pragma warning disable 0414 

public class CBahram : CiaObject,ISaveAndloadClient{
	
	public static CBahram Instance;
	
	public int var1,var2;

    #region MonoBehavior
     void Awake()
     {
        var1 = 1;
        var2 = 2;
        Instance = this;         
     }
     
     void Start()
     {
         

     }
    #endregion

    #region Save And Load
    public void OnSave ()
    {
        print("CBahram notify about save.");
    }

    public void OnLoad ()
    {
        print("CBahram notify about load.");

    }
    #endregion

	


}
