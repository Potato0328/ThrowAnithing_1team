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
        // �ӽ� ������ ����
        Vector3 tempVelocity = Rb.velocity;
        tempVelocity.y = 0;
        Rb.velocity = tempVelocity;

        Rb.AddForce(Vector3.up * Model.JumpPower, ForceMode.Impulse);
        // ���� ���� �ٷ� �߶� ��� ����
        ChangeState(PlayerController.State.Fall);
    }
}
