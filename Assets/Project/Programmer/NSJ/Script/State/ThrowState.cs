using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowState : PlayerState
{
    private Transform _muzzlePoint;
    private float _atttackBufferTime;
    private bool _isCombe;
    private bool _isChangeAttack;

    Coroutine _throwRoutine;
    public ThrowState(PlayerController controller) : base(controller)
    {
        _atttackBufferTime = Player.AttackBufferTime;
        _muzzlePoint = controller.MuzzletPoint;

        View.OnThrowAttackEvent += ThrowObject;
    }
    public override void Enter()
    {
        Player.Rb.velocity = Vector3.zero;
        _isChangeAttack = false;

        if (Player.PrevState != PlayerController.State.ThrowAttack)
        {
            View.SetBool(PlayerView.Parameter.ThrowCombo, false);
            Model.MeleeComboCount = 0;
        }
        else
        {
            View.SetBool(PlayerView.Parameter.ThrowCombo, true);
        }

        // ù ���� �� ù ���� �ִϸ��̼� ����
        if (View.GetBool(PlayerView.Parameter.ThrowCombo) == false)
        {
            View.SetTrigger(PlayerView.Parameter.ThrowAttack);
        }
        else
        {
            Model.MeleeComboCount++;
        }


        if(_throwRoutine == null)
        {
            _throwRoutine = CoroutineHandler.StartRoutine(MeleeAttackRoutine());
        }
        
    }

    public override void Update()
    {
        //Debug.Log("Melee");
    }

    public override void Exit()
    {
        if(_throwRoutine != null)
        {
            CoroutineHandler.StopRoutine(_throwRoutine);
            _throwRoutine = null;
        }
    }


    /// <summary>
    /// ������Ʈ ������ ����
    /// </summary>
    public void ThrowObject()
    {
        if (Model.ThrowObjectStack.Count > 0)
        {
            ThrowObjectData data = Model.PopThrowObject();
            ThrowObject throwObject = Player.InstantiateObject(DataContainer.GetThrowObject(data.ID), _muzzlePoint.position, _muzzlePoint.rotation);
            throwObject.Init(Model.Damage, Model.BoomRadius, Model.HitAdditionals);
            throwObject.Shoot();
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
        while (View.GetIsAnimFinish(PlayerView.Parameter.ThrowAttack) == false)
        {
            // ���� ����
            if (Input.GetButtonDown("Fire2"))
            {
                // ���� ���� ���
                _isCombe = true;
                timeCount = _atttackBufferTime;
            }
            else if (Input.GetButtonDown("Fire1"))
            {
                // ���� ���� ��ȯ
                _isCombe = false;
                _isChangeAttack = true;
                timeCount = _atttackBufferTime;
            }
            timeCount -= Time.deltaTime;
            if (timeCount <= 0)
            {
                // ���� ���� ���
                _isCombe = false;
                timeCount = _atttackBufferTime;
            }

            yield return null;
        }

        // �޺� ���Է��� �Ǿ��� �� �ٽ� ��ô ����
        if (_isCombe == true)
        {
            ChangeState(PlayerController.State.ThrowAttack);
        }
        // ���� Ű �Է��� �ٲ���� �� ��������
        else if (_isChangeAttack == true)
        {
            ChangeState(PlayerController.State.MeleeAttack);
        }
        // �ƹ� �Էµ� ������ �� ���� ���
        else
        {
            ChangeState(PlayerController.State.Idle);
        }

    }
}
