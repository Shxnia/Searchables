using System;
using System.Collections.Generic;
using UnityEngine;

namespace WordSearch
{
	public class ObjectPool
	{
		private GameObject objectPrefab;

		private List<GameObject> instantiatedObjects = new List<GameObject>();

		private Transform parent;

		public ObjectPool(GameObject objectPrefab, int initialSize, Transform parent = null)
		{
			this.objectPrefab = objectPrefab;
			this.parent = parent;
			for (int i = 0; i < initialSize; i++)
			{
				GameObject gameObject = this.CreateObject();
				gameObject.SetActive(false);
			}
		}

		public GameObject GetObject()
		{
			for (int i = 0; i < this.instantiatedObjects.Count; i++)
			{
				if (!this.instantiatedObjects[i].activeSelf)
				{
					return this.instantiatedObjects[i];
				}
			}
			return this.CreateObject();
		}

		public void ReturnAllObjectsToPool()
		{
			for (int i = 0; i < this.instantiatedObjects.Count; i++)
			{
				this.instantiatedObjects[i].SetActive(false);
				this.instantiatedObjects[i].transform.SetParent(this.parent, false);
			}
		}

		public void DestroyAllObjects()
		{
			for (int i = 0; i < this.instantiatedObjects.Count; i++)
			{
				UnityEngine.Object.Destroy(this.instantiatedObjects[i]);
			}
			this.instantiatedObjects.Clear();
		}

		private GameObject CreateObject()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objectPrefab);
			gameObject.transform.SetParent(this.parent, false);
			this.instantiatedObjects.Add(gameObject);
			return gameObject;
		}
	}
}
