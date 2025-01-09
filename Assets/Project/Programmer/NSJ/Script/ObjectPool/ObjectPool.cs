using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    public struct PoolInfo
    {
        public Queue<GameObject> Pool;
        public GameObject Prefab;
        public Transform Parent;
    }
    private Dictionary<GameObject, PoolInfo> m_poolDic = new Dictionary<GameObject, PoolInfo>();
    private static Dictionary<GameObject, PoolInfo> _poolDic { get { return Instance.m_poolDic; } }
    private static Transform thisTransform => Instance.transform;
    private Dictionary<GameObject, PoolInfo> m_poolObjectDic = new Dictionary<GameObject, PoolInfo>();
    private static Dictionary<GameObject, PoolInfo> _poolObjectDic { get { return Instance.m_poolObjectDic; } }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Ǯ ����
    /// </summary>
    public static ObjectPool CreateObjectPool()
    {
        if (Instance != null)
            return Instance;
        // Ǯ ����
        ObjectPool pool = null;
        // Ǯ ã��
        GameObject poolObject = GameObject.FindGameObjectWithTag(Tag.ObjectPool);
        // Ǯ�� ���� ���
        if (poolObject != null)
        {
            // ������Ʈ Ǯ ������Ʈ ã��
            pool = poolObject.GetComponent<ObjectPool>();
            if (pool != null)
                return pool;
            // ������ ������Ʈ �߰��ϱ�
            else
                return poolObject.AddComponent<ObjectPool>();
        }
        // Ǯ�� ���� ���
        else
        {
            // ���Ӱ� Ǯ ������Ʈ ����
            GameObject newPool = new GameObject("ObjectPool");
            newPool.tag = Tag.ObjectPool;
            pool = newPool.AddComponent<ObjectPool>();
        }
        return pool;
    }
    #region GetPool
    public static GameObject GetPool(GameObject prefab)
    {
        CreateObjectPool();
        PoolInfo info = FindPool(prefab);
        if (info.Pool.Count > 0)
        {
            GameObject instance = info.Pool.Dequeue();
            instance.gameObject.SetActive(true);
            instance.transform.position = Vector3.zero;
            instance.transform.rotation = Quaternion.identity;
            instance.transform.SetParent(null);
            return instance.gameObject;
        }
        else
        {
            GameObject instance = Instantiate(info.Prefab);
            _poolObjectDic.Add(instance, info);
            return instance;
        }
    }
    public static GameObject GetPool(GameObject prefab, Transform transform)
    {
        CreateObjectPool();
        PoolInfo info = FindPool(prefab);
        if (info.Pool.Count > 0)
        {
            GameObject instance = info.Pool.Dequeue();
            instance.gameObject.SetActive(true);
            instance.transform.SetParent(transform);
            instance.transform.position = transform.position;
            instance.transform.rotation = transform.rotation;
            return instance.gameObject;
        }
        else
        {
            GameObject instance = Instantiate(info.Prefab,transform);
            _poolObjectDic.Add(instance, info);
            return instance;
        }
    }
    public static GameObject GetPool(GameObject prefab, Transform transform, bool worldPositionStay)
    {
        CreateObjectPool();
        PoolInfo info = FindPool(prefab);
        if (info.Pool.Count > 0)
        {
            GameObject instance = info.Pool.Dequeue();
            instance.gameObject.SetActive(true);
            instance.transform.SetParent(transform);
            if (worldPositionStay == true)
            {
                instance.transform.position = prefab.transform.position;
                instance.transform.rotation = prefab.transform.rotation;
            }
            else
            {
                instance.transform.position = transform.position;
                instance.transform.rotation = transform.rotation;
            }

            return instance.gameObject;
        }
        else
        {
            GameObject instance = Instantiate(info.Prefab, transform, worldPositionStay);
            _poolObjectDic.Add(instance, info);
            return instance;
        }
    }
    public static GameObject GetPool(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        CreateObjectPool();
        PoolInfo info = FindPool(prefab);
        if (info.Pool.Count > 0)
        {
            GameObject instance = info.Pool.Dequeue();
            instance.gameObject.SetActive(true);
            instance.transform.position = pos;
            instance.transform.rotation = rot;
            instance.transform.SetParent(null);
            return instance.gameObject;
        }
        else
        {
            GameObject instance = Instantiate(info.Prefab, pos, rot);
            _poolObjectDic.Add(instance, info);
            return instance;
        }
    }
    public static GameObject GetPool<T>(T prefab) where T : Component
    {
        CreateObjectPool();
        PoolInfo info = FindPool(prefab.gameObject);
        if (info.Pool.Count > 0)
        {
            GameObject instance = info.Pool.Dequeue();
            instance.gameObject.SetActive(true);
            instance.transform.position = Vector3.zero;
            instance.transform.rotation = Quaternion.identity;
            instance.transform.SetParent(null);
            return instance.gameObject;
        }
        else
        {
            GameObject instance = Instantiate(info.Prefab);
            _poolObjectDic.Add(instance, info);
            return instance;
        }
    }
    public static GameObject GetPool<T>(T prefab, Transform transform) where T : Component
    {
        CreateObjectPool();
        PoolInfo info = FindPool(prefab.gameObject);
        if (info.Pool.Count > 0)
        {
            GameObject instance = info.Pool.Dequeue();
            instance.gameObject.SetActive(true);
            instance.transform.SetParent(transform);
            instance.transform.position = transform.position;
            instance.transform.rotation = transform.rotation;
            return instance.gameObject;
        }
        else
        {
            GameObject instance = Instantiate(info.Prefab,transform);
            _poolObjectDic.Add(instance, info);
            return instance;
        }
    }
    public static GameObject GetPool<T>(T prefab, Transform transform, bool worldPositionStay) where T : Component
    {
        CreateObjectPool();
        PoolInfo info = FindPool(prefab.gameObject);
        if (info.Pool.Count > 0)
        {
            GameObject instance = info.Pool.Dequeue();
            instance.gameObject.SetActive(true);
            instance.transform.SetParent(transform);
            if (worldPositionStay == true)
            {
                instance.transform.position = prefab.transform.position;
                instance.transform.rotation = prefab.transform.rotation;
            }
            else
            {
                instance.transform.position = transform.position;
                instance.transform.rotation = transform.rotation;
            }

            return instance.gameObject;
        }
        else
        {
            GameObject instance = Instantiate(info.Prefab, transform, worldPositionStay);
            _poolObjectDic.Add(instance, info);
            return instance;
        }
    }
    public static GameObject GetPool<T>(T prefab, Vector3 pos, Quaternion rot) where T : Component
    {
        CreateObjectPool();
        PoolInfo info = FindPool(prefab.gameObject);
        if (info.Pool.Count > 0)
        {
            GameObject instance = info.Pool.Dequeue();
            instance.gameObject.SetActive(true);
            instance.transform.position = pos;
            instance.transform.rotation = rot;
            instance.transform.SetParent(null);
            return instance.gameObject;
        }
        else
        {
            GameObject instance = Instantiate(info.Prefab, pos, rot);
            _poolObjectDic.Add(instance, info);
            return instance;
        }
    }
    #endregion
    #region ReturnPool
    public static void ReturnPool(GameObject instance)
    {
        CreateObjectPool();
        PoolInfo info = default;
        if (_poolObjectDic.ContainsKey(instance) == true) 
        {
            info = _poolObjectDic[instance];
        }
        else
        {
            info = FindPool(instance);
        }

        instance.transform.SetParent(info.Parent);
        instance.gameObject.SetActive(false);
        info.Pool.Enqueue(instance);
    }
    public static void ReturnPool<T>(T prefab, GameObject instance) where T : Component
    {
        CreateObjectPool();
        PoolInfo info = default;
        if (_poolObjectDic.ContainsKey(instance) == true)
        {
            info = _poolObjectDic[instance];
        }
        else
        {
            info = FindPool(instance);
        }

        instance.transform.SetParent(info.Parent);
        instance.gameObject.SetActive(false);
        info.Pool.Enqueue(instance);
    }
    #endregion
    private static PoolInfo FindPool(GameObject poolPrefab)
    {
        PoolInfo pool = default;
        if (_poolDic.ContainsKey(poolPrefab) == false)
        {
            Transform newParent = new GameObject(poolPrefab.name).transform;
            newParent.SetParent(thisTransform, true); // parent
            Queue<GameObject> newPool = new Queue<GameObject>(); // pool
            PoolInfo newPoolInfo = GetPoolInfo(newPool, poolPrefab, newParent);
            _poolDic.Add(poolPrefab, newPoolInfo);
        }
        pool = _poolDic[poolPrefab];
        return pool;
    }
    private static PoolInfo GetPoolInfo(Queue<GameObject> pool, GameObject prefab, Transform parent)
    {
        PoolInfo info = new PoolInfo();
        info.Pool = pool;
        info.Parent = parent;
        info.Prefab = prefab;
        return info;
    }
}