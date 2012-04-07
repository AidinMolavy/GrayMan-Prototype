using UnityEngine;
using System.Collections;

#pragma warning disable 0414 

public class CBahram : CiaObject,ISaveAndloadClient{
	
	public static CBahram Instance;
	
	public int var1,var2;
    public IState FirstState;
    public IState SecondState;
    
    #region MonoBehavior
     void Awake()
     {
        FirstState  = new CBahram_STT_First(this);
        SecondState = new CBahram_STT_Second(this);
        var1 = 1;
        var2 = 2;
        Instance = this;         
        
     }
     
     IEnumerator Start()
     {
        GoToState(ref FirstState); 
        GoToState(ref SecondState);
        yield return new WaitForSeconds(1);
        CMessageManager.SendMessage(CMessages.eMessages.ActionPressed,null);
        


     }
    #endregion

    #region Save And Load
    public void OnSave ()
    {
        print("CBahram notify about save.");
    }

    public void OnLoad ()
    {
        var1 = CBahram_SALContainer.Instance_Load.var1;
        var2 = CBahram_SALContainer.Instance_Load.var2;
        print("CBahram notify about load.");
        print ("Cbahram data values are : var1 : " + var1 + " var2 : " + var2);

    }
    #endregion

	


}
