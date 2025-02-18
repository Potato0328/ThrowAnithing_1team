using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public ObjectPool.PoolInfo PoolInfo;

    IPooledObject _poolObject;

    private void Awake()
    {
        _poolObject = GetComponent<IPooledObject>();
    }

    public void InitPooledObject()
    {
        if (_poolObject != null)
        {
            _poolObject.InitPooledObject();
        }
    }
}

public interface IPooledObject
{
    void InitPooledObject();
}