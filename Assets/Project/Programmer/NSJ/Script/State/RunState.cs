using UnityEngine;

public class RunState : PlayerState
{
    Vector3 _moveDir;
    public RunState(PlayerController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        Player.View.SetBool(PlayerView.Parameter.Run, true);
    }

    public override void Update()
    {
        //Debug.Log("Run");
        InputKey();
        CheckChangeState();
    }

    public override void FixedUpdate()
    {
        Run();
    }

    public override void Exit()
    {
        View.SetBool(PlayerView.Parameter.Run, false);
    }

    private void InputKey()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        _moveDir = new Vector3(x, 0, z);
    }

    private void Run()
    {
        Player.LookAtMoveDir(_moveDir);

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
        if (_moveDir == Vector3.zero)
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
