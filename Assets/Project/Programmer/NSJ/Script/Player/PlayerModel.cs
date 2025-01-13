using MKH;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Zenject;

public class PlayerModel : MonoBehaviour
{
    public bool IsTest;
    [Inject]
    public PlayerData Data;
    [Inject]
    [HideInInspector] public GlobalPlayerStateData GlobalStateData;
    [Inject]
    [HideInInspector] public GlobalGameData GameData;


    public ArmUnit Arm;
    #region ü��
    public int MaxHp { get { return Data.MaxHp; } set { Data.MaxHp = value; } }
    public float MaxHpMultiplier { get { return Data.MaxHpMultiplier; } set { Data.MaxHpMultiplier = value; } }
    public int CurHp { get { return Data.CurHp; } set { Data.CurHp = value; CurHpSubject?.OnNext(Data.CurHp); } }
    public Subject<int> CurHpSubject = new Subject<int>();
    #endregion
    #region ����
    public int Defense { get { return Data.Defense; } set { Data.Defense = value; } }
    public float DamageReduction { get { return Data.DamageReduction; } set { Data.DamageReduction = value; } }
    #endregion
    #region ����
    public int AttackPower { get { return Data.AttackPower; } set { Data.AttackPower = value; } }
    public float AttackPowerMultiplier { get { return Data.AttackPowerMultiplier; } set { Data.AttackPowerMultiplier = value; } }
    public float DamageMultiplier { get { return Data.DamageMultiplier; } set { Data.DamageMultiplier = value; } }
    public float AttackSpeed
    {
        get
        {
            return Data.AttackSpeed;
        }
        set { Data.AttackSpeed = value; }
    }
    public float AttackSpeedMultiplier { get { return Data.AttackSpeedMultiplier; } set { Data.AttackSpeedMultiplier = value; } }
    public float[] PowerMeleeAttack { get { return Data.PowerMeleeAttack; } set { Data.PowerMeleeAttack = value; } }
    public float[] PowerThrowAttack { get { return Data.PowerThrowAttack; } set { Data.PowerThrowAttack = value; } }
    public float[] PowerSpecialAttack { get { return Data.PowerSpecialAttack; } set { Data.PowerSpecialAttack = value; } }
    #endregion
    #region ġ��Ÿ
    public float CriticalChance { get { return Data.CriticalChance; } set { Data.CriticalChance = value; } }
    public float CriticalDamage { get { return Data.CriticalDamage; } set { Data.CriticalDamage = value; } }
    #endregion
    #region ��ô��
    public int MaxThrowables { get { return Data.MaxThrowables; } set { Data.MaxThrowables = value; } }
    public int CurThrowables
    {
        get { return Data.CurThrowables; }
        set
        {
            Data.CurThrowables = value;
        }
    }
    public Subject<int> CurThrowCountSubject { get { return Data.CurThrowCountSubject; } set { Data.CurThrowCountSubject = value; } }
    public List<ThrowObjectData> ThrowObjectStack { get { return Data.ThrowObjectStack; } set { Data.ThrowObjectStack = value; } }
    #endregion
    #region Ư��ȿ��
    public List<AdditionalEffect> AdditionalEffects { get { return Data.AdditionalEffects; } set { Data.AdditionalEffects = value; } } // ���Ĩ ���� ����Ʈ
    public List<HitAdditional> HitAdditionals { get { return Data.HitAdditionals; } set { Data.HitAdditionals = value; } }
    public List<ThrowAdditional> ThrowAdditionals { get { return Data.ThrowAdditionals; } set { Data.ThrowAdditionals = value; } } // ���� ��� �߰�ȿ�� ����Ʈ
    public List<PlayerAdditional> PlayerAdditionals { get { return Data.PlayerAdditionals; } set { Data.PlayerAdditionals = value; } } // �÷��̾� �߰�ȿ�� ����Ʈ
    #endregion
    #region �̵�
    public float MoveSpeed { get { return (Data.MoveSpeed) / 20; } set { Data.MoveSpeed = value; } } // �̵��ӵ�
    public float MoveSpeedMultyplier { get { return Data.MoveSpeedMultyplier; } set { Data.MoveSpeedMultyplier = value; } }
    // �뽬
    public float DashDistance { get { return Data.DashDistance / 100f; } set { Data.DashDistance = value; } }
    public int DashStamina { get { return Data.DashStamina; } set { Data.DashStamina = value; } }
    // ����
    public float JumpPower { get { return Data.JumpPower / 13f; } set { Data.JumpPower = value * 13f; } }
    public int JumpStamina { get { return Data.JumpStamina; } set { Data.JumpStamina = value; } }
    public int DoubleJumpStamina { get { return Data.DoubleJumpStamina; } set { Data.DoubleJumpStamina = value; } }
    public int JumpDownStamina { get { return Data.JumpDownStamina; } set { Data.JumpDownStamina = value; } }
    #endregion
    #region ���׹̳�
    public float MaxStamina { get { return Data.MaxStamina; } set { Data.MaxStamina = value; } } // �ִ� ���׹̳�
    public float CurStamina
    {
        get { return Data.CurStamina; }
        set
        {
            float originStamina = Data.CurStamina;
            Data.CurStamina = value;
            if (originStamina > Data.CurStamina)
            {
                _player.IsStaminaCool = true;
            }
            CurStaminaSubject.OnNext(Data.CurStamina);
        }
    } // ���� ���׹̳�
    public Subject<float> CurStaminaSubject = new Subject<float>();
    public float RegainStamina { get { return Data.RegainStamina; } set { Data.RegainStamina = value; } } // ���׹̳� �ʴ� ȸ����
    public float StaminaCoolTime { get { return Data.StaminaCoolTime; } set { Data.StaminaCoolTime = value; } } // ���׹̳� ���� �� ��Ÿ��
    public float StaminaReduction { get { return Data.StaminaReduction; } set { Data.StaminaReduction = value; } }
    public float MaxStaminaCharge { get { return Data.MaxStaminaCharge; } set { Data.MaxStaminaCharge = value; } }
    public float CurStaminaCharge { get { return Data.CurStaminaCharge; } set { Data.CurStaminaCharge = value; CurStaminaChargeSubject.OnNext(value); } }
    public Subject<float> CurStaminaChargeSubject = new Subject<float>();
    public float[] MeleeAttackStamina { get { return Data.MeleeAttackStamina; } set { Data.MeleeAttackStamina = value; } }
    #endregion
    #region ����
    public float MaxMana { get { return Data.MaxMana; } set { Data.MaxMana = value; } } // �ִ� Ư���ڿ�
    public Subject<float> MaxManaSubject { get { return Data.MaxManaSubject; } }
    public float CurMana // ���� Ư�� �ڿ�
    {
        get { return Data.CurMana; }
        set
        {
            Data.CurMana = value;
            // ���� Ư������ �ڿ��� �ִ�ġ�� �ѱ� �� ����
            if (Data.CurMana > Data.MaxMana)
            {
                Data.CurMana = Data.MaxMana;
            }
            else if (Data.CurMana < 0)
            {
                Data.CurMana = 0;
            }
            CurManaSubject.OnNext(Data.CurMana);
        }
    }
    public Subject<float> CurManaSubject = new Subject<float>();
    public float[] ManaConsumption { get { return Data.ManaConsumption; } set { Data.ManaConsumption = value; } }
    public float[] RegainMana { get { return Data.RegainMana; } set { Data.RegainMana = value; } } // Ư���ڿ� ȸ����
    public float RegainAdditiveMana { get { return Data.RegainAdditiveMana; } set { Data.RegainAdditiveMana = value; } }
    public float SpecialChargeGage // Ư������ ������
    {
        get { return Data.SpecialChargeGage; }
        set
        {
            Data.SpecialChargeGage = value;
            SpecialChargeGageSubject.OnNext(Data.SpecialChargeGage);
        }
    }
    public Subject<float> SpecialChargeGageSubject = new Subject<float>();
    #endregion
    public float DrainLife { get { return Data.DrainLife; } set { Data.DrainLife = value; } }
    public GlobalGameData.AmWeapon NowWeapon { get { return Data.NowWeapon; } set { Data.NowWeapon = value; } }
    public float EquipmentDropUpgrade { get { return Data.EquipmentDropUpgrade; } set { Data.EquipmentDropUpgrade = value; } }

