using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class EnemyTrace : Action
{
    [SerializeField] SharedFloat speed;         // 몬스터 이동속도
    [SerializeField] SharedTransform player;    // 플레이어
    [SerializeField] SharedFloat traceDist;          // 인식 거리
    [SerializeField] SharedFloat attackDis;

    public override TaskStatus OnUpdate()
    {
        float dir = (player.Value.position - transform.position).magnitude;
        
        if (dir < attackDis.Value)
            return TaskStatus.Success;
        else if(dir > traceDist.Value)
            return TaskStatus.Failure;

        // x,z축만 추적
        Vector3 movePos = new(
            player.Value.position.x,
            transform.position.y,
            player.Value.position.z);
        // 좀비가 캐릭터에 너무 딱 붙으려는 문제 해결용?
        /*if ((movePos - transform.position).magnitude > attackDis.Value - 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePos, speed.Value * Time.deltaTime);
        }*/
        transform.position = Vector3.MoveTowards(transform.position, movePos, speed.Value * Time.deltaTime);

        transform.LookAt(player.Value);
        return TaskStatus.Running;
    }
}
