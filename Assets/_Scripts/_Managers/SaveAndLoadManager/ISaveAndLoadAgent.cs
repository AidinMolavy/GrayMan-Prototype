using UnityEngine;
using System.Collections.Generic;

using System.IO;
using System.Runtime.Serialization;

public interface ISaveAndLoadAgent
{
	List<ISaveAndloadClient> ClientsInstances{get;}
	
	bool SaveToFile  (ref Stream s, CSaveAndLoadTypes.eFormatters format);
	bool LoadFromFile(ref Stream s, CSaveAndLoadTypes.eFormatters format);
		
}
