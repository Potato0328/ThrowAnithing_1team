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

    private void FrenzyPassive()
    {
        if (onFrezenyPassive == false)
            return;

        Debug.Log($"{MoveSpeed}, {AttackSpeed}");

        MoveSpeed = MoveSpeed + (MoveSpeed * 0.2f);
        AttackSpeed = AttackSpeed - (AttackSpeed * 0.2f);

        Debug.Log($"{MoveSpeed}, {AttackSpeed}");
    }

    IEnumerator PassiveOn()
    {
        while (true)
        {
            if (curPhase == Phase.Phase3)
            {
                FrenzyPassive();
                yield break;
            }

            yield return null;
        }
    }

    /// <summary>
    /// Move �ִϸ��̼� �̺�Ʈ
    /// </summary>
    public void FootStep()
    {
        //Debug.Log("FootSteop()");
    }

    /// <summary>
    /// Attack �ִϸ��̼� �̺�Ʈ
    /// </summary>
    public void OnHitBegin()
    {
        //Debug.Log("OnHitBegin()");
    }
    public void OnHitEnd()
    {
        //Debug.Log("OnHitEnd()");
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
            //Debug.Log("curHp > 80");
            shieldParticle.Play();
        }
        else if (CurHp <= state.MaxHp * 0.8f && CurHp > state.MaxHp * 0.5f)
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
        //Debug.Log("Shooting()");
        //AttackMelee();
        // �Ϲ� ���� ���� - ��� ����� ����
        // ����Ʈ�� �ǽ�Ʈ - 1������� ����
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void AttackMelee()
    {
        // ���� �տ� �ִ� ���͵��� Ȯ���ϰ� �ǰ� ����
        // 1. ���濡 �ִ� ���� Ȯ��
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 attackPos = playerPos;
        int hitCount = Physics.OverlapSphereNonAlloc(attackPos, state.AttackDis, overLapCollider, 1 << Layer.Player);
        for (int i = 0; i < hitCount; i++)
        {
            // 2. ���� ���� �ִ��� Ȯ��
            Vector3 source = transform.position;
            source.y = 0;
            Vector3 destination = overLapCollider[i].transform.position;
            destination.y = 0;
            Vector3 targetDir = (destination - source).normalized;
            float targetAngle = Vector3.Angle(transform.forward, targetDir); // ��ũ�ڻ��� �ʿ� (������)
            
            if (targetAngle > 120 * 0.5f)
                continue;

            int finalDamage = state.Atk;
            // ������ �ֱ�
            Battle.TargetAttackWithDebuff(overLapCollider[i], finalDamage, true);
        }
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
        Debug.Log($"{atkAble.Name} ��Ÿ�� ����");
        yield return coolTime.GetDelay();
        atkAble.SetValue(true);
        Debug.Log($"{atkAble.Name} ��Ÿ�� ��");
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
