using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpState : PlayerState
{
    Vector3 _moveDir;
    public DoubleJumpState(PlayerController controller) : base(controller)
    {
        UseStamina = true;
    }

    public override void Enter()
    {
        CheckMoveInput();

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

    private void CheckMoveInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        _moveDir = new Vector3(x, 0, z);
    }

    private void Jump()
    {
        Player.LookAtMoveDir(_moveDir);

        // �ӽ� ������ ����
        Rb.velocity = new Vector3(Rb.velocity.x, 0, Rb.velocity.z); // y�� ������ ����
        Vector3 tempVelocity = transform.forward *Rb.velocity.magnitude; // x,z ���� �������� ���
        Rb.velocity = tempVelocity; // ����

        Rb.AddForce(Vector3.up * Model.JumpPower, ForceMode.Impulse);
        // ���� ���� �ٷ� �߶� ��� ����
        ChangeState(PlayerController.State.Fall);
    }

    private void Temp()
    {

    }
}
