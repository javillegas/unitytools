using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : BaseSingleton<ObjectPool> 
{
	[System.Serializable]
	public class PoolRef
	{
		public GameObject type;
		public int size;
		public bool grow;
		public List<GameObject> pool;

		public PoolRef ()
		{
			type = null;
			size = 0;
			grow = false;
			pool = new List<GameObject> ();
		}
	}

	public List<PoolRef> objectPool;

	void Awake ()
	{
		for (int i = 0; i < objectPool.Count; i++)
		{
			objectPool[i].pool = new List<GameObject> ();
			for (int j = 0; j < objectPool[i].size; j++)
			{
				GameObject tmp = Instantiate (objectPool[i].type) as GameObject;
				tmp.SetActive(false);
				objectPool[i].pool.Add(tmp);
			}
		}
	}

	public GameObject GetObject(string name)
	{
		for (int i = 0; i < objectPool.Count; i++)
		{
			if (objectPool[i].type.name == name)
			{
				for (int j = 0; j < objectPool[i].size; j++)
				{
					if (!objectPool[i].pool[j].activeInHierarchy)
					{
						objectPool[i].pool[j].SetActive(true);
						return objectPool[i].pool[j];
					}
				}

				if (objectPool[i].grow)
				{
					GameObject tmp = Instantiate (objectPool[i].type) as GameObject;
					objectPool[i].pool.Add(tmp);
					return tmp;
				}
			}
		}
		Debug.LogWarning("Object Pool: object not found. Null returned.");
		return null;
	}

	public GameObject GetObject(int id)
	{
		if (objectPool[id].type.name == name)
		{
			for (int j = 0; j < objectPool[id].size; j++)
			{
				if (!objectPool[id].pool[j].activeInHierarchy)
				{
					objectPool[id].pool[j].SetActive(true);
					return objectPool[id].pool[j];
				}
			}
			
			if (objectPool[id].grow)
			{
				GameObject tmp = Instantiate (objectPool[id].type) as GameObject;
				objectPool[id].pool.Add(tmp);
				return tmp;
			}
		}
		Debug.LogWarning ("Object Pool: object not found. Null returned.");
		return null;
	}
}
