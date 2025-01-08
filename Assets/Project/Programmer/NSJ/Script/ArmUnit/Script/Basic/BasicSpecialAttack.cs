using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Basic Special", menuName = "Arm/AttackType/Basic/Special")]
public class BasicSpecialAttack : ArmSpecialAttack
{
    [System.Serializable]
    struct ChargeStruct
    {
        public GameObject DropObject;
        public Vector3 DropSize;
        public float ChargeTime;
        public float ChargeMana;
        public int ObjectCount;
        public Vector3 AttackOffset;
        public float Radius;
        public int Damage;
        public float KnockBackDistance;
    }
    [SerializeField] private ChargeStruct[] _charges;
    [SerializeField] private GameObject _specialRange;
    [SerializeField] private float _moveSpeedMultyPlier;

    private float _maxChargeTime => _charges[_charges.Length - 1].ChargeTime;
    private float _maxChargeMana => _charges[_charges.Length - 1].ChargeMana;
    private GameObject _instanceDropObject;
    private GameObject _instanceSpecialRange;
    private Vector3 _dropPos;
    Coroutine _chargeRoutine;

    public override void Init(PlayerController player)
    {
        base.Init(player);
        for (int i = 0; i < _charges.Length; i++)
        {
            _charges[i].Damage = (int)Model.PowerSpecialAttack[i];
            View.Panel.StepTexts[i].SetText(_charges[i].ObjectCount.GetText());
            _charges[i].ChargeMana = Model.ManaConsumption[i];
        }
    }
    public override void Enter()
    {
        if (Model.ThrowObjectStack.Count < _charges[_index].ObjectCount || Model.CurMana < Model.ManaConsumption[0])
        {
            ChangeState(Player.PrevState);
            return;
        }
        Player.Rb.velocity = Vector3.zero;

        // ��¡ ��� ����
        View.SetTrigger(PlayerView.Parameter.PowerSpecial);
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

        #region ���ݿ� ��ȯ�ߴ� �׷��� ������Ʈ ����
        if (_instanceDropObject != null)
        {
            Destroy(_instanceDropObject);
        }
        if (_instanceSpecialRange != null)
        {
            Destroy(_instanceSpecialRange);
        }
        #endregion

        Model.SpecialChargeGage = 0;
        _index = 0;
        Player.IsInvincible = false;
    }
    public override void Update()
    {

    }
    public override void OnTrigger()
    {
            AttackSpecial();     
    }
    public override void EndAnimation()
    {
        ChangeState(PlayerController.State.Idle);
    }
    IEnumerator ChargeRoutine()
    {
        while (true)
        {
            Move();
            ProcessCharge();

            if (InputKey.GetButtonUp(InputKey.Special))
            {
                Model.SpecialChargeGage = 0;
                if (_instanceDropObject)
                {
                    _instanceDropObject.transform.SetParent(Player.ArmPoint);
                }


                if (_index != 0)
                {
                    _index--;
                    Player.LookAtAttackDir();
                    View.SetTrigger(PlayerView.Parameter.ChargeEnd);
                }
                else
                {
                    View.SetTrigger(PlayerView.Parameter.ChargeCancel);
                    ChangeState(PlayerController.State.Idle);
                }
                _chargeRoutine = null;
                // ĳ���� �ӽ� ����
                Player.IsInvincible = true;
                break;
            }
            yield return null;
        }
    }
    private void ProcessCharge()
    {
        // �÷��̾� ���ݹ��� ��� �ٶ󺸱�
        Player.LookAtAttackDir();
        // ���ݹ��� ��ġ ���
        if (_instanceDropObject != null)
        {
            _dropPos = new Vector3(
                transform.position.x + (Player.transform.forward.x * _charges[_index - 1].AttackOffset.x),
                transform.position.y + 0.01f,
                transform.position.z + (Player.transform.forward.z * _charges[_index - 1].AttackOffset.z));
            _instanceSpecialRange.transform.position = _dropPos;
        }
        // ������ ȿ�� �� ����ٴϱ�
        if (_instanceDropObject != null)
        {
            _instanceDropObject.transform.position = Player.ArmPoint.position;
            _instanceDropObject.transform.rotation = Quaternion.LookRotation(transform.forward);
        }


        // �����ð� ���
        Model.SpecialChargeGage += Time.deltaTime * 100 / _maxChargeTime;
        // �ε����� �迭 ũ�⺸�� ��������
        if (_index < _charges.Length)
        {
            // �Ҹ� ������Ʈ�� ������ ���
            if (Model.ThrowObjectStack.Count >= _charges[_index].ObjectCount)
            {
                // ���� �ð��� ���� �ܰ� ��¡ ���ǽð��� �ѱ� ���
                if (Model.SpecialChargeGage >= _charges[_index].ChargeMana)
                {
                    // ������ �׷���
                    CreateSpecialObject();

                    CreateSpecialRange();

                    _index++;
                }
                // ���� Ư���ڿ������� �������� �� ���� ���
                else if (Model.SpecialChargeGage > Model.CurMana)
                {
                    Model.SpecialChargeGage = Model.CurMana;
                }
            }
            else
            {
                Model.SpecialChargeGage = _charges[_index - 1].ChargeMana;
            }
        }
        else
        {
            Model.SpecialChargeGage = _charges[_index - 1].ChargeMana;
        }
    }

