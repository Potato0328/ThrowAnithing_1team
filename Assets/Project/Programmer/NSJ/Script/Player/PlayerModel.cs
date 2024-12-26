using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using static PlayerData;

public class PlayerModel : MonoBehaviour
{
    public GlobalPlayerData GlobalData;
    public PlayerData Data;
    public ArmUnit Arm;

    public int MaxHp { get { return Data.MaxHp; } set { Data.MaxHp = value; } }
    public int CurHp { get { return Data.CurHp; } set { Data.CurHp = value; } }
    public int Defense { get { return Data.Defense; } set { Data.Defense = value; } }
    public float DamageReduction { get { return Data.DamageReduction; } set { Data.DamageReduction = value; } }
    public int Damage { get { return Data.Damage; } set { Data.Damage = value; } }
    public int AttackSpeed { get { return Data.AttackSpeed; } set { Data.AttackSpeed = value; _view.SetFloat(PlayerView.Parameter.AttackSpeed, Data.AttackSpeed); } }
    public float Critical { get { return Data.Critical; } set { Data.Critical = value; } }
    public float CriticalDamage { get { return Data.CriticalDamage; } set { Data.CriticalDamage = value; } }
    public int MaxThrowCount { get { return Data.MaxThrowCount; } set { Data.MaxThrowCount = value; } }
    public int CurThrowCount
    {
        get { return Data.CurThrowCount; }
        set
        {
            Data.CurThrowCount = value;
            CurThrowCountSubject?.OnNext(Data.CurThrowCount);

        }
    }
    public Subject<int> CurThrowCountSubject = new Subject<int>();

    public List<AdditionalEffect> AdditionalEffects { get { return Data.AdditionalEffects; } set { Data.AdditionalEffects = value; } } // ���Ĩ ���� ����Ʈ
    public List<HitAdditional> HitAdditionals { get { return Data.HitAdditionals; } set { Data.HitAdditionals = value; } }
    public List<ThrowAdditional> ThrowAdditionals { get { return Data.ThrowAdditionals; } set { Data.ThrowAdditionals = value; } } // ���� ��� �߰�ȿ�� ����Ʈ
    public List<PlayerAdditional> PlayerAdditionals { get { return Data.PlayerAdditionals; } set { Data.PlayerAdditionals = value; } } // �÷��̾� �߰�ȿ�� ����Ʈ
    public List<ThrowObjectData> ThrowObjectStack { get { return Data.ThrowObjectStack; } set { Data.ThrowObjectStack = value; } }
    public float MoveSpeed { get { return Data.MoveSpeed; } set { Data.MoveSpeed = value; } } // �̵��ӵ�

    // �뽬
    public float DashPower { get { return Data.DashPower; } set { Data.DashPower = value; } }
    public int DashStamina { get { return Data.DashStamina; } set { Data.DashStamina = value; } }
    // ����
    public float JumpPower { get { return Data.JumpPower; } set { Data.JumpPower = value; } }
    public int JumpStamina { get { return Data.JumpStamina; } set { Data.JumpStamina = value; } }
    public int DoubleJumpStamina { get { return Data.DoubleJumpStamina; } set { Data.DoubleJumpStamina = value; } }
    public int JumpDownStamina { get { return Data.JumpDownStamina; } set { Data.JumpDownStamina = value; } }
    public float MaxStamina { get { return Data.MaxStamina; } set { Data.MaxStamina = value; } } // �ִ� ���׹̳�
    public float CurStamina { get { return Data.CurStamina; } set { Data.CurStamina = value; CurStaminaSubject.OnNext(Data.CurStamina); } } // ���� ���׹̳�
    public Subject<float> CurStaminaSubject = new Subject<float>();
    public float StaminaRecoveryPerSecond { get { return Data.StaminaRecoveryPerSecond; } set { Data.StaminaRecoveryPerSecond = value; } } // ���׹̳� �ʴ� ȸ����
    public float StaminaCoolTime { get { return Data.StaminaCoolTime; } set { Data.StaminaCoolTime = value; } } // ���׹̳� ���� �� ��Ÿ��

    public float MaxSpecialGage { get { return Data.MaxSpecialGage; } set { Data.MaxSpecialGage = value; } } // �ִ� Ư���ڿ�
    public float CurSpecialGage // ���� Ư�� �ڿ�
    {
        get { return Data.CurSpecialGage; }
        set
        {
            Data.CurSpecialGage = value;
            // ���� Ư������ �ڿ��� �ִ�ġ�� �ѱ� �� ����
            if (Data.CurSpecialGage > Data.MaxSpecialGage)
            {
                Data.CurSpecialGage = Data.MaxSpecialGage;
            }
            else if (Data.CurSpecialGage < 0)
            {
                Data.CurSpecialGage = 0;
            }
            CurSpecialGageSubject.OnNext(Data.CurSpecialGage);
        }
    }
    public Subject<float> CurSpecialGageSubject = new Subject<float>();
    public float[] SpecialRecoveryAmount { get { return Data.SpecialRecoveryAmount; } set { Data.SpecialRecoveryAmount = value; } } // Ư���ڿ� ȸ����
    public float SpecialChargeGage // Ư������ ������
    {
        get { return Data.SpecialChargeGage; }
        set
        {
            Data.SpecialChargeGage = value;
            if (Data.SpecialChargeGage > 1)
                SpecialChargeGage = 1;
            SpecialChargeGageSubject.OnNext(Data.SpecialChargeGage);
        }
    }
    public Subject<float> SpecialChargeGageSubject = new Subject<float>();


