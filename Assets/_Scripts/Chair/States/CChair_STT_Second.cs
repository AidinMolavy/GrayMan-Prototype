using UnityEngine;
using System.Collections;
using System;
public class CChair_STT_Second : IState,IMessageObsever {
	
	static public CChair_STT_Second Instance;
	
	CChair_STT_Second()
	{

		Instance = this;
	}
	
	public void OnMessage (CMessages.eMessages m, object data)
	{
		if(m == CMessages.eMessages.ChairActive)
		{
			Debug.Log("STT_CChair_Second state is activated by chariActive and done its job.");
		}
	}
	
    public IEnumerator OnBegin ()
    {
         yield return true;
    }

    public void OnUpdate ()
    {
        
    }

    public IEnumerator OnExit ()
    {
         yield return true;
    }

    public IEnumerator OnEnd ()
    {
         yield return true;
    }
}