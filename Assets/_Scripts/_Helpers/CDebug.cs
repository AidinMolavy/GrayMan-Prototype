using UnityEngine;
using System.Collections;

public static class CDebug {
    
    public enum eMessageTemplate{
        NullRefrences = 0,
        InvalideFileName,
        InvalidePathAddress,
        EmptyOrNullString,
        NullParameter,
        SomthingIsWrong
        
    }
    
    private static  string _preFix = "Aidin Say: "; 
    private static  string _exPreFix = "Exception Handled -> ";
    
    public  static  void LogError(string message){
        string tmpStr = _preFix + message;
        Debug.LogError(tmpStr);
    }
    
    public static void LogError(eMessageTemplate template){
       Debug.LogError(_preFix + retTemplate(template));
            
    }
    
    public static void LogExError(string message){  
        
        string tmpStr;        
        tmpStr  = _preFix + _exPreFix + message ;
        Debug.LogError(tmpStr);
        
        
    }
    
    public static void LogWarning(string message){
        string tmpStr = _preFix + message;
        Debug.LogWarning(tmpStr);
    }
    
    private static string retTemplate(eMessageTemplate template){
        
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
        default :
            return "Template not implemented.";
        }
     }        
}
