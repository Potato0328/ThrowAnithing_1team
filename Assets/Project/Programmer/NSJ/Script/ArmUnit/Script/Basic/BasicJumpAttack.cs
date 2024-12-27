using UnityEngine;

[CreateAssetMenu(fileName = "Basic JumpAttack", menuName = "Arm/AttackType/Basic/JumpAttack")]
public class BasicJumpAttack : ArmJumpAttack
{
    [Header("�÷��̾ ������� ��¦ Ƣ������� ����")]
    [SerializeField] private float _popValue;
    [Header("�ϴ� ���� ����")]
    [SerializeField] private float m_downAngle;
    private float _downAngle {
        get
        {
            if (Player.IsVerticalCameraMove == true)
                return 0;
            else
                return m_downAngle;
        } 
    }
    public override void Enter()
    {
        Debug.Log("�������� ����");
        // �ִϸ��̼� ����
        View.SetTrigger(PlayerView.Parameter.JumpAttack);

    }
    public override void Exit()
    {
        Player.LookAtCameraFoward();
    }

    public override void OnTrigger()
    {
        // ĳ���� ��¦ Ƣ�������

        // �Է��� �������� �÷��̾� ���� ��ȯ
        Player.LookAtMoveDir();
        // �÷��̾� ������ ��ȯ
        Rb.velocity = new Vector3(Rb.velocity.x, 0, Rb.velocity.z);

        if (Player.MoveDir != Vector3.zero)
            Player.ChangeVelocityPlayerFoward();

        // �÷��̾� ī�޶� ���� �ٶ󺸱�
        Player.LookAtAttackDir();
        Rb.AddForce(Vector3.up * _popValue, ForceMode.Impulse);
        ThrowObject();
    }
    public override void EndAnimation()
    {
        ChangeState(PlayerController.State.Fall);
    }

    private void ThrowObject()
    {
        //int throwObjectID = Model.ThrowObjectStack.Count > 0 ? Model.PopThrowObject().ID : 0;
        Quaternion _muzzleRot = Quaternion.Euler(_muzzlePoint.eulerAngles.x + _downAngle, _muzzlePoint.eulerAngles.y, _muzzlePoint.eulerAngles.z);
        ThrowObject throwObject = Player.InstantiateObject(DataContainer.GetThrowObject(0), _muzzlePoint.position, _muzzleRot);
        throwObject.Init(Player, Model.ThrowAdditionals);
        throwObject.Shoot(Player.ThrowPower);
        throwObject.TriggerFirstThrowAddtional();
    }
}
