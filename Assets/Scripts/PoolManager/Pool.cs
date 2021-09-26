using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize;

    private List<GameObject> _pooledObjects = new List<GameObject>();
    private GameObject _poolHolder;

    private void Awake()
    {
        WarmPool();
    }

    public void WarmPool()
    {
        if (!prefab) return;

        _poolHolder = new GameObject();
        _poolHolder.name = gameObject.name + " - Pool";

        for (int i = 0; i < poolSize; i++)
        {
            AddGameObject();
        }

    }

    private GameObject AddGameObject()
    {
        if (!prefab) return null;

        GameObject newGO = (GameObject)Instantiate(prefab);
        newGO.SetActive(false);

        newGO.transform.SetParent(_poolHolder.transform);

        newGO.name = prefab.name + "-" + _pooledObjects.Count;

        _pooledObjects.Add(newGO);

        return newGO;
    }

    public GameObject GetPooledGameObject()
    {
        foreach (var g in _pooledObjects)
        {
            if (!g.activeInHierarchy)
                return g;
        }

        return AddGameObject();
    }

    public void DestroyOne()
    {
        if (_pooledObjects != null)
            foreach (var g in _pooledObjects)
            {
                if (g.activeInHierarchy)
                {
                    g.SetActive(false);
                    return;
                }
            }
    }

}
