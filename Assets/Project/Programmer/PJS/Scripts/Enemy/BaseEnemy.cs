using BehaviorDesigner.Runtime;
using System;
using UnityEngine;

[System.Serializable]
public class State
{
    [Range(100, 1000)] public int Hp;  // ü��
    [Range(0, 50)] public int Damge;       // ���� ������
    [Range(0, 50)] public float TraceDis;  // �ν� ��Ÿ�
    [Range(0, 10)] public float AttackDis; // ���� ��Ÿ�
    [Range(0, 10)] public float Speed;    // �̵� �ӵ�
}


public class BaseEnemy : MonoBehaviour
{
    [SerializeField] BehaviorTree tree;

    [SerializeField] protected State state;

    public int Damge { get { return state.Damge; } }
    public int Hp { get { return state.Hp; } }

    private SharedGameObject playerObj;
    private SharedTransform playerTrans;

    private void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        tree = GetComponent<BehaviorTree>();
    }

    private void Start()
    {
        tree.SetVariable("PlayerObj", playerObj);
        tree.SetVariable("PlayerTrans", playerTrans);
        tree.SetVariable("TraceDis", (SharedFloat)state.TraceDis);
        tree.SetVariable("AttackDis", (SharedFloat)state.AttackDis);
        tree.SetVariable("Speed", (SharedFloat)state.Speed);
    }

    private void OnDrawGizmosSelected()
    {
        // �Ÿ� �׸���
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, state.TraceDis);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, state.AttackDis);
    }
}
