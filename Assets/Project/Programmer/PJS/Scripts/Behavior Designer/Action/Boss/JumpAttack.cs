using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class JumpAttack : Action
{
    public SharedGameObject player; // �÷��̾�
    public float dis;  // �� �� �ڽ��� ��ġ�� �÷��̾��� ��ġ ����

    private BossEnemy enemy;
    private Vector3 playerPos;   // �̵��� �÷��̾��� ��ġ

    public override void OnAwake()
    {
        enemy = GetComponent<BossEnemy>();
    }

    public override void OnStart()
    {
        playerPos = player.Value.transform.position;
        enemy.SetPlayerPos(playerPos);
    }

    public override TaskStatus OnUpdate()
    {
        dis = (transform.position - playerPos).sqrMagnitude;

        return dis <= 0.1f ? TaskStatus.Success : TaskStatus.Running;
    }
}