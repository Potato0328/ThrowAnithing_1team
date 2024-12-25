using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(PlayerController controller) : base(controller)
    {

    }

    public override void Enter()
    {
        Player.Rb.velocity = Vector3.zero;
        if (Player.PrevState == PlayerController.State.Idle)
            return;
        View.SetTrigger(PlayerView.Parameter.Idle);
    }

    public override void Update()
    {
        // �̵�Ű �Է½� Run
        if (MoveDir != Vector3.zero)
        {
            ChangeState(PlayerController.State.Run);
        }
        // 1�� ����Ű �Է½� ��������
        else if (Input.GetKeyDown(KeyCode.V))
        {
            ChangeState(PlayerController.State.MeleeAttack);
        }
        // 2�� ����Ű �Է½� ��ô ����
        else if (Input.GetButtonDown("Fire1"))
        {
            ChangeState(PlayerController.State.ThrowAttack);
        }
        // Ư������ Ű �Է½� Ư�� ����
        else if (Input.GetButtonDown("Fire2"))
        {
            ChangeState(PlayerController.State.SpecialAttack);
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
        // �巹�� Ű�� ������ ���
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            ChangeState(PlayerController.State.Drain);
        }
    }
    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {
        
    }
}
