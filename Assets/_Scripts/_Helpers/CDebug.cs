using UnityEngine;
using System.Collections;

public static class CDebug {
    
    public enum eMessageTemplate{
        NullRefrences = 0,
        InvalideFileName,
        InvalidePathAddress,
        EmptyOrNullString
        
    }
    
    private static  string _prefix = "Aidin Say: "; 
    
    
    public  static  void LogError(string message){
        string tmpStr = _prefix + message;
        Debug.LogError(tmpStr);
    }
    
    public static void LogError(eMessageTemplate template){
       Debug.LogError(retTemplate(template));
            
    }
    
    public static void LogWarning(string message){
        string tmpStr = _prefix + message;
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
        default :
            return "Template not implemented.";
        }
     }        
}