    public float BoomRadius;

    [System.Serializable]
    public struct DefaultDrainStruct
    {
        public float DrainDistance;
        public float DrainStamina;
    }
    // TODO : �ν����� ���� �ʿ�
    [System.Serializable]
    public struct DrainStruct
    {
        public DefaultDrainStruct Default;
        public float DrainDistance;
        public float DrainDistanceMultyPlier;
        public float DrainStamina;
    }
    [SerializeField] public DrainStruct Drain;
    public float DrainDistance
    {
        get
        {
            return Drain.DrainDistance * (1 + DrainDistanceMultyPlier / 100); // �巹�� �⺻ ���� * �巹�� ���� ������(%)
        }
        set { Drain.DrainDistance = value; }
    }
    public float DrainDistanceMultyPlier { get { return Drain.DrainDistanceMultyPlier; } set { Drain.DrainDistanceMultyPlier = value; } }
    public float DrainStamina { get { return Drain.DrainStamina * (1 - StaminaReduction / 100); } set { Drain.DrainStamina = value; } }
    [Tooltip("���� ���� �ܰ�")]
    public int ChargeStep;
    [Tooltip("�˹� �Ÿ� ����")]
    public float KnockBackDistanceMultiplier;


    private PlayerView _view;
    private PlayerController _player;
    public void PushThrowObject(ThrowObjectData throwObjectData)
    {
        Data.PushThrowObject(throwObjectData);
    }

