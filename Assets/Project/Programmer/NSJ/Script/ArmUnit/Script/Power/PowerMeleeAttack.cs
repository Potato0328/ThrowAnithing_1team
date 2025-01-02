using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Power Melee", menuName = "Arm/AttackType/Power/Melee")]
public class PowerMeleeAttack : ArmMeleeAttack
{
    [System.Serializable]
    struct ChargeStruct
    {
        public float ChargeTime;
        public int Damage;
        public float AttackRange;
        [Range(0, 180)] public float AttackAngle;
        public float KnockBackRange;
        public float MovePower;
        [HideInInspector] public float Stamina;
        [HideInInspector] public GameObject ArmEffect;
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

    private GameObject _curArmEffect;
    Coroutine _chargeRoutine;
    public override void Init(PlayerController player)
    {
        base.Init(player);
        for (int i = 0; i < _charges.Length; i++)
        {
            _charges[i].Damage = (int)Model.PowerMeleeAttack[i];
            _charges[i].Stamina = Model.MeleeAttackStamina[i];
            _charges[i].ArmEffect = Binder.PowerMeleeEffect[i];
            _charges[i].ArmEffect.SetActive(false);
        }
    }

    public override void Enter()
    {
        // ����� ���׹̳���ŭ �ٽ� ȸ��
        Model.CurStamina += Model.MeleeAttackStamina[0];
        // �ִ� ���׹̳� �������� ����
        Model.MaxStaminaCharge = _charges[_charges.Length - 1].ChargeTime;
        // ��ġ ����
        Player.Rb.velocity = Vector3.zero;
        // ���׹̳� ȸ�� ����
        Player.CanStaminaRecovery = false;
        // ���ݹ��� �ٶ�
        Player.LookAtAttackDir();
        // �ִϸ��̼� ����
        View.SetTrigger(PlayerView.Parameter.PowerMelee);
        // ������ ���� ����Ʈ
        ShowArmEffect();
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
        // ���׹̳� �ٽ� ȸ�� ����
        Player.CanStaminaRecovery = true;

        // ������ ���� ����Ʈ ����
        _curArmEffect.SetActive(false);
        _curArmEffect = null;

        // �ʱ�ȭ
        _curChargeTime = 0;
        _index = 0;
        Player.IsInvincible = false;
    }
    public override void Update()
    {

    }

    public override void OnTrigger()
    {
        AttackMelee();
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

            if (InputKey.GetButtonUp(InputKey.Melee))
            {
                Player.IsInvincible = true;

                _chargeRoutine = null;
                // ���ݹ��� �ٶ󺸱�
                Player.LookAtAttackDir();
                // �ִϸ��̼� ����
                View.SetTrigger(PlayerView.Parameter.ChargeEnd);
                break;
            }
            yield return null;
        }
    }

    private void ProcessCharge()
    {
        // �����ð� ���
        _curChargeTime += Time.deltaTime * View.GetFloat(PlayerView.Parameter.AttackSpeed);
        if (_charges.Length > _index + 1)
        {
            // ���׹̳��� �����ϸ� ���� ����
            if (Model.CurStamina < _charges[_index + 1].Stamina)
            {
                ChargeEnd();
                return;
            }
            // ���� �ð��� ���� �ܰ�� �Ѿ �� ���� ��
            if (_curChargeTime > _charges[_index + 1].ChargeTime)
            {
                _index++;
                ShowArmEffect();
            }
            
        }
        else
        {
            ChargeEnd();
        }
    }
    public void AttackMelee()
    {
        // �ڿ��Ҹ� ó��
        Model.CurStamina -= _charges[_index].Stamina;

        Debug.Log(transform.name);
        // ĳ���� ���� ���� �̵�
        Rb.AddForce(Vector3.forward * _charges[_index].MovePower);

        // ���� �տ� �ִ� ���͵��� Ȯ���ϰ� �ǰ� ����
        // 1. ���濡 �ִ� ���� Ȯ��
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y + Player.AttackHeight, transform.position.z);
        Vector3 attackPos = playerPos;
        int hitCount = Physics.OverlapSphereNonAlloc(attackPos, _charges[_index].AttackRange, Player.OverLapColliders, 1 << Layer.Monster);
        for (int i = 0; i < hitCount; i++)
        {
            Transform targetTransform = Player.OverLapColliders[i].transform;
            // 2. ���� ���� �ִ��� Ȯ��
            Vector3 source = transform.position;
            source.y = 0;
            Vector3 destination = targetTransform.position;
            destination.y = 0;

            Vector3 targetDir = (destination - source).normalized;
            float targetAngle = Vector3.Angle(transform.forward, targetDir); // ��ũ�ڻ��� �ʿ� (������)
            if (targetAngle > _charges[_index].AttackAngle * 0.5f)
                continue;


            int attackDamage = Player.GetFinalDamage(_charges[_index].Damage);
            Player.Battle.TargetAttackWithDebuff(Player.OverLapColliders[i], attackDamage, true);

            if (_charges[_index].KnockBackRange > 0)
            {
                // �������� �б�
                Player.DoKnockBack(targetTransform, transform.forward, _charges[_index].KnockBackRange);
                // �÷��̾� �߽� �б�
                //Player.DoKnockBack(targetTransform, transform, _charges[_index].KnockBackRange);
            }

            if (_index == 0)
                break;
        }

    }
    public override void OnDrawGizmos()
    {
        if (Model == null)
            return;
        //�Ÿ�
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y + Player.AttackHeight, transform.position.z);
        Vector3 attackPos = playerPos;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos, _charges[_index].AttackRange);

        //����
        Vector3 rightDir = Quaternion.Euler(0, _charges[_index].AttackAngle * 0.5f, 0) * transform.forward;
        Vector3 leftDir = Quaternion.Euler(0, _charges[_index].AttackAngle * -0.5f, 0) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + rightDir * _charges[_index].AttackRange);
        Gizmos.DrawLine(transform.position, transform.position + leftDir * _charges[_index].AttackRange);
    }

    // ������ �� ����Ʈ ��Ÿ����
    private void ShowArmEffect()
    {
        if (_curArmEffect != null)
            _curArmEffect.SetActive(false);
        // ������ ����Ʈ
        _charges[_index].ArmEffect.SetActive(true);
        _charges[_index].ArmEffect.transform.SetParent(Player.ArmPoint, false);
        _curArmEffect = _charges[_index].ArmEffect;
    }

    private void ChargeEnd()
    {
        if (_chargeRoutine != null)
        {
            CoroutineHandler.StopRoutine(_chargeRoutine);
            _chargeRoutine = null;
        }
        Player.IsInvincible = true;

        _chargeRoutine = null;
        // ���ݹ��� �ٶ󺸱�
        Player.LookAtAttackDir();
        // �ִϸ��̼� ����
        View.SetTrigger(PlayerView.Parameter.ChargeEnd);
    }
}
