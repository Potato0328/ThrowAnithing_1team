using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class JumpAttack : Action
{
    public SharedGameObject player;
    public float dis;  // �� �� �ڽ��� ��ġ�� �÷��̾��� ��ġ ����
    public string testText;
    public float speed;
    public SharedVector3 playerPos;


    private Rigidbody rigid;
    private BossEnemy enemy;

    public override void OnAwake()
    {
        rigid = GetComponent<Rigidbody>();
        enemy = GetComponent<BossEnemy>();
    }

    public override void OnStart()
    {
        speed = enemy.GetState().Speed;
        
        //player.Value.transform.position;
        //new Vector3(player.Value.transform.position.x, transform.position.y, player.Value.transform.position.z);
    }

    public override TaskStatus OnUpdate()
    {
        dis = (transform.position - playerPos.Value).sqrMagnitude;

        if (dis <= 0.1f)
        {
            testText = "����";
            return TaskStatus.Success;
        }
        else
        {
            testText = "������";
        }
        //Vector3 jumpMove = Vector3.MoveTowards(transform.position, player.Value.transform.position, enemy.GetState().Speed * Time.deltaTime);
        //Debug.Log(jumpMove);
        //rigid.MovePosition(jumpMove);
        //transform.position = Vector3.MoveTowards(transform.position, playerPos, enemy.GetState().Speed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, playerPos.Value, enemy.GetState().Speed * Time.deltaTime);

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