    public ThrowObjectData PopThrowObject()
    {
        return Data.PopThrowObject();
    }
    public ThrowObjectData PeekThrowObject()
    {
        return Data.PeekThrowObject();
    }
    public void ClearThrowObject()
    {
        Data.ClearThrowObject();
    }

    private void Awake()
    {
        _view = GetComponent<PlayerView>();
        _player = GetComponent<PlayerController>();
        Data.IsDead = false;

        if (SceneManager.GetActiveScene().name == SceneName.LobbyScene)
        {
            Data.CopyGlobalPlayerData(GlobalStateData, GameData);
        }
        if (IsTest == true)
        {
            GlobalStateData.NewPlayerSetting();
            Data.CopyGlobalPlayerData(GlobalStateData, GameData);
            RegainStamina = 100;
            RegainMana[0] = 100;
        }

        DrainDistance = Drain.Default.DrainDistance;
        DrainStamina = Drain.Default.DrainStamina;
        JumpDownStamina = 40;
        CriticalDamage = 200;
        StaminaCoolTime = 1;

    }

    private float prevAttackSpeed;
    private GlobalGameData.AmWeapon prevWeapon;
    private float prevMaxMana;
    void Start()
    {
        this.FixedUpdateAsObservable()
            .Where(x => prevAttackSpeed != AttackSpeed)
            .Subscribe(x =>
            {
                _view.SetFloat(PlayerView.Parameter.AttackSpeed, AttackSpeed);
                prevAttackSpeed = AttackSpeed;
            }
            );
        this.FixedUpdateAsObservable()
            .Where(x => prevWeapon != NowWeapon)
            .Subscribe(x => { _player.ChangeArmUnit(NowWeapon); prevWeapon = NowWeapon; });

        this.FixedUpdateAsObservable()
            .Where(x => prevMaxMana != MaxMana)
        .Subscribe(x =>
            {
                _view.Panel.MpBar.maxValue = MaxMana;
                _view.Panel.SetChargingMpVarMaxValue(MaxMana);
                prevMaxMana = MaxMana;
            });
    }
    #region �޺� ���� ���� �ڵ�
    //[System.Serializable]
    //public struct MeleeStruct
    //{
    //    [HideInInspector] public int ComboCount;
    //    public MeleeAttackStruct[] PowerMeleeAttack;
    //}
    //[System.Serializable]
    //public struct MeleeAttackStruct
    //{
    //    public float Range;
    //    [Range(0, 360)] public float Angle;
    //    [Range(0, 5)] public float DamageMultiplier;
    //}
    //[Header("�������� ���� �ʵ�")]
    //[SerializeField] public MeleeStruct PrevMelee;
    //public int MeleeComboCount
    //{
    //    get { return PrevMelee.ComboCount; }
    //    set
    //    {
    //        PrevMelee.ComboCount = value;
    //        if (PrevMelee.ComboCount >= PrevMelee.PowerMeleeAttack.Length)
    //        {
    //            PrevMelee.ComboCount = 0;
    //        }
    //    }
    //}
    //public float Range => PrevMelee.PowerMeleeAttack[PrevMelee.ComboCount].Range;
    //public float Angle => PrevMelee.PowerMeleeAttack[PrevMelee.ComboCount].Angle;
    //public float DamageMultiplier => PrevMelee.PowerMeleeAttack[PrevMelee.ComboCount].DamageMultiplier;
    #endregion
}


public partial class GlobalPlayerData
{

}


public partial class PlayerData
{
    [System.Serializable]
    public struct HpStruct
    {
        [Header("�ִ� ü��")]
        public int MaxHp;
        [Header("�ִ� ü�� ����(%)")]
        public float MaxHpMultiplier;
        [Header("���� ü��")]
        public int CurHp;
    }

