using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public static class CSaveAndLoadTypes {
 
    [System.Serializable]
	public  enum eFormatters {
		Binary = 0,
		XML
	}
    
    [System.Serializable]
    public  enum eInOrExclude {
        Include = 0,
        Exclude
    }
    
	[System.Serializable]
	public  class stInfo{
		public string elapsedTime;//Time elapsed since player start the game(play time).
		public string dateAndTime;//Current date and time.
		public int    saveCounts;//Number of saves that player does.
        public int    index;
	}
    
	[System.Serializable]
	public class stSaveInfo{
		
		public stSaveInfo()
		{
			info = new stInfo();
		}
		
		public stInfo info;
		public string filePath;//Path of actual save file on storage device.include name of file.
	}
    
    public class stSAL{
        
        public List<ISaveAndLoadAgent> agents;
        public eInOrExclude eAgent;
        public string       path;
        
        public stSAL(){
            
            agents = new List<ISaveAndLoadAgent>();
     
        }
            
        
    }
    
}
