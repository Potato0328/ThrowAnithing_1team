using System.Collections;
using UnityEngine;

public class BossEnemy : BaseEnemy
{
    [SerializeField] BossEnemyState bossState;

    [HideInInspector] public Coroutine attackAble;

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
        // 1������ - �Ϸ�Ʈ�� �Ƹ�
        // 2������ - ������ ����
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
        //Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }
}
