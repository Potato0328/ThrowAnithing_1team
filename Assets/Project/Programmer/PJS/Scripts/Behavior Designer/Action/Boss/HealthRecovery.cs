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
    private bool able;  // ��� ���� Ȯ��
    private Coroutine recovery;

    public override void OnStart()
    {
        enemy = GetComponent<BossEnemy>();
        enemy.createShield = true;
        minRecoveryPersent = (maxAddHp.Value / 100f) / maxTime.Value;
    }

    public override TaskStatus OnUpdate()
    {
        if (recovery != null && enemy.createShield == true)
            return TaskStatus.Running;
        else if (enemy.createShield == false)
        {
            // ȸ���� ���� -> �Ϲ� ���·� ��ȯ => �ڷ�ƾ���� �ذ���
            // �ǵ尡 ���� -> �׷α� ���·� ��ȯ
            if (enemy.breakShield == true)
            {
                StopCoroutine(RecoveryRoutin());
                Debug.Log(123);
            }
            return TaskStatus.Failure;
        }

        recovery = StartCoroutine(RecoveryRoutin());
        return TaskStatus.Success;
    }

    IEnumerator RecoveryRoutin()
    {
        int time = maxTime.Value;
        int recoveryHp = Mathf.RoundToInt(enemy.GetState().MaxHp * minRecoveryPersent);
        Debug.Log("ȸ�� ����");
        while (time > 0)    // ȸ�� �ϴ� �ð�
        {
            yield return 1f.GetDelay();
            enemy.CurHp += recoveryHp;
            time--;
            Debug.Log("ȸ�� ��...");
        }

        Debug.Log("ȸ�� ��");
        // ȸ�� ��
        enemy.createShield = false;
        transform.GetComponent<Animator>().SetBool("Recovery", false);
    }
}