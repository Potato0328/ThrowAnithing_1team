using System.Collections;
using UnityEngine;

/// <summary>
/// �ڷ�ƾ �븮 ���� Ŭ����
/// </summary>
public class CoroutineHandler : MonoBehaviour
{
    public static CoroutineHandler Instance;

    private void Awake()
    {
        InitSingleTon();
    }

    /// <summary>
    /// �ڷ�ƾ ����
    /// </summary>
    public static Coroutine StartRoutine(IEnumerator enumerator)
    {
        return Instance.StartCoroutine(enumerator);
    }

    public static Coroutine StartRoutine(Coroutine coroutine, IEnumerator enumerator)
    {
        if (coroutine == null)
        {
            return Instance.StartCoroutine(enumerator);
        }
        return null;
    }

    /// <summary>
    /// �ڷ�ƾ ����
    /// </summary>
    public static Coroutine StopRoutine(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            Instance.StopCoroutine(coroutine);
            coroutine = null;
        }
        return coroutine;
    }



    private void InitSingleTon()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
