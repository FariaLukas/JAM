using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InitializerManager : MonoBehaviour
{
    public List<IInitializable> initializables;

    private void Awake()
    {
        initializables = new List<IInitializable>();

        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.TryGetComponent(out IInitializable init))
            {
                initializables.Add(init);
            }

        }
    }

    private void Start()
    {
        foreach (var i in initializables)
        {
            i.Init();
        }

    }
}

public interface IInitializable
{
    void Init();
}
