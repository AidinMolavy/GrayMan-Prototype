using UnityEngine;
using System.Collections;


public class CBahram_STT_First : IState,IMessageObsever {
	
#region Private Fields
    
    private CiaObject _owner;
    private CMessageManager.MessageDelegate messageHandler;
    
#endregion

#region Constructors    
    
    public CBahram_STT_First(){
        _owner = null;
    }
    
    public CBahram_STT_First(CiaObject owner){
        _owner = owner;    
    }
    
#endregion
    
#region Ineteractive Object's Events
    
    public IEnumerator OnBegin(){
      messageHandler = OnMessage;
      CMessageManager.MessageEvent +=  OnMessage;
      Debug.Log("\"CBahram_STT_First\" OnBegin called.");
      yield return true;
    }
    
    public void        OnUpdate(){    
       // Debug.Log("\"CBahram_STT_First\" Update is called.");
    }

    public IEnumerator OnExit()
    {
        CMessageManager.MessageEvent -=   messageHandler;
        Debug.Log("\"CBahram_STT_First\" OnExit is called.");
        yield return true;
    }

    public IEnumerator OnEnd()
    {
        yield return true;
    }
    
#endregion
    
#region Additional Events    
    
	public void OnMessage(CMessages.eMessages m,object data)
	{
		if (m == CMessages.eMessages.ActionPressed )
		{
			Debug.Log("\"CBahram_STT_First\" state receive Action pressed message.");
		}
	}
    
#endregion
    
}