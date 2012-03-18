using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public static class CSingleton {
 
    //MonoBehavoir Singleton
    /// <summary>
    /// Gets the singleton instance.
    /// This function just work for type of classes that inherit form MonoBehavior.
    /// </summary>
    /// <returns>
    /// The singleton instance.
    /// </returns>
    /// <param name='instances'>
    /// list of instance that created.
    /// </param>
    /// <param name='t'>
    /// Type of instances.
    /// </param>
    /// <param name='tag'>
    /// GameObject's tag.
    /// </param>
    /// <param name='name'>
    /// GameObject's name.
    /// </param>
    public static object GetSingletonInstance(ref ArrayList instances,Type t,string tag,string name){
    
        if (instances.Count == 1)
            return instances[0];
        if (instances.Count > 1){
            CDebug.LogError(CDebug.eMessageTemplate.SomthingIsWrong);
            return null;
        }
        //create instance when no instance exist.
        //first create GameObject and then attach script to it and return the instance.
        if (instances.Count <= 0){//no instance exist   
            GameObject[] gos; 
            gos = GameObject.FindGameObjectsWithTag(tag);//looking for game objects that have match tag name.
            if(gos.Length > 1){//if more than one game object exist.
               for(int j = 0; j < gos.Length; j++){//find the game object that match with "name" parameter.
                     if (gos[j].name == name){//if name matched then add script to that object and return.
                        gos[j].AddComponent(t);
                        return instances[0]; 
                    }
               }
                gos[0].AddComponent(t);//when no game object's name matced then add script to one of them.
                CDebug.LogWarning(
                    "Multiple GameObject with tag \""+ tag +"\" detected." +
                     "Anyway i add \""+ t.FullName +"\" script to one of them that name is \""+ gos[0].name + "\".");
                return instances[0];
                
            }
            if(gos.Length == 1){
                gos[0].AddComponent(t);
                return instances[0];
            }
            if(gos.Length <= 0){
                GameObject go = new GameObject(name);
                go.AddComponent(t);
                go.tag  = tag;
                return instances[0];
            }
        }  
        return null;
    }
    
    // ScriptableObject Singleton
    /// <summary>
    /// Get the singleton instance.
    /// This overload work just for object that inherit from "ScriptableObject".
    /// </summary>
    /// <returns>
    /// The singleton instance.
    /// </returns>
    /// <param name='instances'>
    /// List of instance. Must inherit form "ScriptableObject" 
    /// </param>
    /// <param name='t'>
    /// Type of the class.
    /// </param>
    public static object GetSingletonInstance(ref ArrayList instances,Type t){
        if (instances.Count == 1)
            return instances[0];
        if (instances.Count > 1){
            CDebug.LogError(CDebug.eMessageTemplate.SomthingIsWrong);
            return null;
        }
        //create instance when no instance exist.
        if (instances.Count <= 0){//no instance exist
            if(t.IsSubclassOf(typeof(ScriptableObject)))
                ScriptableObject.CreateInstance(t);
            else
                Activator.CreateInstance(t);
            return instances[0]; 
        }
        return null;
    }
    
    
    /// <summary>
    /// Destroies the extra instances.
    /// this functtion ensure that just one instance of class existed.
    /// first add instance to instances array then call this function.
    /// you must call this function on top most in the Awake() function.
    /// this class work 
    /// </summary>
    /// 
    public static void DestroyExtraInstances(ArrayList instances ){        
        if( instances.Count > 1){
            for(int i = 1; i < instances.Count; i++ ){
               if(instances[i] is MonoBehaviour)
                    MonoBehaviour.Destroy((MonoBehaviour)instances[i]);
                else if (instances[i] is ScriptableObject)
                    ScriptableObject.Destroy((ScriptableObject)instances[i]);
                    
                instances.RemoveAt(i);
            }
        }
    }
}