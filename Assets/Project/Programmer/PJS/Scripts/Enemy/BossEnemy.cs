using UnityEngine;

public class BossEnemy : BaseEnemy
{
    [SerializeField] BossEnemyState bossState;

    public void FootStep()
    {
        Debug.Log("FootSteop()");
    }

    public void OnHitBegin()
    {
        Debug.Log("OnHitBegin()");
    }

    public void OnHitEnd()
    {
        Debug.Log("OnHitEnd()");
    }

    public void ThunderStomp()
    {
        Debug.Log("ThunderStomp()");
        // ü���� ���� ���� ����
        // 1������ - �Ϸ�Ʈ�� �Ƹ�
        // 2������ - ������ ����
    }

    public void DieEff()
    {
        Debug.Log("DieEff()");
    }

    public void ShakingForAni()
    {
        Debug.Log("ShakingForAni()");
    }

    public void Shooting()
    {
        Debug.Log("Shooting()");
        // �Ϲ� ���� ���� - ��� ����� ����
        // ����Ʈ�� �ǽ�Ʈ - 1������� ����
    }
}
