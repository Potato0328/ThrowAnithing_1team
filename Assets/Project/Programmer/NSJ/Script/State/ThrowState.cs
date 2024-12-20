using System.Collections;
using UnityEngine;

public class ThrowState : PlayerState
{
    private Transform _muzzlePoint;
    private float _atttackBufferTime;
    private bool m_isCombo;
    private bool _isCombo
    {
        get { return m_isCombo; }
        set
        {
            m_isCombo = value;
        }
    }
    private bool _isChangeAttack;

    Coroutine _throwRoutine;
    public ThrowState(PlayerController controller) : base(controller)
    {
        _atttackBufferTime = Player.AttackBufferTime;
        _muzzlePoint = controller.MuzzletPoint;

    }
    public override void Enter()
    {
        Player.Rb.velocity = Vector3.zero;
        _isChangeAttack = false;

        // ù ���� �� ù ���� �ִϸ��̼� ����
        if (Player.PrevState != PlayerController.State.ThrowAttack)
        {
            View.SetTrigger(PlayerView.Parameter.ThrowAttack);
        }
        else
        {
            View.SetTrigger(PlayerView.Parameter.OnCombo);
        }

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
    }

    public override void Update()
    {
        //Debug.Log("Melee");
    }

    public override void Exit()
    {
        if (_throwRoutine != null)
        {
            CoroutineHandler.StopRoutine(_throwRoutine);
            _throwRoutine = null;
        }
    }
    public override void OnDash()
    {
        _isCombo = false;
    }

    /// <summary>
    /// ������Ʈ ������ ����
    /// </summary>
    public override void OnThrowAttack()
    {
        if (Model.ThrowObjectStack.Count > 0)
        {
            ThrowObjectData data = Model.PopThrowObject();
            ThrowObject(data.ID);
        }
        else
        {
            ThrowObject(0);
        }
    }

    private void ThrowObject(int throwObjectID)
    {
        ThrowObject throwObject = Player.InstantiateObject(DataContainer.GetThrowObject(throwObjectID), _muzzlePoint.position, _muzzlePoint.rotation);
        throwObject.Init(Model.Damage, Model.BoomRadius, Model.HitAdditionals);
        throwObject.Shoot();
    }

    public override void OnCombo()
    {
        if (_throwRoutine == null)
        {
            _throwRoutine = CoroutineHandler.StartRoutine(OnComboRoutine());
        }
    }

    public override void EndCombo()
    {
        ChangeState(PlayerController.State.Idle);
    }



    IEnumerator OnComboRoutine()
    {
        while (true)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                ChangeState(PlayerController.State.ThrowAttack);
            }
            else if (Input.GetButtonDown("Fire1"))
            {
                ChangeState(PlayerController.State.MeleeAttack);
            }
            yield return null;
        }
    }
}
