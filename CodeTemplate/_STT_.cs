using UnityEngine;
using System.Collections;

public class _STT_ : IState,IMessageObsever {
	
#region Private Fields
    
    private CiaObject _owner;
    
#endregion

#region Constructors    
    
    public CBahram_STT_First(){
        _owner = null;
    }
    
    public CBahram_STT_First(CiaObject owner){
        _owner = owner;    
    }
    
#endregion
    
#region Interactive Object's Events
    
    public IEnumerator OnBegin(){
	
	  //Additional Event Must Add Here.
      CMessageManager.MessageEvent += OnMessage;//Sample Of Additional Event Syntax.
      yield return true;
	  
    }
    
    public void        OnUpdate(){    

    }

    public IEnumerator OnExit()
    {
		//Additional Event Must Remove Here.
        CMessageManager.MessageEvent -= messageHandler;
        yield return true;
    }

    public IEnumerator OnEnd()
    {
        yield return true;
    }
    
#endregion
    
#region Additional Events    
    
	public void OnMessage(CMessages.eMessages m,object data){

	}
    
#endregion
    
}