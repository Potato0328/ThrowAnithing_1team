using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using Unity.Collections;
using UnityEngine;

public class HealthRecovery : Action
{
    public int addHp;
    public float maxTime = 15f;   // �ִ� �ð�()
    public float minTime = 3f;   // �ּ� �ð�
    public int maxAddHp = 10;    // ȸ�� �Ǵ� �ִ� ��ġ
    [ReadOnly] public int minAddHp;        // ȸ���ϴ� �ּ� ��ġ

    const float persent = 100f;
    private BossEnemy enemy;
    public float minRecoveryPersent;  // �ּ� ȸ���� �ۼ�Ʈ ex) �ʴ� n% ȸ��
    public float maxRecoveryPersent;  // �ִ� ȸ���� �ۼ�Ʈ ex) n%���� ȸ��

    public override void OnStart()
    {
        enemy = GetComponent<BossEnemy>();

        minAddHp = maxAddHp / (int)(maxTime / minTime);
        minRecoveryPersent = minAddHp / persent;
        maxRecoveryPersent = maxAddHp / persent;
    }

    public override TaskStatus OnUpdate()
    {
        StartCoroutine(RecoveryRoutin());

        return TaskStatus.Success;
    }

    IEnumerator RecoveryRoutin()
    {
        //while ()    // �� ȸ�� �Ǿ��°�?
        //{
        //    enemy.CurHp += (int)(enemy.GetState().MaxHp * 0.2f);
        //    yield return minTime.GetDelay();
        //}

        yield return minTime.GetDelay();
        enemy.CurHp += (int)(enemy.GetState().MaxHp * minRecoveryPersent);
        Debug.Log("�ð�����");
    }
}