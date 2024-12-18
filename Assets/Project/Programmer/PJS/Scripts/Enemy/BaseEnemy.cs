using BehaviorDesigner.Runtime;
using System;
using UnityEngine;

[System.Serializable]
public class State
{
    public int Hp;  // ü��
    [Range(0, 50)]public int Damge;       // ���� ������
    [Range(0, 50)]public float TraceDis;  // �ν� ��Ÿ�
    [Range(0, 10)]public float AttackDis; // ���� ��Ÿ�
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
