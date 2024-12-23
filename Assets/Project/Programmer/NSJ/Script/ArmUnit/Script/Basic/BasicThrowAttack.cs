using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Basic Throw", menuName = "Arm/AttackType/Basic/Throw")]
public class BasicThrowAttack : ArmThrowAttack
{
    Coroutine _throwRoutine;
    public override void Enter()
    {

        Player.Rb.velocity = Vector3.zero;

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
    public override void Exit()
    {
        if (_throwRoutine != null)
        {
            CoroutineHandler.StopRoutine(_throwRoutine);
            _throwRoutine = null;
        }
    }

    public override void OnTrigger()
    {
        ThrowObject();
    }
    public override void EndAnimation()
    {

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
        if (_throwRoutine != null)
        {
            ChangeState(PlayerController.State.Idle);
        }
    }



    private void ThrowObject()
    {
        int throwObjectID = Model.ThrowObjectStack.Count > 0 ? Model.PopThrowObject().ID : 0;

        ThrowObject throwObject = Player.InstantiateObject(DataContainer.GetThrowObject(throwObjectID), _muzzlePoint.position, _muzzlePoint.rotation);
        throwObject.Init(Player, Model.HitAdditionals, Model.ThrowAdditionals);
        throwObject.Shoot(Player.ThrowPower);
        throwObject.TriggerFirstThrowAddtional();
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
