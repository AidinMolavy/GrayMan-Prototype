using UnityEngine;
using System.Collections;
using System.IO;

public interface ISaveAndLoad {

	bool SaveToFile  (ref Stream s, ref object formatter);
	bool LoadFromFile( ref Stream s,ref object formatter);
		
}