    [System.Serializable]
    public struct AttackStruct
    {
        [Header("���ݷ�")]
        public int AttackPower;
        [Header("���ݷ� ����")]
        public float AttackPowerMultiplier;
        [Header("������ ����(%)")]
        public float DamageMultiplier;
        [Header("���� �ӵ�")]
        public float AttackSpeed;
        [Header("���� �ӵ� ����(%)")]
        public float AttackSpeedMultiplier;
        [Header("�Ͻ�Ʈ-�Ŀ� �������ݷ�")]
        public float[] PowerMeleeAttack;
        [Header("�Ͻ�Ʈ-�Ŀ� ��ô���ݷ�")]
        public float[] PowerThrowAttack;
        [Header("�Ͻ�Ʈ-�Ŀ� Ư�����ݷ�")]
        public float[] PowerSpecialAttack;
    }
    [System.Serializable]
    public struct StaminaStruct
    {
        [Header("�ִ� ���׹̳�")]
        public float MaxStamina; // �ִ� ���׹̳�
        [Header("���� ���׹̳�")]
        public float CurStamina; // ���� ���׹̳�
        [Header("���׹̳� �ʴ� ȸ����")]
        public float RegainStamina; // ���׹̳� �ʴ� ȸ����
        [Header("���׹̳� ��� �� ��Ÿ��")]
        public float StaminaCoolTime; // ���׹̳� ���� �� ��Ÿ��
        [Header("���׹̳� �Ҹ� (?)")]
        public float ConsumesStamina; // ���׹̳� �Ҹ�
        [Header("���׹̳� �Ҹ� ���ҷ�(%)")]
        public float StaminaReduction;
        [Header("���׹̳� �ִ� ���� �ð�")]
        public float MaxStaminaCharge;
        [Header("���� ���׹̳� ���� �ð�")]
        public float CurStaminaCharge;
        [Header("������-�Ŀ� �������� ���׹̳� �Ҹ�")]
        public float[] MeleeAttackStamina;
    }
    [System.Serializable]
    public struct JumpStruct
    {
        [Header("������")]
        public float JumpPower;
        [Header("���� ���׹̳�")]
        public int JumpStamina;
        [Header("���� ���� ���׹̳�")]
        public int DoubleJumpStamina;
        [Header("�ϰ� ���� ���׹̳�")]
        public int JumpDownStamina;
        [Header("�ִ� ���� Ƚ��")]
        public int MaxJumpCount;
        [Header("���� ���� Ƚ��")]
        public int CurJumpCount;
    }
    [System.Serializable]
    public struct DashStruct
    {
        [Header("�뽬 �ӵ�")]
        public float DashDistance;
        [Header("�뽬 ���׹̳�")]
        public int DashStamina;
    }
    [System.Serializable]
    public struct SpecialStruct
    {
        [Header("�ִ� ����")]
        public float MaxMana;
        [Header("���� ����")]
        public float CurMana;
        [Header("��ô ���� �� ���� ȸ��")]
        public float[] RegainMana; // ������ ���ݴ� ���� ȸ��
        [Header("���� ȸ���� ������(%)")]
        public float RegainAdditiveMana;
        [Header("���� �Ҹ�")]
        public float[] ManaConsumption; // ���� �Ҹ�
        [HideInInspector] public float SpecialChargeGage;
    }
    [System.Serializable]
    public struct DefenseStruct
    {
        [Header("����")]
        public int Defense;
        [Header("���� ���ҷ�")]
        public float DamageReduction;
    }
    [System.Serializable]
    public struct CriticalStruct
    {
        [Header("ũ��Ƽ�� Ȯ��")]
        [Range(0, 100)] public float CriticalChance;
        [Header("ũ��Ƽ�� ������")]
        public float CriticalDamage;
    }
    [System.Serializable]
    public struct ThrowStruct
    {
        [Header("�ִ� ��ô�� ��")]
        public int MaxThrowables;
        [Header("���� ��ô�� ��")]
        public int CurThrowables;
        [Header("��ô�� �Ĺֽ� �߰� ȹ�� ��")]
        public float GainMoreThrowables;
        [Header("��ô�� ����Ʈ")]
        public List<ThrowObjectData> ThrowObjectStack;
    }
    [System.Serializable]
    public struct AdditionalStruct
    {
        [Header("Ư��ȿ�� ����Ʈ")]
        public List<AdditionalEffect> AdditionalEffects; // Ư��ȿ�� ����Ʈ
        [Header("���� �� ȿ�� ����Ʈ")]
        public List<HitAdditional> HitAdditionals;
        [Header("��ô�� ȿ�� ����Ʈ")]
        public List<ThrowAdditional> ThrowAdditionals; // ���� ��� �߰�ȿ�� ����Ʈ
        [Header("�÷��̾� ��ü ȿ�� ����Ʈ")]
        public List<PlayerAdditional> PlayerAdditionals; // �÷��̾� �߰�ȿ�� ����Ʈ
    }
    [System.Serializable]
    public struct MoveStruct
    {
        [Header("�̵��ӵ�")]
        public float MoveSpeed;
        [Header("�̵��ӵ� ����")]
        public float MoveSpeedMultyplier;
    }
    [System.Serializable]
    public struct DataStruct
    {
        public HpStruct Hp;
        public AttackStruct Attack;
        public DefenseStruct Defense;
        public CriticalStruct Critical;
        public StaminaStruct Stamina;
        public SpecialStruct Special;
        public MoveStruct Move;
        public DashStruct Dash;
        public JumpStruct Jump;
        public GlobalGameData.AmWeapon NowWeapon;
        public ThrowStruct Throw;
        public AdditionalStruct Additional;
        public float DrainLife;
        public float EquipmentDropUpgrade;
        // �����̻� ���ӽð�
    }
    [SerializeField] public DataStruct Data;
    #region ü��
    // ü��
    public int MaxHp
    {
        get
        {
            float maxHpMultiplier = 1 + MaxHpMultiplier / 100 >= 0 ? 1 + MaxHpMultiplier / 100 : 0;
            return (int)((Data.Hp.MaxHp + (int)EquipStatus.HP) * maxHpMultiplier); // (�⺻ ü��+���ü��) * ü�� ����
        }
        set
        {
            Data.Hp.MaxHp = value;
            OnChangePlayerDataEvent?.Invoke();
        }
    }
    public float MaxHpMultiplier
    {
        get { return Data.Hp.MaxHpMultiplier; }
        set
        {
            int prevMaxHp = MaxHp;
            float prevMaxHpMultiplier = MaxHpMultiplier;

            Data.Hp.MaxHpMultiplier = value;
            // ü�� ������ ������ ��� ����ü�� ������
            if (Data.Hp.MaxHpMultiplier < prevMaxHpMultiplier)
            {
                if (CurHp > MaxHp)
                    CurHp = MaxHp;
            }
            // ü�� ������ ������ ��� ����ü���� ������ ü�¸�ŭ �÷���
            else if (Data.Hp.MaxHpMultiplier > prevMaxHpMultiplier)
            {
                int HpComparison = MaxHp - prevMaxHp;
                CurHp += HpComparison;
            }

            OnChangePlayerDataEvent?.Invoke();
        }
    }
    public int CurHp
    {
        get
        {
            return Data.Hp.CurHp;
        }
        set
        {
            Data.Hp.CurHp = value;
            // ����ü���� �ִ�ü���� ���� ���ϵ���
            if (Data.Hp.CurHp > MaxHp)
                Data.Hp.CurHp = MaxHp;
        }
    }
    #endregion
    #region ����
    // ����
    public int Defense { get { return Data.Defense.Defense + (int)EquipStatus.Defense; } set { Data.Defense.Defense = value; OnChangePlayerDataEvent?.Invoke(); } }
    public float DamageReduction { get { return Data.Defense.DamageReduction; } set { Data.Defense.DamageReduction = value; } }
    #endregion
    #region ���� 
    public int AttackPower
    {
        get
        {
            float attackMultiplier = 1 + AttackPowerMultiplier / 100 >= 0 ? 1 + AttackPowerMultiplier / 100 : 0; // ������ ������ 0���� ������ ��� 0���� ����
            return (int)((Data.Attack.AttackPower + (int)EquipStatus.Damage) * attackMultiplier); // (�⺻������+��񵥹���) * ���ݷ� ����
        }
        set
        {
            Data.Attack.AttackPower = value;
            OnChangePlayerDataEvent?.Invoke();
        }
    }
    public float AttackPowerMultiplier { get { return Data.Attack.AttackPowerMultiplier; } set { Data.Attack.AttackPowerMultiplier = value; OnChangePlayerDataEvent?.Invoke(); } }
    public float DamageMultiplier { get { return Data.Attack.DamageMultiplier; } set { Data.Attack.DamageMultiplier = value; } }
    public float AttackSpeed
    {
        get
        {
            float attackSpeedMultiplier = 1 + AttackSpeedMultiplier / 100 >= 0 ? 1 + AttackSpeedMultiplier / 100 : 0; // ������ ������ 0���� ������ ��� 0���� ����
            return (Data.Attack.AttackSpeed * (1 + EquipStatus.AttackSpeed)) * attackSpeedMultiplier; // �⺻���ݼӵ� * �����ݼӵ� * ���ݼӵ� ����
        }
        set
        {
            Data.Attack.AttackSpeed = value;
            OnChangePlayerDataEvent?.Invoke();
        }
    }
    public float AttackSpeedMultiplier { get { return Data.Attack.AttackSpeedMultiplier; } set { Data.Attack.AttackSpeedMultiplier = value; OnChangePlayerDataEvent?.Invoke(); } }
    public float[] PowerMeleeAttack { get { return Data.Attack.PowerMeleeAttack; } set { Data.Attack.PowerMeleeAttack = value; } }
    public float[] PowerThrowAttack { get { return Data.Attack.PowerThrowAttack; } set { Data.Attack.PowerThrowAttack = value; } }
    public float[] PowerSpecialAttack { get { return Data.Attack.PowerSpecialAttack; } set { Data.Attack.PowerSpecialAttack = value; } }
    #endregion
    #region ���׹̳�

