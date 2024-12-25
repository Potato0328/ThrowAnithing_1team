using UnityEngine;

public class RunState : PlayerState
{
    public RunState(PlayerController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        View.SetTrigger(PlayerView.Parameter.Run);
    }

    public override void Update()
    {
        CheckChangeState();
    }

    public override void FixedUpdate()
    {
        Run();
    }

    public override void Exit()
    {
        
    }

    private void Run()
    { 
        Player.LookAtMoveDir();

        // �÷��̾� �̵�
        // ���� �ְ� ���� �ε����� ���� ���¿����� �̵�
        if (Player.IsGround == false && Player.IsWall == true)
            return;

        Vector3 originRb = Rb.velocity;
        Vector3 velocityDir = transform.forward * Model.MoveSpeed;
        Rb.velocity = new Vector3(velocityDir.x, originRb.y, velocityDir.z);
    }

    private void CheckChangeState()
    {
        // �̵�Ű �Է��� ������ ���� ���
        if (MoveDir == Vector3.zero)
        {
            ChangeState(PlayerController.State.Idle);
        }
        // 1�� ����Ű �Է½� ���� ����
        else if (Input.GetKeyDown(KeyCode.V))
        {
            ChangeState(PlayerController.State.MeleeAttack);
        }
        // 2�� ����Ű �Է� �� ��ô ����
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
        // ���߿��� ������ �� �߶�
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

}
