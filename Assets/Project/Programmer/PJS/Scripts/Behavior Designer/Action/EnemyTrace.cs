using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class EnemyTrace : Action
{
    [SerializeField] SharedFloat speed;         // ���� �̵��ӵ�
    [SerializeField] SharedTransform player;    // �÷��̾�
    [SerializeField] SharedFloat traceDist;          // �ν� �Ÿ�
    [SerializeField] SharedFloat attackDis;

    public override TaskStatus OnUpdate()
    {
        float dir = (player.Value.position - transform.position).magnitude;
        
        if (dir < attackDis.Value)
            return TaskStatus.Success;
        else if(dir > traceDist.Value)
            return TaskStatus.Failure;

        transform.position = Vector3.MoveTowards(transform.position, player.Value.position, speed.Value * Time.deltaTime);
        transform.LookAt(player.Value);
        return TaskStatus.Running;
    }
}
