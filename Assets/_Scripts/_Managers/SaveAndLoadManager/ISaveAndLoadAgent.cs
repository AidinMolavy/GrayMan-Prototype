using UnityEngine;
using System.Collections.Generic;
using System.IO;

public interface ISaveAndLoadAgent
{
	List<ISaveAndloadClient> ClientsInstances{get;}
	
	bool SaveToFile  (ref Stream s, CSaveAndLoadTypes.eFormatters format);
	bool LoadFromFile(ref Stream s, CSaveAndLoadTypes.eFormatters format);
		
}
