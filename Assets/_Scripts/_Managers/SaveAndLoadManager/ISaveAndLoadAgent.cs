using UnityEngine;
using System.Collections.Generic;
using System.IO;

public interface ISaveAndLoadAgent
{
	List<ISaveAndloadClient> ClientInstance{get;}
	
	bool SaveToFile  (ref Stream s, CSaveAndLoadTypes.eFormatters format);
	bool LoadFromFile(ref Stream s, CSaveAndLoadTypes.eFormatters format);
		
}
