using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class EnemyTrace : Action
{
    [SerializeField] SharedFloat speed;         // ���� �̵��ӵ�
    [SerializeField] SharedTransform player;    // �÷��̾�
    [SerializeField] SharedFloat dist;

    public override TaskStatus OnUpdate()
    {
        float dir = (player.Value.position - transform.position).magnitude;
        
        if (dir < 1f)
            return TaskStatus.Success;
        else if(dir > dist.Value)
            return TaskStatus.Failure;

        transform.position = Vector3.MoveTowards(transform.position, player.Value.position, speed.Value * Time.deltaTime);
        transform.LookAt(player.Value);
        return TaskStatus.Running;
    }
}