    // ���׹̳�
    public float MaxStamina { get { return Data.Stamina.MaxStamina + EquipStatus.Stemina; } set { Data.Stamina.MaxStamina = value; OnChangePlayerDataEvent?.Invoke(); } }
    public float CurStamina { get { return Data.Stamina.CurStamina; } set { Data.Stamina.CurStamina = value; } }
    public float RegainStamina { get { return Data.Stamina.RegainStamina; } set { Data.Stamina.RegainStamina = value; } }
    public float StaminaCoolTime { get { return Data.Stamina.StaminaCoolTime; } set { Data.Stamina.StaminaCoolTime = value; } }
    public float ConsumesStamina { get { return Data.Stamina.ConsumesStamina; } set { Data.Stamina.ConsumesStamina = value; } }
    public float StaminaReduction { get { return Data.Stamina.StaminaReduction; } set { Data.Stamina.StaminaReduction = value; } }
    public float MaxStaminaCharge { get { return Data.Stamina.MaxStaminaCharge; } set { Data.Stamina.MaxStaminaCharge = value; } }
    public float CurStaminaCharge { get { return Data.Stamina.CurStaminaCharge; } set { Data.Stamina.CurStaminaCharge = value; } }
    public float[] MeleeAttackStamina { get { return Data.Stamina.MeleeAttackStamina; } set { Data.Stamina.MeleeAttackStamina = value; } }
    #endregion
    #region ����
    // Ư������
    public float MaxMana
    {
        get { return Data.Special.MaxMana + EquipStatus.Mana; }
        set
        {
            Data.Special.MaxMana = value;
            MaxManaSubject.OnNext(value);
            OnChangePlayerDataEvent?.Invoke();
        }
    }
    public Subject<float> MaxManaSubject = new Subject<float>();
    public float CurMana { get { return Data.Special.CurMana; } set { Data.Special.CurMana = value; } }
    public float[] RegainMana { get { return Data.Special.RegainMana; } set { Data.Special.RegainMana = value; } }
    public float RegainAdditiveMana { get { return Data.Special.RegainAdditiveMana; } set { Data.Special.RegainAdditiveMana = value; } }
    public float[] ManaConsumption { get { return Data.Special.ManaConsumption; } set { Data.Special.ManaConsumption = value; } }
    public float SpecialChargeGage { get { return Data.Special.SpecialChargeGage; } set { Data.Special.SpecialChargeGage = value; } }
    #endregion
    #region �̵�
    public float MoveSpeed
    {
        get
        {
            float moveSpeedMultyplier = 1 + MoveSpeedMultyplier / 100 < 0 ? 0 : 1 + MoveSpeedMultyplier / 100;
            return Data.Move.MoveSpeed * (1 + EquipStatus.Speed) * moveSpeedMultyplier; // �⺻ �̵��ӵ� * ��� �̵��ӵ� * �̵��ӵ� ����
        }
        set
        {
            Data.Move.MoveSpeed = value;
            OnChangePlayerDataEvent?.Invoke();
        }
    }
    public float MoveSpeedMultyplier { get { return Data.Move.MoveSpeedMultyplier; } set { Data.Move.MoveSpeedMultyplier = value; } }
    // ����
    public float JumpPower { get { return Data.Jump.JumpPower; } set { Data.Jump.JumpPower = value; OnChangePlayerDataEvent?.Invoke(); } }
    public int JumpStamina
    {
        get { return (int)(Data.Jump.JumpStamina * (1 - StaminaReduction / 100)); }
        set { Data.Jump.JumpStamina = value; }
    }
    public int DoubleJumpStamina
    {
        get
        { return (int)(Data.Jump.DoubleJumpStamina * (1 - StaminaReduction / 100)); }
        set
        { Data.Jump.DoubleJumpStamina = value; }
    }
    public int JumpDownStamina
    {
        get { return (int)(Data.Jump.JumpDownStamina * (1 - StaminaReduction / 100)); }
        set { Data.Jump.JumpDownStamina = value; }
    }
    public int MaxJumpCount { get { return Data.Jump.MaxJumpCount; } set { Data.Jump.MaxJumpCount = value; } }
    public int CurJumpCount { get { return Data.Jump.CurJumpCount; } set { Data.Jump.CurJumpCount = value; } }
    // �뽬
    public float DashDistance { get { return Data.Dash.DashDistance; } set { Data.Dash.DashDistance = value; } }
    public int DashStamina
    {
        get { return (int)(Data.Dash.DashStamina * (1 - StaminaReduction / 100)); }

        set { Data.Dash.DashStamina = value; }
    }
    #endregion
    #region ġ��Ÿ
    // ũ��Ƽ��
    public float CriticalChance { get { return Data.Critical.CriticalChance + EquipStatus.Critical; } set { Data.Critical.CriticalChance = value; OnChangePlayerDataEvent?.Invoke(); } }
    public float CriticalDamage { get { return Data.Critical.CriticalDamage; } set { Data.Critical.CriticalDamage = value; } }
    #endregion
    #region ��ô��
    // ��ô������Ʈ
    public int MaxThrowables { get { return Data.Throw.MaxThrowables; } set { Data.Throw.MaxThrowables = value; } }
    public int CurThrowables { get { return Data.Throw.CurThrowables; } set { Data.Throw.CurThrowables = value; CurThrowCountSubject.OnNext(Data.Throw.CurThrowables); } }
    public Subject<int> CurThrowCountSubject = new Subject<int>();
    public float GainMoreThrowables { get { return Data.Throw.GainMoreThrowables; } set { Data.Throw.GainMoreThrowables = value; } }
    public List<ThrowObjectData> ThrowObjectStack { get { return Data.Throw.ThrowObjectStack; } set { Data.Throw.ThrowObjectStack = value; } }
    #endregion
    #region �߰�ȿ��
    //�߰�ȿ��
    public List<AdditionalEffect> AdditionalEffects { get { return Data.Additional.AdditionalEffects; } set { Data.Additional.AdditionalEffects = value; } }
    public List<HitAdditional> HitAdditionals { get { return Data.Additional.HitAdditionals; } set { Data.Additional.HitAdditionals = value; } }
    public List<ThrowAdditional> ThrowAdditionals { get { return Data.Additional.ThrowAdditionals; } set { Data.Additional.ThrowAdditionals = value; } } // ���� ��� �߰�ȿ�� ����Ʈ
    public List<PlayerAdditional> PlayerAdditionals { get { return Data.Additional.PlayerAdditionals; } set { Data.Additional.PlayerAdditionals = value; } } // �÷��̾� �߰�ȿ�� ����Ʈ
    #endregion
    public float DrainLife { get { return Data.DrainLife; } set { Data.DrainLife = value; } }
    // ������
    public GlobalGameData.AmWeapon NowWeapon { get { return Data.NowWeapon; } set { Data.NowWeapon = value; Debug.Log($"{Data.NowWeapon}"); } }
    public float EquipmentDropUpgrade { get { return Data.EquipmentDropUpgrade + (100 * EquipStatus.EquipRate); } set { Data.EquipmentDropUpgrade = value; } }

