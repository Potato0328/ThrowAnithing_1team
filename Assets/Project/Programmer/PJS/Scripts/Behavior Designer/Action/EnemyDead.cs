using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class EnemyDead : Action
{
    [SerializeField] Animator anim;

    private bool _isFirst;

    public override void OnAwake()
    {
        anim = GetComponent<Animator>();
    }

    public override TaskStatus OnUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Zombie Death") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            return TaskStatus.Success;
        }

        if (_isFirst == false)
        {
            _isFirst = true;

            BattleSystem battle = GetComponent<BattleSystem>();
            battle.Die();

            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            Collider[] colliders = transform.GetComponentsInChildren<Collider>();
            foreach (Collider item in colliders)
            {
                item.enabled = false;
            }
        }

        return TaskStatus.Running;
    }
}