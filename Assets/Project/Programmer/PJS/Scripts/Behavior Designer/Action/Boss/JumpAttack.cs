using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class JumpAttack : Action
{
    public SharedGameObject player;
    public float dis;  // �� �� �ڽ��� ��ġ�� �÷��̾��� ��ġ ����
    public string testText;
    public float speed;
    public Vector3 playerPos;

    private BossEnemy enemy;

    public override void OnAwake()
    {
        enemy = GetComponent<BossEnemy>();
    }

    public override void OnStart()
    {
        speed = enemy.GetState().Speed;
        playerPos = new Vector3(player.Value.transform.position.x, transform.position.y, player.Value.transform.position.z);
    }

    public override TaskStatus OnUpdate()
    {
        dis = (transform.position - playerPos).sqrMagnitude;

        if (dis <= 2f)
        {
            testText = "����";
            return TaskStatus.Success;
        }
        else
        {
            testText = "������";
        }

        //transform.position = Vector3.MoveTowards(transform.position, playerPos.Value, enemy.GetState().Speed * Time.deltaTime);
        transform.position = Vector3.Slerp(transform.position, playerPos, enemy.GetState().Speed * Time.deltaTime);
        return TaskStatus.Running;
    }

    public override void OnReset()
    {
        player = null;
        dis = 0;
        testText = null;
        speed = 0;
        playerPos = Vector3.zero;
    }
}