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
        Player.Rb.velocity = Vector3.zero;
    }

    private void InputKey()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        _moveDir = new Vector3(x, 0, z);
    }

    private void Run()
    {
        // ī�޶� �������� �÷��̾ �ٶ󺸰�
        Quaternion cameraRot = Quaternion.Euler(0, Player.CamareArm.eulerAngles.y, 0);
        transform.rotation = cameraRot;
        // ī�޶�� �ٽ� ���� ���� ���� ����
        if (Player.CamareArm.parent != null)
        {
            Player.CamareArm.localRotation = Quaternion.Euler(Player.CamareArm.localRotation.eulerAngles.x, 0, 0);
        }
           

        Player.CamareArm.SetParent(null);

        // �Է��� �������� �÷��̾ �ٶ�
        Vector3 moveDir = transform.forward * _moveDir.z + transform.right * _moveDir.x;
        if (moveDir == Vector3.zero)
            return;
        transform.rotation = Quaternion.LookRotation(moveDir);

        // �÷��̾� �̵�
        Rb.velocity = transform.forward * Model.MoveSpeed;

        Player.CamareArm.SetParent(Player.transform);
    }

    private void CheckChangeState()
    {
        if (_moveDir == Vector3.zero)
        {
            Player.ChangeState(PlayerController.State.Idle);
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            Player.ChangeState(PlayerController.State.MeleeAttack);
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            Player.ChangeState(PlayerController.State.ThrowAttack);
        }
    }

}
