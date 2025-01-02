using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Power Throw", menuName = "Arm/AttackType/Power/Throw")]
public class PowerThrowAttack : ArmThrowAttack
{
    [System.Serializable]
    struct ChargeStruct
    {
        public float ChargeTime;
        public int ObjectCount;
        public int Damage;
        public float KnockBackDistance;
    }
    [SerializeField] private ChargeStruct[] _charges;
    private float m_curChargeTime;
    private float _curChargeTime
    {
        get { return m_curChargeTime; }
        set
        {
            m_curChargeTime = value;
            Model.CurStaminaCharge = m_curChargeTime;
            View.SetFloat(PlayerView.Parameter.Charge, m_curChargeTime);
        }
    }
    Coroutine _chargeRoutine;
    public override void Init(PlayerController player)
    {
        base.Init(player);
        for (int i = 0; i < _charges.Length; i++)
        {
            _charges[i].Damage = (int)Model.PowerThrowAttack[i];
        }
    }
    public override void Enter()
    {
        Player.Rb.velocity = Vector3.zero;
        Model.MaxStaminaCharge = _charges[_charges.Length - 1].ChargeTime;

        Player.LookAtCameraFoward();
        View.SetTrigger(PlayerView.Parameter.PowerThrow);
        if (_chargeRoutine == null)
        {
            _chargeRoutine = CoroutineHandler.StartRoutine(ChargeRoutine());
        }
    }
    public override void Exit()
    {
        if (_chargeRoutine != null)
        {
            CoroutineHandler.StopRoutine(_chargeRoutine);
            _chargeRoutine = null;
        }

        _curChargeTime = 0;
        _index = 0;
        // ĳ���� �ӽ� ����
        Player.IsInvincible = false;
    }
    public override void Update()
    {

    }

    public override void OnTrigger()
    {
        ThrowObject();
    }
    public override void EndCombo()
    {
        ChangeState(PlayerController.State.Idle);
    }

    IEnumerator ChargeRoutine()
    {
        _index = 0;
        while (true)
        {
            ProcessCharge();

            // ���� ���� �� ������ �ִϸ��̼� ����
            if (InputKey.GetButtonUp(InputKey.Throw))
            {
                Player.LookAtAttackDir();
                View.SetTrigger(PlayerView.Parameter.ChargeEnd);
                _chargeRoutine = null;
                // ĳ���� �ӽ� ����
                Player.IsInvincible = true;
                break;
            }
            yield return null;
        }
    }
    private void ThrowObject()
    {
        int throwObjectID = Model.ThrowObjectStack.Count > 0 && _index > 0 ? Model.PopThrowObject().ID : 0;

        ThrowObject throwObject = Player.InstantiateObject(DataContainer.GetThrowObject(throwObjectID), _muzzlePoint.position, _muzzlePoint.rotation);
        throwObject.Init(Player, Model.ThrowAdditionals);

        // �˹鰡���ϸ� �˹�
        if (_charges[_index].KnockBackDistance > 0)
        {
            throwObject.KnockBackDistance = _charges[_index].KnockBackDistance;
        }

        //TODO : ������ ���� ���� �ʿ�
        throwObject.Damage = Player.GetFinalDamage(_charges[_index].Damage);
        UseThrowObject(_charges[_index].ObjectCount);
        throwObject.Shoot(Player.ThrowPower);
        throwObject.TriggerFirstThrowAddtional();
    }

    private void ProcessCharge()
    {
        // �����ð� ���
        _curChargeTime += Time.deltaTime * View.GetFloat(PlayerView.Parameter.AttackSpeed);
        if (_charges.Length > _index + 1)
        {
            // �Ҹ� ������Ʈ�� �����ϸ� ���� ����
            if (Model.ThrowObjectStack.Count <= _charges[_index].ObjectCount)
            {
                _curChargeTime = _charges[_index].ChargeTime;
                return;
            }
            // ���� �ð��� ���� �ܰ�� �Ѿ� �� �� ���� ��
            if (_curChargeTime > _charges[_index + 1].ChargeTime)
            {
                _index++;
            }
        }
        else
        {
            _curChargeTime = _charges[_index].ChargeTime + 0.01f;

        }
    }

    private void UseThrowObject(int count)
    {
        for (int i = 0; i < count - 1; i++)
        {
            if (Model.ThrowObjectStack.Count > 0)
            {
                Model.PopThrowObject();
            }
        }
    }
}
