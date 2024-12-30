using BehaviorDesigner.Runtime;
using System;
using UnityEngine;

[System.Serializable]
public class State
{
    public string Name;
    [Range(50, 3000)] public int MaxHp;  // ü��
    [Range(0, 20)] public int Atk;       // ���ݷ�
    [Range(0, 10)] public float Def;    // ����
    [Range(0, 10)] public float Speed;    // �̵� �ӵ�
    [Range(0, 10)] public float AtkDelay;   // ���� �ӵ�
    [Range(0, 10)] public float AttackDis;  // ���� ��Ÿ�
    [Range(0, 10)] public float TraceDis;   // �ν� ��Ÿ�
}

[RequireComponent(typeof(BattleSystem))]
public class BaseEnemy : MonoBehaviour, IHit,IDebuff
{
    [SerializeField] protected BehaviorTree tree;

    [Header("���� �⺻ �������ͽ�")]
    [SerializeField] protected State state;
    [Header("������ ��� Ȯ��(100 ����)")]
    [SerializeField] float reward;
    [Header("���� ü��")]
    [SerializeField] int curHp;

    [HideInInspector] int maxHp;      // �ִ� ü��
    [HideInInspector] float speed;      // �̵��ӵ�
    [HideInInspector] float jumpPower;  // ������
    [HideInInspector] float attackSpeed;// ���ݼӵ�

    [HideInInspector] public int resultDamage;  // ���������� ���� �Դ� ������
    [HideInInspector] public Collider[] overLapCollider = new Collider[100];
    [HideInInspector] public BattleSystem Battle;

    public int Damage { get { return state.Atk; } }
    public int MaxHp {  get { return maxHp; } set { maxHp = value; } }
    public int CurHp { get { return curHp; } set { curHp = value; } }
    public float MoveSpeed { get { return speed; } set { speed = value; } }
    public float JumpPower { get { return jumpPower; } set { jumpPower = value; } }
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }



    protected SharedGameObject playerObj;

    private void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        tree = GetComponent<BehaviorTree>();
        Battle = GetComponent<BattleSystem>();
        // FIXME : ���߿� ���� �ʿ�
        gameObject.layer = Layer.Monster;
    }

    private void Start()
    {
        SettingVariable();
        curHp = state.MaxHp;
        speed = state.Speed;
        attackSpeed = state.AtkDelay;
    }

    public State GetState()
    {
        return state;
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
        Debug.Log($"TakeDamage : {isStun}") ;
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
                if (overLapCollider[i].gameObject.name.CompareTo("Boss") == 0)
                    return;

                hit.TakeDamage(damage, false);
            }
        }
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
