using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class JumpAttack : Action
{
    public SharedGameObject player; // �÷��̾�
    public float dis;  // �� �� �ڽ��� ��ġ�� �÷��̾��� ��ġ ����
    public string testText;
    public Vector3 playerPos;   // �̵��� �÷��̾��� ��ġ

    private BossEnemy enemy;

    public override void OnAwake()
    {
        enemy = GetComponent<BossEnemy>();
    }

    public override void OnStart()
    {
        enemy.SetPlayerPos(player.Value.transform.position);
    }

    public override TaskStatus OnUpdate()
    {
        dis = (transform.position - playerPos).sqrMagnitude;

        if (dis <= 0.1f)
        {
            testText = "����";
            return TaskStatus.Success;
        }
        else
        {
            testText = "������";
            return TaskStatus.Running;
        }
    }
}