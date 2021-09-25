using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolSize;

    private List<GameObject> _pooledObjects;
    private GameObject _poolHolder;

    private void Awake()
    {
        WarmPool();
    }

    public void WarmPool()
    {
        if (!_prefab) return;

        _pooledObjects = new List<GameObject>();
        _poolHolder = new GameObject();
        _poolHolder.name = gameObject.name + " - Pool";

        for (int i = 0; i < _poolSize; i++)
        {
            AddGameObject();
        }

    }

    private GameObject AddGameObject()
    {
        if (!_prefab) return null;

        GameObject newGO = (GameObject)Instantiate(_prefab);
        newGO.SetActive(false);

        newGO.transform.SetParent(_poolHolder.transform);

        newGO.name = _prefab.name + "-" + _pooledObjects.Count;

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