    [System.Serializable]
    public struct MeleeStruct
    {
        [HideInInspector] public int ComboCount;
        public MeleeAttackStruct[] MeleeAttack;
    }
    [System.Serializable]
    public struct MeleeAttackStruct
    {
        public float Range;
        [Range(0, 360)] public float Angle;
        [Range(0, 5)] public float DamageMultiplier;
    }
    [Header("�������� ���� �ʵ�")]
    [SerializeField] public MeleeStruct Melee;
    public int MeleeComboCount
    {
        get { return Melee.ComboCount; }
        set
        {
            Melee.ComboCount = value;
            if (Melee.ComboCount >= Melee.MeleeAttack.Length)
            {
                Melee.ComboCount = 0;
            }
        }
    }
    public float Range => Melee.MeleeAttack[Melee.ComboCount].Range;
    public float Angle => Melee.MeleeAttack[Melee.ComboCount].Angle;
    public float DamageMultiplier => Melee.MeleeAttack[Melee.ComboCount].DamageMultiplier;

    [System.Serializable]
    public struct ThrowStruct
    {
        public float BoomRadius;
    }
    [Header("��ô ���� ���� �ʵ�")]
    [SerializeField] public ThrowStruct Throw;
    public float BoomRadius { get { return Throw.BoomRadius; } set { Throw.BoomRadius = value; } }

    // TODO : �ν����� ���� �ʿ�
    public float DrainDistance;

    public int ChargeStep;


    private PlayerView _view;
    public void PushThrowObject(ThrowObjectData throwObjectData)
    {
        ThrowObjectStack.Add(throwObjectData);
        CurThrowCount++;
    }

    public ThrowObjectData PopThrowObject()
    {
        CurThrowCount--;
        ThrowObjectData data = ThrowObjectStack[CurThrowCount];
        ThrowObjectStack.RemoveAt(CurThrowCount);
        return data;
    }
    public ThrowObjectData PeekThrowObject()
    {
        ThrowObjectData data = ThrowObjectStack[CurThrowCount - 1];
        return data;
    }

    private void Awake()
    {
        _view = GetComponent<PlayerView>();
    }
    // TODO : �ϴ� ����Ʈ ����, �̱������� ���� �� ���Ŀ� �����丵 
    private void Start()
    {
        //Data = DataContainer.Instance.PlayerData;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            AttackSpeed++;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            AttackSpeed--;
        }
    }
}


public partial class GlobalPlayerData
{

}


public partial class PlayerData
{
    [System.Serializable]
    public struct HpStruct
    {
        public int MaxHp;
        public int CurHp;
    }

    [System.Serializable]
    public struct StaminaStruct
    {
        public float MaxStamina; // �ִ� ���׹̳�
        public float CurStamina; // ���� ���׹̳�
        public float StaminaRecoveryPerSecond; // ���׹̳� �ʴ� ȸ����
        public float StaminaCoolTime; // ���׹̳� ���� �� ��Ÿ��
    }
    [System.Serializable]
    public struct JumpStruct
    {
        public float JumpPower;
        public int JumpStamina;
        public int DoubleJumpStamina;
        public int JumpDownStamina;
    }
    [System.Serializable]
    public struct DashStruct
    {
        public float DashPower;
        public int DashStamina;
    }
    [System.Serializable]
    public struct SpecialStruct
    {
        public float MaxSpecialGage;
        public float CurSpecialGage;
        public float[] SpecialRecoveryAmount;
        [HideInInspector] public float SpecialChargeGage;
    }
    [System.Serializable]
    public struct DefenseStruct
    {
        public int Defense;
        [Range(0, 100)] public float DamageReduction;
    }
    [System.Serializable]
    public struct CriticalStruct
    {
        [Range(0, 100)] public float Critical;
        public float CriticalDamage;
    }
    [System.Serializable]
    public struct NSJTestStruct
    {
        public HpStruct Hp;
        public DefenseStruct Defense;
        public StaminaStruct Stamina;
        public SpecialStruct Special;
        public JumpStruct Jump;
        public DashStruct Dash;
        public float MoveSpeed;
        public int Damage;
        // �����̻� ���ӽð�
        public int AttackSpeed;
        public CriticalStruct Critical;
        // ���� ����
        public int MaxThrowCount;
        public int CurThrowCount;
        public List<ThrowObjectData> ThrowObjectStack;
        public List<AdditionalEffect> AdditionalEffects; // ���Ĩ ���� ����Ʈ
        public List<HitAdditional> HitAdditionals;
        public List<ThrowAdditional> ThrowAdditionals; // ���� ��� �߰�ȿ�� ����Ʈ
        public List<PlayerAdditional> PlayerAdditionals; // �÷��̾� �߰�ȿ�� ����Ʈ
    }
    [SerializeField] private NSJTestStruct _NSJTest;
    public float MoveSpeed { get { return _NSJTest.MoveSpeed; } set { _NSJTest.MoveSpeed = value; } }
    // ü��
    public int MaxHp { get { return _NSJTest.Hp.MaxHp; } set { _NSJTest.Hp.MaxHp = value; } }
    public int CurHp { get { return _NSJTest.Hp.CurHp; } set { _NSJTest.Hp.CurHp = value; } }
    // ����
    public int Defense { get { return _NSJTest.Defense.Defense; } set { _NSJTest.Defense.Defense = value; } }
    public float DamageReduction{ get { return _NSJTest.Defense.DamageReduction; } set { _NSJTest.Defense.DamageReduction = value; } }

