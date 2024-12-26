using Assets.Project.Programmer.NSJ.RND.Script;
using BehaviorDesigner.Runtime;
using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class State
{
    [Range(50, 3000)] public int MaxHp;  // ü��
    [Range(0, 20)] public int Atk;       // ���ݷ�
    [Range(0, 10)] public float Def;    // ����
    [Range(0, 10)] public float Speed;    // �̵� �ӵ�
    [Range(0, 10)] public float AtkDelay;   // ���� �ӵ�
    [Range(0, 10)] public float AttackDis;  // ���� ��Ÿ�
    [Range(0, 10)] public float TraceDis;   // �ν� ��Ÿ�
}

public class BaseEnemy : MonoBehaviour, IHit
{
    [SerializeField] BehaviorTree tree;

    [Header("���� �⺻ �������ͽ�")]
    [SerializeField] protected State state;
    [Header("������ ��� Ȯ��(100 ����)")]
    [SerializeField] float reward;
    [Header("���� ü��")]
    [SerializeField] int curHp;

    [HideInInspector] public int resultDamage;  // ���������� ���� �Դ� ������
    [HideInInspector] public Collider[] overLapCollider = new Collider[100];
    public int Damage { get { return state.Atk; } }
    public int CurHp { get { return curHp; } set { curHp = value; } }

    protected SharedGameObject playerObj;

    private void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        tree = GetComponent<BehaviorTree>();
    }

    private void Start()
    {
        SettingVariable();
        curHp = state.MaxHp;
    }

    private void SettingVariable()
    {
        tree.SetVariable("PlayerObj", playerObj);
        tree.SetVariable("PlayerTrans", (SharedTransform)playerObj.Value.transform);
        tree.SetVariable("Speed", (SharedFloat)state.Speed);
        tree.SetVariable("AtkDelay", (SharedFloat)state.AtkDelay);
        tree.SetVariable("TraceDis", (SharedFloat)state.TraceDis);
        tree.SetVariable("AttackDis", (SharedFloat)state.AttackDis);
        tree.SetVariable("Reward", (SharedFloat)reward);
    }

    private void OnDrawGizmosSelected()
    {
        // �Ÿ� �׸���
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, state.TraceDis);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, state.AttackDis);
    }

    /// <summary>
    /// ���Ͱ� ���ع޴� ������
    /// </summary>
    public void TakeDamage(int damage, bool isStun)
    {
        resultDamage = damage - (int)state.Def;
        tree.SetVariableValue("TakeDamage", true);

        if (resultDamage <= 0)
            resultDamage = 0;

        curHp -= resultDamage;
        
        // TODO : ��� ���� TakeDamage �Ű������� ��ȯ
        tree.SetVariableValue("Stiff", isStun);
        Debug.Log($"{resultDamage} ���ظ� ����. curHP : {curHp}");
    }

    /// <summary>
    /// ���� �� ���� ������ �ο�
    /// </summary>
    public void TakeChargeBoom(float range, int damage)
    {
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, range, overLapCollider);
        for (int i = 0; i < hitCount; i++)
        {
            IHit hit = overLapCollider[i].GetComponent<IHit>();
            if (hit != null)
            {
                hit.TakeDamage(damage, false);
            }
        }
    }
}
