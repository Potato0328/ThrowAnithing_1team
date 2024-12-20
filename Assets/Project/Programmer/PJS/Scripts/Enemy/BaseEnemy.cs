using BehaviorDesigner.Runtime;
using System;
using UnityEngine;

[System.Serializable]
public class State
{
    [Range(50, 1000)] public int MaxHp;  // ü��
    [Range(0, 20)] public int Atk;       // ���ݷ�
    [Range(0, 10)] public float Def;    // ����
    [Range(0, 10)] public float Speed;    // �̵� �ӵ�
    [Range(0, 10)] public float AtkDelay;    // ���� �ӵ�
    [Range(0, 10)] public float TraceDis;  // �ν� ��Ÿ�
    [Range(0, 10)] public float AttackDis; // ���� ��Ÿ�
}


public class BaseEnemy : MonoBehaviour
{
    [SerializeField] BehaviorTree tree;

    [SerializeField] protected State state;
    [SerializeField] int curHp;
    public int Damge { get { return state.Atk; } }
    public int CurHp { get { return curHp; } }

    protected SharedGameObject playerObj;

    private void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        tree = GetComponent<BehaviorTree>();
    }

    private void Start()
    {
        tree.SetVariable("PlayerObj", playerObj);
        tree.SetVariable("PlayerTrans", (SharedTransform)playerObj.Value.transform);
        tree.SetVariable("Speed", (SharedFloat)state.Speed);
        tree.SetVariable("AtkDelay", (SharedFloat)state.AtkDelay);
        tree.SetVariable("TraceDis", (SharedFloat)state.TraceDis);
        tree.SetVariable("AttackDis", (SharedFloat)state.AttackDis);

        curHp = state.MaxHp;
    }

    public void GetDamage(float damage)
    {
        float finalDamage = damage - state.Def;

        if (finalDamage < 0)
            finalDamage = 0;

        curHp -= (int)finalDamage;
        
        Debug.Log($"{(int)damage} ���ظ� ����. curHP : {curHp}");
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
