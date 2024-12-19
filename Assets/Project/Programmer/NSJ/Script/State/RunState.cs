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
        // ī�޶� �������� �÷��̾ �ٶ󺸰�
        Quaternion cameraRot = Quaternion.Euler(0, Player.CamareArm.eulerAngles.y, 0);
        transform.rotation = cameraRot;
        // ī�޶�� �ٽ� ���� ���� ���� ����
        if (Player.CamareArm.parent != null)
        {
            // ī�޶� ��鸲 ���� ����ִ� �ڵ�
            Player.CamareArm.localPosition = new Vector3(0, Player.CamareArm.localPosition.y, 0);
            Player.CamareArm.localRotation = Quaternion.Euler(Player.CamareArm.localRotation.eulerAngles.x, 0, 0);
        }

        Player.CamareArm.SetParent(null);

        // �Է��� �������� �÷��̾ �ٶ�
        Vector3 moveDir = transform.forward * _moveDir.z + transform.right * _moveDir.x;
        if (moveDir == Vector3.zero)
            return;
        transform.rotation = Quaternion.LookRotation(moveDir);

        // �÷��̾� �̵�
        Vector3 originRb = Rb.velocity;
        Rb.velocity = transform.forward * Model.MoveSpeed;
        Rb.velocity = new Vector3(Rb.velocity.x, originRb.y, Rb.velocity.z);

        Player.CamareArm.SetParent(Player.transform);
    }

    private void CheckChangeState()
    {
        // �̵�Ű �Է��� ������ ���� ���
        if (_moveDir == Vector3.zero)
        {
            ChangeState(PlayerController.State.Idle);
        }
        // 1�� ����Ű �Է½� ���� ����
        else if (Input.GetButtonDown("Fire1"))
        {
            ChangeState(PlayerController.State.MeleeAttack);
        }
        // 2�� ����Ű �Է� �� ��ô ����
        else if (Input.GetButtonDown("Fire2"))
        {
            ChangeState(PlayerController.State.ThrowAttack);
        }
        // ���鿡�� ���� Ű �Է� �� ����
        else if (Player.IsGround == true && Input.GetButtonDown("Jump"))
        {
            ChangeState(PlayerController.State.Jump);
        }
        // ���߿��� ������ �� �߶�
        else if (Player.IsGround == false && Rb.velocity.y <= -2f)
        {
            ChangeState(PlayerController.State.Fall);
        }
    }

}
