using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerState
{
    private Vector3 _moveDir;
    public DashState(PlayerController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        InputKey();
        View.SetTrigger(PlayerView.Parameter.Dash);
    }

    public override void Update()
    {
        Dash();
        if(View.GetIsAnimFinish(PlayerView.Parameter.Dash) == true)
        {
            ChangeState(PlayerController.State.Idle);
        }
    }

    /// <summary>
    /// �뽬
    /// </summary>
    public void Dash()
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
        {
            moveDir = transform.forward;
        }
        transform.rotation = Quaternion.LookRotation(moveDir);
            
        Rb.velocity = transform.forward * Model.MoveSpeed * 3;
        Player.CamareArm.SetParent(transform);
    }

    private void InputKey()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        _moveDir = new Vector3(x, 0, z);
    }
}
