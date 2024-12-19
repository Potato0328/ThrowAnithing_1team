using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using UnityEngine;

public class MeleeAttackState : PlayerState
{
    private float _atttackBufferTime;
    private bool m_isCombo;
    private bool _isCombo
    {
        get { return m_isCombo; }
        set
        {
            m_isCombo = value;
            View.SetBool(PlayerView.Parameter.MeleeCombo, m_isCombo);
        }
    }
    private bool _isChangeAttack;
    private float _attackHeight; 

    Coroutine _meleeRoutine;
    public MeleeAttackState(PlayerController controller) : base(controller)
    {
        _atttackBufferTime = Player.AttackBufferTime;
        _attackHeight = Player.AttackHeight;

        View.OnMeleeAttackEvent += AttackMelee;
    }

    public override void Enter()
    {
        Player.Rb.velocity = Vector3.zero;
        _isChangeAttack = false;

        if (_isCombo == false)
        {
            // ù ������ ��� �������� �ִϸ��̼� ����
            View.SetTrigger(PlayerView.Parameter.MeleeAttack);
        }
        else
        {
            Model.MeleeComboCount++;
        }

        if(_meleeRoutine == null)
        {
            _meleeRoutine = CoroutineHandler.StartRoutine(MeleeAttackRoutine());
        }
      
    }

    public override void Update()
    {
        //Debug.Log("Melee");
    }

    public override void Exit()
    {
        if (_meleeRoutine != null) 
        {
            CoroutineHandler.StopRoutine(_meleeRoutine);
            _meleeRoutine = null;
        }

    }

    public override void OnDash()
    {
        _isCombo = false;
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void AttackMelee()
    {
        // ���� �տ� �ִ� ���͵��� Ȯ���ϰ� �ǰ� ����
        // 1. ���濡 �ִ� ���� Ȯ��
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y + _attackHeight, transform.position.z);
        Vector3 attackPos = playerPos;
        int hitCount = Physics.OverlapSphereNonAlloc(attackPos, Model.Range, Player.OverLapColliders, 1 << 4);
        for (int i = 0; i < hitCount; i++)
        {
            // 2. ���� ���� �ִ��� Ȯ��
            Vector3 source = transform.position;
            source.y = 0;
            Vector3 destination = Player.OverLapColliders[i].transform.position;
            destination.y = 0;

            Vector3 targetDir = (destination - source).normalized;
            float targetAngle = Vector3.Angle(transform.forward, targetDir); // ��ũ�ڻ��� �ʿ� (������)
            if (targetAngle > Model.Angle * 0.5f)
                continue;

            IHit hit = Player.OverLapColliders[i].GetComponent<IHit>();

            int attackDamage = (int)(Model.Damage * Model.DamageMultiplier);
            hit.TakeDamage(attackDamage);
        }
    }



    IEnumerator MeleeAttackRoutine()
    {
        if (Player.IsAttackFoward == true)
        {
            // ī�޶� �������� �÷��̾ �ٶ󺸰�
            Quaternion cameraRot = Quaternion.Euler(0, Player.CamareArm.eulerAngles.y, 0);
            transform.rotation = cameraRot;
            // ī�޶�� �ٽ� ���� ���� ���� ����
            if (Player.CamareArm.parent != null)
            {
                Player.CamareArm.localRotation = Quaternion.Euler(Player.CamareArm.localRotation.eulerAngles.x, 0, 0);
            }
        }

        yield return null;
        float timeCount = _atttackBufferTime;
        while (View.GetIsAnimFinish(PlayerView.Parameter.MeleeAttack) == false)
        {       
            // ���� ����
            if (Input.GetButtonDown("Fire1"))
            {
                // ���� ���� ���
                _isCombo = true;
                timeCount = _atttackBufferTime;
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                // ������ ���� ��ȯ
                _isCombo = false;
                _isChangeAttack = true;
                timeCount = _atttackBufferTime;
            }

            // ���� Ÿ�̸�
            timeCount -= Time.deltaTime;
            if (timeCount <= 0)
            {
                // ���� ���� ���
                _isCombo = false;
                timeCount = _atttackBufferTime;
            }

            yield return null;
        }
        
        // �޺����Է� �Ǿ����� �ٽ� ���� ���� 
        if (_isCombo == true)
        {         
            ChangeState(PlayerController.State.MeleeAttack);
        }
        // ���� ����� �ٲ���� �� ��ô ����
        else if (_isChangeAttack == true)
        {
            ChangeState(PlayerController.State.ThrowAttack);
        }
        // �ƹ� �Էµ� ������ �� ���ø��
        else
        {
            ChangeState(PlayerController.State.Idle);
        }
    }

    public override void OnDrawGizmos()
    {
        if (Model == null)
            return;
        //�Ÿ�
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y + _attackHeight, transform.position.z);
        Vector3 attackPos = playerPos;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos, Model.Range);

        //����
        Vector3 rightDir = Quaternion.Euler(0, Model.Angle * 0.5f, 0) * transform.forward;
        Vector3 leftDir = Quaternion.Euler(0, Model.Angle * -0.5f, 0) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + rightDir * Model.Range);
        Gizmos.DrawLine(transform.position, transform.position + leftDir * Model.Range);
    }
}
