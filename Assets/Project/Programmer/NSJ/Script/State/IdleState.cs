using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(PlayerController controller) : base(controller)
    {

    }

    public override void Enter()
    {
        Player.Rb.velocity = Vector3.zero;
        View.SetBool(PlayerView.Parameter.Idle, true);
    }

    public override void Update()
    {
        //Debug.Log("Idle");

        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        // �̵�Ű �Է½� Run
        if (moveDir != Vector3.zero)
        {
            ChangeState(PlayerController.State.Run);
        }
        // 1�� ����Ű �Է½� ��������
        else if (Input.GetButtonDown("Fire1"))
        {
            ChangeState(PlayerController.State.MeleeAttack);
        }
        // 2�� ����Ű �Է½� ��ô ����
        else if (Input.GetButtonDown("Fire2"))
        {
            ChangeState(PlayerController.State.ThrowAttack);
        }
        // ���鿡�� ���� Ű �Է� �� ����
        else if (Player.IsGround == true && Input.GetButtonDown("Jump"))
        {
            ChangeState(PlayerController.State.Jump);
        }
        // ���߿��� y�� ������ �����϶� �߶�
        else if (Player.IsGround == false && Rb.velocity.y <= -1f)
        {
            ChangeState(PlayerController.State.Fall);
        }
    }
    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {
        View.SetBool(PlayerView.Parameter.Idle, false);
    }
}
