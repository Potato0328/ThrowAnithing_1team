using System.Collections;
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
        public bool IsActive;
    }

    /// <summary>
    /// �����տ�
    /// </summary>
    private Dictionary<int, PoolInfo> _poolDic = new Dictionary<int, PoolInfo>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

    }

    /// <summary>
    /// Ǯ ����
    /// </summary>
    public static ObjectPool CreateObjectPool()
    {

        if (Instance != null)
        {
            return Instance;
        }
        else
        {
            // ���Ӱ� Ǯ ������Ʈ ����
            GameObject newPool = new GameObject("ObjectPool");
            ObjectPool pool = newPool.AddComponent<ObjectPool>();
            return pool;
        }

    }
    #region GetPool
    public static GameObject Get(GameObject prefab)
    {
        GameObject instance = ProcessGet(prefab);
        return instance;
    }
    public static GameObject Get(GameObject prefab, Transform transform)
    {
        GameObject instance = ProcessGet(prefab, transform);
        return instance;
    }
    public static GameObject Get(GameObject prefab, Transform transform, bool worldPositionStay)
    {
        GameObject instance = ProcessGet(prefab, transform, worldPositionStay);
        return instance;
    }
    public static GameObject Get(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        GameObject instance = ProcessGet(prefab, pos, rot);
        return instance;
    }
    public static GameObject Get(GameObject prefab, float returnDelay)
    {
        GameObject instance = ProcessGet(prefab);
        Return(instance, returnDelay);
        return instance;
    }
    public static GameObject Get(GameObject prefab, Transform transform, float returnDelay)
    {
        GameObject instance = ProcessGet(prefab, transform);
        Return(instance, returnDelay);
        return instance;
    }
    public static GameObject Get(GameObject prefab, Transform transform, bool worldPositionStay, float returnDelay)
    {
        GameObject instance = ProcessGet(prefab, transform, worldPositionStay);
        Return(instance, returnDelay);
        return instance;
    }
    public static GameObject Get(GameObject prefab, Vector3 pos, Quaternion rot, float returnDelay)
    {
        GameObject instance = ProcessGet(prefab, pos, rot);
    
        Return(instance, returnDelay);
        return instance;
    }
    public static T Get<T>(T prefab) where T : Component
    {
        GameObject instance = ProcessGet(prefab.gameObject);
        T component = instance.GetComponent<T>();
        return component;
    }
    
    public static T Get<T>(T prefab, Transform transform) where T : Component
    {
        GameObject instance = ProcessGet(prefab.gameObject, transform);
        T component = instance.GetComponent<T>();
        return component;
    }
    
    public static T Get<T>(T prefab, Transform transform, bool worldPositionStay) where T : Component
    {
        GameObject instance = ProcessGet(prefab.gameObject, transform, worldPositionStay);
        T component = instance.GetComponent<T>();
        return component;
    }
    
    public static T Get<T>(T prefab, Vector3 pos, Quaternion rot) where T : Component
    {
        GameObject instance = ProcessGet(prefab.gameObject, pos, rot);
        T component = instance.GetComponent<T>();
        return component;
    }
    
    public static T Get<T>(T prefab, float returnDelay) where T : Component
    {
        GameObject instance = ProcessGet(prefab.gameObject);
        T component = instance.GetComponent<T>();
        Return(component, returnDelay);
        return component;
    }
    public static T Get<T>(T prefab, Transform transform, float returnDelay) where T : Component
    {
        GameObject instance = ProcessGet(prefab.gameObject, transform);
        T component = instance.GetComponent<T>();
        Return(component, returnDelay);
        return component;
    }
    public static T Get<T>(T prefab, Transform transform, bool worldPositionStay, float returnDelay) where T : Component
    {
        GameObject instance = ProcessGet(prefab.gameObject, transform, worldPositionStay);
        T component = instance.GetComponent<T>();
        Return(component, returnDelay);
        return component;
    }
    public static T Get<T>(T prefab, Vector3 pos, Quaternion rot, float returnDelay) where T : Component
    {
        GameObject instance = ProcessGet(prefab.gameObject, pos, rot);
        T component = instance.GetComponent<T>();
        Return(component, returnDelay);
        return component;
    }
    #endregion
    #region ReturnPool
    public static void Return(GameObject instance)
    {
        ProcessReturn(instance.gameObject);
    }
    public static void Return<T>(T instance) where T : Component
    {
        ProcessReturn(instance.gameObject);
    }
    public static void Return(GameObject instance, float delay)
    {
        if (instance == null)
            return;
        if (instance.activeSelf == false)
            return;
        Instance.StartCoroutine(ReturnRoutine(instance, delay));
    }
    public static void Return<T>(T instance, float delay) where T : Component
    {
        if (instance == null)
            return;

        if (instance.gameObject.activeSelf == false)
            return;
        Instance.StartCoroutine(ReturnRoutine(instance.gameObject, delay));
    }
    static IEnumerator ReturnRoutine(GameObject instance, float delay)
    {
        Debug.Log(instance);
        yield return delay.GetDelay();
        if (instance == null)
            yield break;

        if (instance.activeSelf == false)
            yield break;

        Return(instance);
    }
    #endregion
    #region GetAutoPool
    public static void Get(GameObject prefab, float intervalTime, float returnDelay, ref Coroutine coroutine)
    {
        if (coroutine == null)
        {
            coroutine = Instance.StartCoroutine(Instance.GetAutoPoolRoutine(prefab, intervalTime, returnDelay));
        }
    }
    public static void Get(GameObject prefab, Transform transform, float intervalTime, float returnDelay, ref Coroutine coroutine)
    {
        if (coroutine == null)
        {
            coroutine = Instance.StartCoroutine(Instance.GetAutoPoolRoutine(prefab, transform, false, intervalTime, returnDelay));
        }
    }
    public static void Get(GameObject prefab, Transform transform, bool worldPositionStay, float intervalTime, float returnDelay, ref Coroutine coroutine)
    {
        if (coroutine == null)
        {
            coroutine = Instance.StartCoroutine(Instance.GetAutoPoolRoutine(prefab, transform, worldPositionStay, intervalTime, returnDelay));
        }
    }
    public static void Get(GameObject prefab, Vector3 pos, Quaternion rot, float intervalTime, float returnDelay, ref Coroutine coroutine)
    {
        if (coroutine == null)
        {
            coroutine = Instance.StartCoroutine(Instance.GetAutoPoolRoutine(prefab, pos, rot, intervalTime, returnDelay));
        }
    }
    public static void Get(GameObject prefab, float intervalTime, float returnDelay, float duration)
    {
        Coroutine coroutine = Instance.StartCoroutine(Instance.GetAutoPoolRoutine(prefab, intervalTime, returnDelay));
        Instance.StartCoroutine(Instance.GetAutoPoolDurationRoutine(coroutine, duration));
    }
    public static void Get(GameObject prefab, Vector3 pos, Quaternion rot, float intervalTime, float returnDelay, float duration)
    {
        Coroutine coroutine = Instance.StartCoroutine(Instance.GetAutoPoolRoutine(prefab, pos, rot, intervalTime, returnDelay));
        Instance.StartCoroutine(Instance.GetAutoPoolDurationRoutine(coroutine, duration));
    }
    public static void Return(ref Coroutine coroutine)
    {
        if (coroutine != null)
        {
            Instance.StopCoroutine(coroutine);
            coroutine = null;
        }
    }
    IEnumerator GetAutoPoolRoutine(GameObject prefab, float intervalTime, float returnDelay)
    {
        while (true)
        {
            Get(prefab, returnDelay);
            yield return intervalTime.GetDelay();
        }
    }
    IEnumerator GetAutoPoolRoutine(GameObject prefab, Transform transform, bool worldPositionStay, float intervalTime, float returnDelay)
    {
        while (true)
        {
            Get(prefab, transform, worldPositionStay, returnDelay);
            yield return intervalTime.GetDelay();
        }
    }
    IEnumerator GetAutoPoolRoutine(GameObject prefab, Vector3 pos, Quaternion rot, float intervalTime, float returnDelay)
    {
        while (true)
        {
            Get(prefab, pos, rot, returnDelay);
            yield return intervalTime.GetDelay();
        }
    }
    IEnumerator GetAutoPoolDurationRoutine(Coroutine coroutine, float duration)
    {
        yield return duration.GetDelay();
        Return(ref coroutine);
    }
    #endregion
    private static PoolInfo FindPool(GameObject poolPrefab)
    {
        CreateObjectPool();

        int prefabID = poolPrefab.GetInstanceID();

        PoolInfo pool = default;
        if (Instance._poolDic.ContainsKey(prefabID) == false)
        {
            Transform newParent = new GameObject(poolPrefab.name).transform;
            newParent.SetParent(Instance.transform, true); // parent
            Queue<GameObject> newPool = new Queue<GameObject>(); // pool
            PoolInfo newPoolInfo = GetPoolInfo(newPool, poolPrefab, newParent);

            // Ǯ ��ųʸ� �߰�
            Instance._poolDic.Add(prefabID, newPoolInfo);
            // Ǯ ��Ƽ����� ����
            Instance.StartCoroutine(Instance.IsActiveRoutine(prefabID));
        }

        pool = Instance._poolDic[prefabID];
        pool.IsActive = true;
        Instance._poolDic[prefabID] = pool;

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
    private static void AddPoolObjectComponent(GameObject instance, PoolInfo info)
    {
        PooledObject poolObject = instance.GetOrAddComponent<PooledObject>();
        poolObject.PoolInfo = info;
    }
    private static GameObject ProcessGet(GameObject prefab)
    {
        GameObject instance = null;
        PoolInfo info = FindPool(prefab);
        if (FindObject(info))
        {
            instance = info.Pool.Dequeue();
            instance.transform.position = Vector3.zero;
            instance.transform.rotation = Quaternion.identity;
            instance.transform.SetParent(null);
            instance.gameObject.SetActive(true);
        }
        else
        {
            instance = Instantiate(info.Prefab);
            AddPoolObjectComponent(instance, info);
        }
        return instance;
    }
    private static GameObject ProcessGet(GameObject prefab, Transform transform)
    {
        GameObject instance = null;
        PoolInfo info = FindPool(prefab);
        if (FindObject(info))
        {
            instance = info.Pool.Dequeue();
            instance.transform.SetParent(transform);
            instance.transform.position = transform.position;
            instance.transform.rotation = transform.rotation;
            instance.gameObject.SetActive(true);
        }
        else
        {
            instance = Instantiate(info.Prefab, transform);
            AddPoolObjectComponent(instance, info);
        }

        return instance;
    }
    private static GameObject ProcessGet(GameObject prefab, Transform transform, bool worldPositionStay)
    {
        GameObject instance = null;
        PoolInfo info = FindPool(prefab);
        if (FindObject(info))
        {
            instance = info.Pool.Dequeue();
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
            instance.gameObject.SetActive(true);
        }
        else
        {
            instance = Instantiate(info.Prefab, transform, worldPositionStay);
            AddPoolObjectComponent(instance, info);
        }

        return instance;
    }
    private static GameObject ProcessGet(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        GameObject instance = null;
        PoolInfo info = FindPool(prefab);
        if (FindObject(info))
        {
            instance = info.Pool.Dequeue();
            instance.transform.position = pos;
            instance.transform.rotation = rot;
            instance.transform.SetParent(null);
            instance.gameObject.SetActive(true);
        }
        else
        {
            instance = Instantiate(info.Prefab, pos, rot);
            AddPoolObjectComponent(instance, info);
        }
        return instance;
    }
    private static void ProcessReturn(GameObject instance)
    {
        CreateObjectPool();
        if (instance == null)
            return;

        if (instance.activeSelf == false)
            return;

        PooledObject poolObject = instance.GetComponent<PooledObject>();
        PoolInfo info = poolObject.PoolInfo;

        instance.transform.position = info.Prefab.transform.position;
        instance.transform.rotation = info.Prefab.transform.rotation;
        instance.transform.localScale = info.Prefab.transform.localScale;
        instance.transform.SetParent(info.Parent);

        poolObject.InitPooledObject();

        instance.gameObject.SetActive(false);
        info.Pool.Enqueue(instance.gameObject);
    }
    private static bool FindObject(PoolInfo info)
    {
        GameObject instance = null;
        while (true)
        {
            if (info.Pool.Count <= 0)
                return false;

            instance = info.Pool.Peek();
            if (instance != null)
                break;

            info.Pool.Dequeue();
        }

        return true;

    }
    private IEnumerator IsActiveRoutine(int id)
    {
        float maxTimer = 300;
        float delayTime = 10;
        float timer = maxTimer;
        while (true)
        {      
            // Ǯ ��������� �ð� �ʱ�ȭ
            if (Instance._poolDic[id].IsActive == true)
            {
                timer = maxTimer;
                PoolInfo pool = Instance._poolDic[id];
                pool.IsActive = false;
                Instance._poolDic[id] = pool;
            }

            // Ÿ�̸� ���� �� 
            if (timer <= 0)
            {
                // Ǯ(ť) ���� ������Ʈ �ı�
                int poolCount = Instance._poolDic[id].Pool.Count;
                for (int i = 0; i < poolCount; i++)
                {
                    GameObject pooledObject = Instance._poolDic[id].Pool.Dequeue();
                    if(pooledObject != null)
                    {
                        Destroy(pooledObject);
                    }
                }
            }
            else
            {
                timer -= delayTime;
            }
            yield return delayTime.GetDelay();
        }
    }
}