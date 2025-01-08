using UnityEngine;

public class HitState : PlayerState
{
    public HitState(PlayerController controller) : base(controller)
    {
        Player.OnPlayerHitFuncEvent += TakeDamage;
    }


    public override void Enter()
    {
        // ������ ����
        Rb.velocity = new Vector3(0, Rb.velocity.y, 0);

        // �÷��̾� ��������
        Player.IsInvincible = true;

        View.SetTrigger(PlayerView.Parameter.Hit);
    }

    public override void Exit()
    {
        // �÷��̾� ���� ����
        Player.IsInvincible = false;

        if (Player.IsGround == true)
        {
            // ���� ���̾������� ���� ���� ����
            Player.IsJumpAttack = false;
            Player.IsDoubleJump = false;
        }
    }

    public override void EndAnimation()
    {
        ChangeState(PlayerController.State.Idle);
    }

    private int TakeDamage(int damage, bool isIgnoreDef, CrowdControlType type)
    {
        if (Player.IsShield == true)
            return 0;
        if (Player.IsInvincible == true)
            return 0;
        int finalDamage = 0;
        // ���������� �ƴ� �� ���� ���
        if (isIgnoreDef == false)
        {
            // ���� ���
            finalDamage = damage - Model.Defense;
            // �޴� ���� ���� ���
            finalDamage = (int)(finalDamage * (1 - Model.DamageReduction / 100f));
            // 0���� ������ ���� 0���� ���� ��Ŵ
            finalDamage = finalDamage <= 0 ? 0 : finalDamage;
        }
        else
        {
            finalDamage = damage;
        }
        // ���� �������� ������ Hit ���� �ȵ�
        if (finalDamage == 0)
            return finalDamage;

        Model.CurHp -= finalDamage;


        if (Model.CurHp > 0)
        {
            // ���� ����
            if (type == CrowdControlType.Stiff)
            {
                ChangeState(PlayerController.State.Hit);
            }
        }
        else
        {
            // Die
            Player.Die();
        }
        return finalDamage;
    }

}
