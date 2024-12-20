using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class EnemyAttack : Action
{
    [SerializeField] Animator anim;

    public override void OnAwake()
    {
        anim = GetComponent<Animator>();
    }

    public override TaskStatus OnUpdate()
    {
        // TODO : ���� �ִϸ��̼� Ȯ�� �� �ۼ�Ʈ ���ϱ�
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Zombie Attack") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            anim.SetBool("Attack", false);
            return TaskStatus.Success;
        }

        anim.SetBool("Attack", true);
        return TaskStatus.Running;
    }
}