    [HideInInspector] public bool IsDead;
    [System.Serializable]
    public struct InventoryStruct
    {
        public EquipmentInventory Inventory;
        public InventoryMain InventoryMain;
        public GameObject BlueChipChoice;
        public BlueChipPanel BlueChipPanel;
        public BlueChipChoicePanel BlueChipChoicePanel;
        public BlueChipChoiceController BlueChipChoiceController;
        public GameObject ChoicePanel;
    }
    public InventoryStruct Inventory;

    public EquipmentEffect EquipStatus => Inventory.Inventory.CurrentEquipmentEffect;

    public event UnityAction OnChangePlayerDataEvent;
    public void CopyGlobalPlayerData(GlobalPlayerStateData globalData, GlobalGameData gameData)
    {
        Data.Hp.MaxHp = (int)globalData.maxHp;
        Data.Hp.CurHp = (int)globalData.maxHp;
        Data.Attack.AttackPower = (int)globalData.commonAttack;

        Data.Attack.PowerMeleeAttack = new float[globalData.shortRangeAttack.Length];
        Data.Attack.PowerMeleeAttack[0] = (int)globalData.shortRangeAttack[0];
        Data.Attack.PowerMeleeAttack[1] = (int)globalData.shortRangeAttack[1];
        Data.Attack.PowerMeleeAttack[2] = (int)globalData.shortRangeAttack[2];

        Data.Attack.PowerThrowAttack = new float[globalData.longRangeAttack.Length];
        Data.Attack.PowerThrowAttack[0] = (int)globalData.longRangeAttack[0];
        Data.Attack.PowerThrowAttack[1] = (int)globalData.longRangeAttack[1];
        Data.Attack.PowerThrowAttack[2] = (int)globalData.longRangeAttack[2];
        Data.Attack.PowerThrowAttack[3] = (int)globalData.longRangeAttack[3];

        Data.Attack.PowerSpecialAttack = new float[globalData.specialAttack.Length];
        Data.Attack.PowerSpecialAttack[0] = (int)globalData.specialAttack[0];
        Data.Attack.PowerSpecialAttack[1] = (int)globalData.specialAttack[1];
        Data.Attack.PowerSpecialAttack[2] = (int)globalData.specialAttack[2];
        Data.Attack.AttackSpeed = globalData.attackSpeed;

        Data.Move.MoveSpeed = globalData.movementSpeed;
        Data.Critical.CriticalChance = globalData.criticalChance;
        Data.Defense.Defense = (int)globalData.defense;
        Data.EquipmentDropUpgrade = globalData.equipmentDropUpgrade;
        Data.DrainLife = globalData.drainLife;
        Data.Stamina.MaxStamina = globalData.maxStamina;
        Data.Stamina.RegainStamina = globalData.regainStamina;
        Data.Stamina.ConsumesStamina = globalData.consumesStamina;

        Data.Special.RegainMana = new float[globalData.regainMana.Length];
        Data.Special.RegainMana[0] = globalData.regainMana[0];
        Data.Special.RegainMana[1] = globalData.regainMana[1];
        Data.Special.RegainMana[2] = globalData.regainMana[2];
        Data.Special.RegainMana[3] = globalData.regainMana[3];

        Data.Special.ManaConsumption = new float[globalData.manaConsumption.Length];
        Data.Special.ManaConsumption[0] = globalData.manaConsumption[0];
        Data.Special.ManaConsumption[1] = globalData.manaConsumption[1];
        Data.Special.ManaConsumption[2] = globalData.manaConsumption[2];
        Data.Throw.GainMoreThrowables = globalData.gainMoreThrowables;
        Data.Throw.MaxThrowables = (int)globalData.maxThrowables;
        Data.NowWeapon = gameData.nowWeapon;
        Data.Special.MaxMana = globalData.maxMana;
        Data.Jump.MaxJumpCount = (int)globalData.maxJumpCount;
        Data.Jump.JumpPower = globalData.jumpPower;
        Data.Jump.JumpStamina = (int)globalData.jumpConsumesStamina;
        Data.Jump.DoubleJumpStamina = (int)globalData.doubleJumpConsumesStamina;
        Data.Dash.DashDistance = globalData.dashDistance;
        Data.Dash.DashStamina = (int)globalData.dashConsumesStamina;

        Data.Stamina.MeleeAttackStamina = new float[globalData.shortRangeAttackStamina.Length];
        Data.Stamina.MeleeAttackStamina[0] = globalData.shortRangeAttackStamina[0];
        Data.Stamina.MeleeAttackStamina[1] = globalData.shortRangeAttackStamina[1];
        Data.Stamina.MeleeAttackStamina[2] = globalData.shortRangeAttackStamina[2];

        OnChangePlayerDataEvent?.Invoke();
    }

    public void ClearAdditional()
    {
        AdditionalEffects.Clear();
        HitAdditionals.Clear();
        ThrowAdditionals.Clear();
        PlayerAdditionals.Clear();
    }

    public void PushThrowObject(ThrowObjectData throwObjectData)
    {
        ThrowObjectStack.Add(throwObjectData);
        CurThrowables++;
    }

    public ThrowObjectData PopThrowObject()
    {
        CurThrowables--;
        ThrowObjectData data = ThrowObjectStack[CurThrowables];
        ThrowObjectStack.RemoveAt(CurThrowables);
        return data;
    }
    public ThrowObjectData PeekThrowObject()
    {
        ThrowObjectData data = ThrowObjectStack[CurThrowables - 1];
        return data;
    }
    public void ClearThrowObject()
    {
        ThrowObjectStack.Clear();
        CurThrowables = 0;
    }
}
