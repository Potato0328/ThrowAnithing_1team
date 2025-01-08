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
    public SharedBool able;  // ��� ���� Ȯ��

    public override void OnStart()
    {
        enemy = GetComponent<BossEnemy>();
        enemy.createShield = true;
        minRecoveryPersent = (maxAddHp.Value / 100f) / maxTime.Value;
    }

    public override TaskStatus OnUpdate()
    {
        if (enemy.recovery != null && enemy.createShield == true)
        {
            return TaskStatus.Running;
        }
        else if (enemy.createShield == false)
        {
            // ȸ���� ���� -> �Ϲ� ���·� ��ȯ => �ڷ�ƾ���� �ذ���
            // �ǵ尡 ���� -> �׷α� ���·� ��ȯ
            able.SetValue(true);
            return TaskStatus.Failure;
        }
        
        enemy.RecoveryStartCoroutine(maxTime.Value, minRecoveryPersent);
        return TaskStatus.Success;
    }
}