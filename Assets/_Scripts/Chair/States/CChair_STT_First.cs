using UnityEngine;
using System.Collections;

public class CChair_STT_First : IState,IMessageObsever {
	
	private CiaObject _owner;
	
    
    public CChair_STT_First(){
        
        
        _owner = null;    
    }
    
    public CChair_STT_First(CiaObject owner){
        _owner = owner;
    }
    public IEnumerator OnBegin ()
    {
        CMessageManager.MessageEvent += OnMessage;
         Debug.Log("\"CChair_STT_First\" OnBegin called.");
        yield return true;
    }

    public void OnUpdate ()
    {
        
    }

    public IEnumerator OnExit ()
    {
        CMessageManager.MessageEvent -= OnMessage;
        Debug.Log("\"CChair_STT_First\" OnExit called.");
         yield return true;
    }

    public IEnumerator OnEnd ()
    {
         yield return true;
    }	
    
    public void OnMessage (CMessages.eMessages m, object data)
    {
        Debug.Log("\"CChair_STT_First\" state receive Action pressed message.");
    }
	
}
