using UnityEngine;
using System.Collections;

/// <summary>
/// Encapsulate Debug class functinality with custome behavoiur.
/// In entire program use this class instead of Debug class.
/// With using this class we can have more specific error and better understandig of what happend.
/// </summary>
public static class CDebug {
    
    public enum eMessageTemplate{
        NullRefrences = 0,
        InvalideFileName,
        InvalidePathAddress,
        EmptyOrNullString,
        NullParameter,
        SomthingIsWrong,
        NotImplemented
        
    }
    
    private static  string _preFix   = "Aidin Say: ";//Prefix for all errors. specify who send the error. 
    private static  string _exPreFix = "Exception Handled -> ";//When an exeption handled we use this prefix.
    
    public  static  void LogError     (string message){
        string tmpStr = _preFix + message;
        Debug.LogError(tmpStr);
    }
    
    public static   void LogError     (eMessageTemplate template){
       Debug.LogError(_preFix + retTemplate(template));
            
    }
    
    public static   void LogExError   (string message){  
        
        string tmpStr;        
        tmpStr  = _preFix + _exPreFix + message ;
        Debug.LogError(tmpStr);
    }
    
    public static   void LogWarning   (string message){
        string tmpStr = _preFix + message;
        Debug.LogWarning(tmpStr);
    }
    
    public static   void LogExWarning (string message){
        string tmpStr;        
        tmpStr  = _preFix + _exPreFix + message ;
        Debug.LogWarning(tmpStr);
    }
    
    private static  string retTemplate(eMessageTemplate template){
        
        switch(template){
            
            case eMessageTemplate.NullRefrences :
                return "Null Reference.";
            
            case eMessageTemplate.InvalideFileName :
                return "Invalide File Name.";
                    
            case eMessageTemplate.InvalidePathAddress :
                return "Invalide Path Address.";
            case eMessageTemplate.EmptyOrNullString :
                return "Empty Or Null String.";
            case eMessageTemplate.NullParameter :
                return "NUll Parameter Is Not Accepted.";
            case eMessageTemplate.SomthingIsWrong :
                return "Somthing IS Wrong. This Error Must Never Happend.";
            case eMessageTemplate.NotImplemented :
                return "You Are Using Method or Propety That Is Not Implemented. Please Implement It First.";         
            default :
                return "Template not implemented.";
        }
   }        
}
