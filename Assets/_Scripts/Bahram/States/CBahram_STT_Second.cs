using UnityEngine;
using System.Collections;

public class CBahram_STT_Second : IState,IMessageObsever {
    
    private CiaObject _owner;   
    public CBahram_STT_Second(){
        _owner = null;
    }
    
    public CBahram_STT_Second(CiaObject owner){
        _owner = owner;    
    }
	
    public IEnumerator OnBegin (){

        
        CMessageManager.MessageEvent += OnMessage;
        Debug.Log("\"CBahram_STT_Second\" OnBegin called.");
        yield return true;
        
    }

    public void OnUpdate ()
    {
       // Debug.Log("\"CBahram_STT_Second\" Update is called.");
    }

    public IEnumerator OnExit ()
    {
        CMessageManager.MessageEvent -=   OnMessage;
        Debug.Log("\"CBahram_STT_Second\" OnBegin called.");
        yield return true;
    }

    public IEnumerator OnEnd ()
    {
        yield return true;
    }

	
	public void OnMessage(CMessages.eMessages m,object data)
	{
		if (m == CMessages.eMessages.ActionPressed )
		{
			Debug.Log("\"CBahram_STT_Second\" state receive Action pressed message.");
		}
	}

}
