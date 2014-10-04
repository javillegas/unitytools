using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseResourceManager<T> : BaseSingleton<BaseResourceManager<T>>
	where T : Object
{
	public List<string> paths;

	private List<T> resource;

	public void LoadAll()
	{
		if (paths.Count == 0)
		{
			Debug.LogWarning("Loading resources: paths are not set.");
			return;
		}
		foreach (string p in paths)
		{
			Object[] tmp = Resources.LoadAll (p, typeof(T));
			resource = new List<T> ();
			
			for (int i = 0; i < tmp.Length; i++)
				resource.Add (tmp[i] as T);
		}
	}

	public void Load (string path)
	{
		resource.Add (Resources.Load (path, typeof(T)) as T);
	}

	public void Unload (int id)
	{
		Resources.UnloadAsset (resource [id]);
		resource.RemoveAt (id);
	}

	public void Unload (string name)
	{
		for (int i = 0; i < resource.Count; i++)
		{
			if (resource[i].name == name)
			{
				Resources.UnloadAsset(resource[i]);
				return;
			}
		}		
		Debug.LogWarning("Unloading mesh: mesh not found.");
	}

	public void UnloadAll ()
	{
		for (int i = 0 ; i < resource.Count; i++)
		{
			Resources.UnloadAsset(resource[i]);
			resource.RemoveAt(i);
		}
	}

	public T this[int id]
	{
		get { return resource[id]; }
		set { resource[id] = value; }
	}

	public T Get (string name)
	{
		for (int i = 0; i < resource.Count; i++)
		{
			if (resource[i].name == name)
				return resource[i];
		}

		Debug.LogWarning("Resource not found. Null returned.");
		return null;
	}
}
