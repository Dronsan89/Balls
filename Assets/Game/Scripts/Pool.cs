using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    private T prefab;
    private bool autoExpand;
    private Transform container;

    private List<T> pool;

    public Pool(T prefab, int count, Transform container, bool autoExpand)
    {
        this.prefab = prefab;
        this.container = container;
        this.autoExpand = autoExpand;

        CreatePool(count);
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
        {
            return element;
        }

        if (autoExpand)
        {
            return CreateObject(true);
        }

        throw new Exception($"There is no free elements in pool of type{typeof(T)}");
    }

    public void DiactivateAll()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            pool[i].gameObject.SetActive(false);
        }
    }

    private void CreatePool(int count)
    {
        pool = new List<T>();

        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = UnityEngine.Object.Instantiate(prefab, container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        pool.Add(createdObject);
        return createdObject;
    }

    private bool HasFreeElement(out T element)
    {
        foreach (var obj in pool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                element = obj;
                element.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }
}
