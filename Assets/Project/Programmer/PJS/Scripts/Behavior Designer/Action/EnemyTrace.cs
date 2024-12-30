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

        // x,z�ุ ����
        Vector3 movePos = new(
            player.Value.position.x,
            transform.position.y,
            player.Value.position.z);
        // ���� ĳ���Ϳ� �ʹ� �� �������� ���� �ذ��?
        if ((movePos - transform.position).magnitude > attackDis.Value - 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePos, speed.Value * Time.deltaTime);
        }
       // transform.position = Vector3.MoveTowards(transform.position, movePos, speed.Value * Time.deltaTime);

        transform.LookAt(player.Value);
        // ���� �ٽ� ����ֱ�
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        return TaskStatus.Running;
    }
}
