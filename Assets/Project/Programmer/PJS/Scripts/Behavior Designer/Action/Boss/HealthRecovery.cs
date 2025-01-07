using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using UnityEngine;

public class HealthRecovery : Action
{
    public SharedInt maxTime = 15;   // �ִ� �ð� n��
    public SharedInt maxAddHp = 10;    // ȸ�� �Ǵ� �ִ� ��ġ n%

    private BossEnemy enemy;
    private float minRecoveryPersent;  // �ּ� ȸ���ϴ� �ۼ�Ʈ ex) �ʴ� n%

    public override void OnStart()
    {
        enemy = GetComponent<BossEnemy>();

        minRecoveryPersent = (maxAddHp.Value / 100f) / maxTime.Value;
    }

    public override TaskStatus OnUpdate()
    {
        StartCoroutine(RecoveryRoutin());

        return TaskStatus.Success;
    }

    IEnumerator RecoveryRoutin()
    {
        int time = maxTime.Value;
        int recoveryHp = Mathf.RoundToInt(enemy.GetState().MaxHp * minRecoveryPersent);
        Debug.Log(time);
        while (time > 0)    // ȸ�� �ϴ� �ð�
        {
            yield return 1f.GetDelay();
            enemy.CurHp += recoveryHp;
            time--;
            Debug.Log("ȸ��");
        }
    }
}