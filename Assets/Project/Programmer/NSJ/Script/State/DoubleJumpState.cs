using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpState : PlayerState
{
    public DoubleJumpState(PlayerController controller) : base(controller)
    {
        UseStamina = true;
    }

    public override void Enter()
    {
        View.SetTrigger(PlayerView.Parameter.DoubleJump);
    }
    public override void Exit()
    {

    }

    public override void Update()
    {
        // Debug.Log("Jump");
    }

    public override void OnTrigger()
    {
        Jump();
    }

    private void Jump()
    {
        Player.LookAtMoveDir();

        // �ӽ� ������ ����
        Rb.velocity = new Vector3(Rb.velocity.x, 0, Rb.velocity.z); // y�� ������ ����
        Player.ChangeVelocityPlayerFoward();

        Rb.AddForce(Vector3.up * Model.JumpPower, ForceMode.Impulse);
        // ���� ���� �ٷ� �߶� ��� ����
        ChangeState(PlayerController.State.Fall);
    }

    private void Temp()
    {

    }
}
