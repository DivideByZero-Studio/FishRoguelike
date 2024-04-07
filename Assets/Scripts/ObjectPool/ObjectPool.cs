using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField] private List<GameObject> _objectPrefabs;
    [SerializeField] private int _objectsCount;

    private List<GameObject> _disabledPool;
    private List<GameObject> _activePool;

    public static readonly Vector3 DefaultPosition = new Vector3(0f, 0f, 0f);

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _disabledPool = new List<GameObject>();
        _activePool = new List<GameObject>();
        FillDisabledPool();
    }

    public bool TryAddObjectToPool(Type type)
    {
        for (int i = 0; i <= _objectPrefabs.Count; i++)
        {
            if (_objectPrefabs[i].TryGetComponent(type, out var component))
            {
                var obj = Instantiate(_objectPrefabs[i]);
                obj.SetActive(false);
                _disabledPool.Add(obj);
                return true;
            }
        }
        return false;
    }

    public GameObject GetObject(Type type)
    {
        foreach (GameObject obj in _disabledPool)
        {
            if (obj.TryGetComponent(type, out var component))
            {
                ActivateObject(obj);
                Debug.Log(_activePool.Count);
                return obj;
            }
        }
        return null;
    }

    public void ClearActivePool()
    {
        foreach (GameObject obj in _activePool)
        {
            if (_disabledPool.Contains(obj) == false)
            {
                DeactivateObject(obj);
            }
        }
        _activePool.Clear();
    }

    public GameObject SmartGetObject(Type type)
    {
        var obj = GetObject(type);
        if (obj != null)
        {
            return obj;
        }
        TryAddObjectToPool(type);
        obj = GetObject(type);
        return obj;
    }

    public bool DeactivateObject(GameObject obj)
    {
        Debug.Log(_disabledPool.Count);
        if (_activePool.Contains(obj))
        {
            _disabledPool.Add(obj);
            _activePool.Remove(obj);
            obj.SetActive(false);
            return true;
        }
        return false;
    }

    private void FillDisabledPool()
    {
        foreach (GameObject obj in _objectPrefabs)
        {
            for (int i = 0; i < _objectsCount; i++)
            {
                var poolObj = Instantiate(obj);
                _disabledPool.Add(poolObj);
                poolObj.SetActive(false);
            }
        }
    }

    private bool ActivateObject(GameObject obj)
    {
        if (_disabledPool.Contains(obj))
        {
            _activePool.Add(obj);
            _disabledPool.Remove(obj);
            obj.SetActive(true);
            return true;
        }
        return false;
    }
}
