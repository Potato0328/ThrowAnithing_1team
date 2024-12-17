using System.Collections;
using UnityEngine;

public class MeleeAttackState : PlayerState
{
    private float _atttackBufferTime;
    private bool _isCombe;
    private bool _isChangeAttack;
    public MeleeAttackState(PlayerController controller) : base(controller)
    {
        _atttackBufferTime = _player.AttackBufferTime;
    }

    public override void Enter()
    {
        _isChangeAttack = false;
        if (_player.View.GetBool(PlayerView.Parameter.MeleeCombo) == false)
        {
            _player.View.SetTrigger(PlayerView.Parameter.MeleeAttack);
        }
        else
        {
            _player.Model.MeleeComboCount++;
        }
        CoroutineHandler.StartRoutine(MeleeAttackRoutine());
    }

    public override void Update()
    {
        //Debug.Log("Melee");
    }

    public override void Exit()
    {

    }

    IEnumerator MeleeAttackRoutine()
    {
        if (_player.IsAttackFoward == true)
        {
            // ī�޶� �������� �÷��̾ �ٶ󺸰�
            Quaternion cameraRot = Quaternion.Euler(0, _player.CamareArm.eulerAngles.y, 0);
            _player.transform.rotation = cameraRot;
            // ī�޶�� �ٽ� ���� ���� ���� ����
            if (_player.CamareArm.parent != null)
            {
                _player.CamareArm.localRotation = Quaternion.Euler(_player.CamareArm.localRotation.eulerAngles.x, 0, 0);
            }
        }

        yield return null;
        float timeCount = _atttackBufferTime;
        while (_player.View.IsAnimationFinish == false)
        {       
            // ���� ����
            if (Input.GetButtonDown("Fire1"))
            {
                // ���� ���� ���
                _isCombe = true;
                _player.View.SetBool(PlayerView.Parameter.MeleeCombo, true);
                timeCount = _atttackBufferTime;
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                // ������ ���� ��ȯ
                _isCombe = false;
                _isChangeAttack = true;
                _player.View.SetBool(PlayerView.Parameter.MeleeCombo, false);
                timeCount = _atttackBufferTime;
            }

            // ���� Ÿ�̸�
            timeCount -= Time.deltaTime;
            if (timeCount <= 0)
            {
                // ���� ���� ���
                _isCombe = false;
                _player.View.SetBool(PlayerView.Parameter.MeleeCombo, false);
                timeCount = _atttackBufferTime;
            }

            yield return null;
        }

        if (_isCombe == true)
        {
            _player.ChangeState(PlayerController.State.MeleeAttack);
        }
        else if (_isChangeAttack == true)
        {
            _player.Model.MeleeComboCount = 0;
            _player.ChangeState(PlayerController.State.ThrowAttack);
        }
        else
        {
            _player.Model.MeleeComboCount = 0;
            _player.ChangeState(PlayerController.State.Idle);
        }

    }
}
