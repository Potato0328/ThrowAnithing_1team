using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    public GlobalPlayerData GlobalData;
    public PlayerData Data;

    public ArmUnit Arm;
    public int Damage { get { return Data.Damage; } set { Data.Damage = value; } }
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

    public List<AdditionalEffect> AdditionalEffects { get { return Data.AdditionalEffects; } set { Data.AdditionalEffects= value; } } // ���Ĩ ���� ����Ʈ
    public List<HitAdditional> HitAdditionals { get { return Data.HitAdditionals; } set { Data.HitAdditionals = value; } }
    public List<ThrowAdditional> ThrowAdditionals { get { return Data.ThrowAdditionals; } set { Data.ThrowAdditionals = value; } } // ���� ��� �߰�ȿ�� ����Ʈ
    public List<PlayerAdditional> PlayerAdditionals { get { return Data.PlayerAdditionals; } set { Data.PlayerAdditionals= value; } } // �÷��̾� �߰�ȿ�� ����Ʈ
    public List<ThrowObjectData> ThrowObjectStack { get { return Data.ThrowObjectStack; } set { Data.ThrowObjectStack = value; } }
    public float MoveSpeed { get { return Data.MoveSpeed; } set { Data.MoveSpeed = value; } } // �̵��ӵ�

    // TODO : �ν����� ���� �ʿ�
    public float DashPower; // �뽬 �ӵ�
    // TODO : �ν����� ���� �ʿ�
    public float JumpPower; // ������
    [System.Serializable]
    public struct StaminaStruct
    {
        public float MaxStamina; // �ִ� ���׹̳�
        public float CurStamina; // ���� ���׹̳�
        public float StaminaRecoveryPerSecond; // ���׹̳� �ʴ� ȸ����
        public float StaminaCoolTime; // ���׹̳� ���� �� ��Ÿ��
    }
    [SerializeField] public StaminaStruct Stamina;
    public float MaxStamina { get { return Stamina.MaxStamina; } set { Stamina.MaxStamina = value; } } // �ִ� ���׹̳�
    public float CurStamina { get { return Stamina.CurStamina; } set { Stamina.CurStamina = value; CurStaminaSubject.OnNext(Stamina.CurStamina); } } // ���� ���׹̳�
    public Subject<float> CurStaminaSubject = new Subject<float>();
    public float StaminaRecoveryPerSecond { get { return Stamina.StaminaRecoveryPerSecond; } set { Stamina.StaminaRecoveryPerSecond = value; } } // ���׹̳� �ʴ� ȸ����
    public float StaminaCoolTime { get { return Stamina.StaminaCoolTime; } set { Stamina.StaminaCoolTime = value; } } // ���׹̳� ���� �� ��Ÿ��


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

    // TODO : �ϴ� ����Ʈ ����, �̱������� ���� �� ���Ŀ� �����丵 
    private void Start()
    {
        //Data = DataContainer.Instance.PlayerData;
    }
}


public partial class GlobalPlayerData
{

}
public partial class PlayerData
{
    [System.Serializable]
    public struct NSJTest
    {
        public float MoveSpeed;
        public int Damage;
        public int MaxThrowCount;
        public int CurThrowCount;
        public List<ThrowObjectData> ThrowObjectStack;
        public List<AdditionalEffect> AdditionalEffects; // ���Ĩ ���� ����Ʈ
        public List<HitAdditional> HitAdditionals;
        public List<ThrowAdditional> ThrowAdditionals; // ���� ��� �߰�ȿ�� ����Ʈ
        public List<PlayerAdditional> PlayerAdditionals; // �÷��̾� �߰�ȿ�� ����Ʈ
    }
    [SerializeField] private NSJTest _NSJTest;
    public float MoveSpeed { get { return _NSJTest.MoveSpeed; } set { _NSJTest.MoveSpeed = value; } }
    public int Damage { get { return _NSJTest.Damage; } set { _NSJTest.Damage = value; } }
    public int MaxThrowCount { get { return _NSJTest.MaxThrowCount; } set { _NSJTest.MaxThrowCount = value; } }
    public int CurThrowCount { get { return _NSJTest.CurThrowCount; } set { _NSJTest.CurThrowCount = value; } }
    public List<ThrowObjectData> ThrowObjectStack { get { return _NSJTest.ThrowObjectStack; } set { _NSJTest.ThrowObjectStack = value; } }
    public List<AdditionalEffect> AdditionalEffects { get { return _NSJTest.AdditionalEffects; } set { _NSJTest.AdditionalEffects = value; } }
    public List<HitAdditional> HitAdditionals { get { return _NSJTest.HitAdditionals; } set { _NSJTest.HitAdditionals = value; } }
    public List<ThrowAdditional> ThrowAdditionals { get { return _NSJTest.ThrowAdditionals; } set { _NSJTest.ThrowAdditionals = value; } } // ���� ��� �߰�ȿ�� ����Ʈ
    public List<PlayerAdditional> PlayerAdditionals { get { return _NSJTest.PlayerAdditionals; } set { _NSJTest.PlayerAdditionals= value; } } // �÷��̾� �߰�ȿ�� ����Ʈ
}
