using UnityEngine;
using System.Collections;

public static class CSaveAndLoadTypes {

	public  enum eFormatters {
		Binary = 0,
		XML
	}
	
	public  class stInfo{
		public string elapsedTime;//Time elapsed since player start the game(play time).
		public string dateAndTime;//Current date and time.
		public int    saveCounts;//Number of saves that player does.
	}
	
	public class stSaveInfo{
		
		public stSaveInfo()
		{
			info = new stInfo();
		}
		
		public stInfo info;
		public string filePath;//Path of actual save file on storage device.include name of file.
	}
}