    private void CreateSpecialObject()
    {
        if (_instanceDropObject != null)
            Destroy(_instanceDropObject);
        _instanceDropObject = Instantiate(_charges[_index].DropObject, Player.ArmPoint.position, transform.rotation);
        _instanceDropObject.transform.localScale = _charges[_index].DropSize;
    }
    private void CreateSpecialRange()
    {
        if (_instanceDropObject != null)
            Destroy(_instanceSpecialRange);
        // ���ݹ��� �׷���
        _dropPos = new Vector3(
              transform.position.x + (Player.CamareArm.forward.x * _charges[_index].AttackOffset.x),
              transform.position.y + 0.01f,
              transform.position.z + (Player.CamareArm.forward.z * _charges[_index].AttackOffset.z));
        _instanceSpecialRange = Instantiate(_specialRange, _dropPos, Quaternion.identity);
        // ũ�� ����
        _instanceSpecialRange.transform.localScale = new Vector3(
            _charges[_index].Radius * 2,
            _instanceSpecialRange.transform.localScale.y,
            _charges[_index].Radius * 2);
    }
    private void AttackSpecial()
    {
        int finalDamage = Player.GetFinalDamage(_charges[_index].Damage, out bool isCritical);
        // ���� �� ������ ������
        int hitCount = Physics.OverlapSphereNonAlloc(_dropPos, _charges[_index].Radius, Player.OverLapColliders, 1 << Layer.Monster);
        for (int i = 0; i < hitCount; i++)
        {
            // ������ �ֱ�
            Battle.TargetAttackWithDebuff(Player.OverLapColliders[i], finalDamage, CrowdControlType.Stiff, isCritical, false);

            // �˹� �����ϸ� �˹�
            if (_charges[_index].KnockBackDistance > 0)
                Player.DoKnockBack(Player.OverLapColliders[i].transform, transform, _charges[_index].KnockBackDistance);
        }
        // ���� ��뷮��ŭ ����
        Model.CurMana -= _charges[_index].ChargeMana;
        // ����� ������Ʈ��ŭ ����
        for (int i = 0; i < _charges[_index].ObjectCount; i++)
        {
            Model.PopThrowObject();
        }

        Destroy(_instanceSpecialRange);
    }

    private void Move()
    {
        if (Player.MoveDir == Vector3.zero)
            return;
        Player.LookAtMoveDir();

        // �÷��̾� �̵�
        // ���� �ְ� ���� �ε����� ���� ���¿����� �̵�
        if (Player.IsGround == false && Player.IsWall == true)
            return;
        Vector3 originRb = Rb.velocity;
        Vector3 velocityDir = transform.forward * (Model.MoveSpeed * _moveSpeedMultyPlier);
        Rb.velocity = new Vector3(velocityDir.x, originRb.y, velocityDir.z);
    }
}
