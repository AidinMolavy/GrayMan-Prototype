using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class CSaveAndLoadManager : MonoBehaviour {

	public CSaveAndLoadManager Instance;
	private List<ISaveAndLoad> SALObjects;//list of object(Classes that implement ISaveAndLoad).
	public List<ISaveAndLoad> SALObjects_Exclude;//list of object that dont need to save or load.
	public List<Stream>       Files;//list of opened files that in format of  stream.
	public List<string>       Paths;//Path of actual save file on storage device.include name of file.
	
	public CSaveAndLoadManager()
	{
		Instance = this;
		SALObjects = new List<ISaveAndLoad>();
		SALObjects_Exclude = new List<ISaveAndLoad>();
		Files = new List<Stream>();
	}
	
	public void RegisterObject(ISaveAndLoad o)
	{
		if(0 != null)
			SALObjects.Add(o);
		else
			#warning null object 
	}
	public bool RemoveObject(ISaveAndLoad o)
	{
		if(o != null)
			if (SALObjects.
	}

}
