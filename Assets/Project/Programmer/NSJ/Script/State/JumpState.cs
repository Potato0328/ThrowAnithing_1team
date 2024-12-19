using System.Collections;
using UnityEngine;

public class JumpState : PlayerState
{
    private float _jumpPower;

    public JumpState(PlayerController controller) : base(controller)
    {
        View.OnJumpEvent += Jump;
        _jumpPower = controller.Model.JumpPower;
    }

    public override void Enter()
    {
        
        View.SetTrigger(PlayerView.Parameter.Jump);

    }
    public override void Exit()
    {

    }

    public override void Update()
    {
       // Debug.Log("Jump");
    }

    public override void FixedUpdate()
    {
      
    }

    private void Jump()
    {
        Rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        // ���� ���� �ٷ� �߶� ��� ����
        ChangeState(PlayerController.State.Fall);
    }
}
