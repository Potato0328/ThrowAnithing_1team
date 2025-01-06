using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using static BossEnemy;

public class SetPhase : Action
{
    private BossEnemy enemy;

    public override void OnStart()
    {
        enemy = GetComponent<BossEnemy>();
    }

    public override TaskStatus OnUpdate()
    {
        Debug.Log("Ȯ�ο�");
        // ���� ü������ ������ ����
        if (enemy.CurHp > enemy.MaxHp * 0.8f)
        {
            enemy.curPhase = PhaseType.Phase1;
        }
        else if (enemy.CurHp <= enemy.MaxHp * 0.8f && enemy.CurHp > enemy.MaxHp * 0.5f)
        {
            enemy.curPhase = PhaseType.Phase2;
        }
        else if (enemy.CurHp <= enemy.MaxHp * 0.5f && enemy.CurHp > enemy.MaxHp * 0.3f)
        {
            enemy.curPhase = PhaseType.Phase3;
        }

        return TaskStatus.Failure;
    }
}