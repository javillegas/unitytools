using UnityEngine;
using System.Collections;

public class ResourceManager : BaseResourceManager<Mesh> 
{
 	void Awake()
	{
		paths.Add("meshes/cars");
		LoadAll ();
	}
}