    // ���׹̳�
    public float MaxStamina { get { return _NSJTest.Stamina.MaxStamina; } set { _NSJTest.Stamina.MaxStamina = value; } }
    public float CurStamina { get { return _NSJTest.Stamina.CurStamina; } set { _NSJTest.Stamina.CurStamina = value; } }
    public float StaminaRecoveryPerSecond { get { return _NSJTest.Stamina.StaminaRecoveryPerSecond; } set { _NSJTest.Stamina.StaminaRecoveryPerSecond = value; } }
    public float StaminaCoolTime { get { return _NSJTest.Stamina.StaminaCoolTime; } set { _NSJTest.Stamina.StaminaCoolTime = value; } }
    // Ư������
    public float MaxSpecialGage { get { return _NSJTest.Special.MaxSpecialGage; } set { _NSJTest.Special.MaxSpecialGage = value; } }
    public float CurSpecialGage { get { return _NSJTest.Special.CurSpecialGage; } set { _NSJTest.Special.CurSpecialGage = value; } }
    public float[] SpecialRecoveryAmount { get { return _NSJTest.Special.SpecialRecoveryAmount; } set { _NSJTest.Special.SpecialRecoveryAmount = value; } }
    public float SpecialChargeGage { get { return _NSJTest.Special.SpecialChargeGage; } set { _NSJTest.Special.SpecialChargeGage = value; } }
    // ����
    public float JumpPower { get { return _NSJTest.Jump.JumpPower; } set { _NSJTest.Jump.JumpPower = value; } }
    public int JumpStamina { get { return _NSJTest.Jump.JumpStamina; } set { _NSJTest.Jump.JumpStamina = value; } }
    public int DoubleJumpStamina { get { return _NSJTest.Jump.DoubleJumpStamina; } set { _NSJTest.Jump.DoubleJumpStamina = value; } }
    public int JumpDownStamina { get { return _NSJTest.Jump.JumpDownStamina; } set { _NSJTest.Jump.JumpDownStamina = value; } }
    // �뽬
    public float DashPower { get { return _NSJTest.Dash.DashPower; } set { _NSJTest.Dash.DashPower = value; } }
    public int DashStamina { get { return _NSJTest.Dash.DashStamina; } set { _NSJTest.Dash.DashStamina = value; } }
    public int Damage { get { return _NSJTest.Damage; } set { _NSJTest.Damage = value; } }
    public int AttackSpeed { get { return _NSJTest.AttackSpeed; } set { _NSJTest.AttackSpeed = value; } }
    public float Critical { get { return _NSJTest.Critical.Critical; } set { _NSJTest.Critical.Critical = value; } }
    public float CriticalDamage { get { return _NSJTest.Critical.CriticalDamage; } set { _NSJTest.Critical.CriticalDamage = value; } }
    public int MaxThrowCount { get { return _NSJTest.MaxThrowCount; } set { _NSJTest.MaxThrowCount = value; } }
    public int CurThrowCount { get { return _NSJTest.CurThrowCount; } set { _NSJTest.CurThrowCount = value; } }
    public List<ThrowObjectData> ThrowObjectStack { get { return _NSJTest.ThrowObjectStack; } set { _NSJTest.ThrowObjectStack = value; } }
    public List<AdditionalEffect> AdditionalEffects { get { return _NSJTest.AdditionalEffects; } set { _NSJTest.AdditionalEffects = value; } }
    public List<HitAdditional> HitAdditionals { get { return _NSJTest.HitAdditionals; } set { _NSJTest.HitAdditionals = value; } }
    public List<ThrowAdditional> ThrowAdditionals { get { return _NSJTest.ThrowAdditionals; } set { _NSJTest.ThrowAdditionals = value; } } // ���� ��� �߰�ȿ�� ����Ʈ
    public List<PlayerAdditional> PlayerAdditionals { get { return _NSJTest.PlayerAdditionals; } set { _NSJTest.PlayerAdditionals = value; } } // �÷��̾� �߰�ȿ�� ����Ʈ
}
