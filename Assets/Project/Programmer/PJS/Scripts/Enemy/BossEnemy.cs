using BehaviorDesigner.Runtime;
using System.Collections;
using UnityEngine;

public class BossEnemy : BaseEnemy
{
    public enum Phase { Phase1, Phase2, Phase3 }
    public Phase curPhase = Phase.Phase1;
    //[SerializeField] BossEnemyState bossState;
    [SerializeField] ParticleSystem shieldParticle;

    private Coroutine attackAble;
    private bool onFrezenyPassive = false;


    private void Update()
    {
        ChangePhase();
        //FrenzyPassive();
    }

    private void ChangePhase()
    {
        // ���� ü������ ������ ����
        if (CurHp > state.MaxHp * 0.8f)
        {
            Debug.Log("curHp > 80");
            curPhase = Phase.Phase1;
        }
        else if (CurHp <= state.MaxHp * 0.8f && CurHp > state.MaxHp * 0.5f)
        {
            Debug.Log("80 >= curHP > 50");
            curPhase = Phase.Phase2;
        }
        else
        {
            Debug.Log("curHP <= 50");
            curPhase = Phase.Phase3;
            onFrezenyPassive = true;
        }
    }

    private void FrenzyPassive()
    {
        if (onFrezenyPassive == false)
            return;

        Debug.Log($"{MoveSpeed}, {AttackSpeed}");

        MoveSpeed = MoveSpeed + (MoveSpeed * 0.2f);
        AttackSpeed = AttackSpeed - (AttackSpeed * 0.2f);

        Debug.Log($"{MoveSpeed}, {AttackSpeed}");
    }

    /// <summary>
    /// Move �ִϸ��̼� �̺�Ʈ
    /// </summary>
    public void FootStep()
    {
        Debug.Log("FootSteop()");
    }

    /// <summary>
    /// Attack �ִϸ��̼� �̺�Ʈ
    /// </summary>
    public void OnHitBegin()
    {
        Debug.Log("OnHitBegin()");
    }
    public void OnHitEnd()
    {
        Debug.Log("OnHitEnd()");
    }

    /// <summary>
    /// Attack_3 �ִϸ��̼� �̺�Ʈ
    /// </summary>
    public void ThunderStomp()
    {
        Debug.Log("ThunderStomp()");
        // ü���� ���� ���� ����
        if (CurHp > state.MaxHp * 0.8f)
        {
            // 1������ - �Ϸ�Ʈ�� �Ƹ�
            Debug.Log("curHp > 80");
            shieldParticle.Play();
        }
        else if(CurHp <= state.MaxHp * 0.8f && CurHp > state.MaxHp * 0.5f)
        {
            // 2������ - ������ ����
            Debug.Log("80 >= curHP > 50");
        }

    }

    /// <summary>
    /// ��� �ִϸ��̼� �̺�Ʈ
    /// </summary>
    public void DieEff()
    {
        Debug.Log("DieEff()");
    }
    public void ShakingForAni()
    {
        Debug.Log("ShakingForAni()");
    }

    /// <summary>
    /// �Ϲ� ���� �ִϸ��̼� �̺�Ʈ
    /// </summary>
    public void Shooting()
    {
        Debug.Log("Shooting()");
        // �Ϲ� ���� ���� - ��� ����� ����
        // ����Ʈ�� �ǽ�Ʈ - 1������� ����
    }

    /// <summary>
    /// ���� ���� ���� Ȯ��
    /// </summary>
    public void AttackAble()
    {
        if (attackAble != null)
            return;

        attackAble = StartCoroutine(AttackDelayRoutine());
    }

    /// <summary>
    /// ���� ������
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackDelayRoutine()
    {
        // ���� ������ ����
        tree.SetVariableValue("AttackAble", false);
        Debug.Log("���� ������ ����");
        yield return state.AtkDelay.GetDelay();
        // ���� ������ ��
        tree.SetVariableValue("AttackAble", true);
        Debug.Log("���� ������ ��");
    }

    /// <summary>
    /// ���� ��Ÿ�� ���� �ڷ�ƾ
    /// </summary>
    /// <param name="atkAble">�ش� ��ų�� bool Ÿ��</param>
    /// <param name="coolTime">��Ÿ�� �ð�</param>
    public IEnumerator CoolTimeRoutine(SharedBool atkAble, float coolTime)
    {
        atkAble.SetValue(false);
        Debug.Log("��Ÿ�� ����");
        yield return coolTime.GetDelay();
        atkAble.SetValue(true);
        Debug.Log("��Ÿ�� ��");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (true == Physics.BoxCast(transform.position, transform.lossyScale / 2.0f, transform.forward, out RaycastHit hit, transform.rotation, 8))
        {
            // Hit�� �������� ray�� �׷��ش�.
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);

            // Hit�� ������ �ڽ��� �׷��ش�.
            Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, transform.lossyScale);
        }
        else
        {
            // Hit�� ���� �ʾ����� �ִ� ���� �Ÿ��� ray�� �׷��ش�.
            Gizmos.DrawRay(transform.position, transform.forward * 8);
        }

        // �Ÿ� �׸���
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, state.TraceDis);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, state.AttackDis);
    }
}
