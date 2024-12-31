using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class TestStopSpeed : Conditional
{
	[SerializeField] SharedString animationName;

	private Animator anim;

    public override void OnStart()
    {
        anim = GetComponent<Animator>();
    }

    public override TaskStatus OnUpdate()
	{
        Debug.Log(anim.GetCurrentAnimatorStateInfo(0).IsName(animationName.Value));
        if(anim.GetCurrentAnimatorStateInfo(0).IsName(animationName.Value))
        {
            Debug.Log("���� O");
            return TaskStatus.Success;
        }
        else
        {
            Debug.Log("���� X");
            return TaskStatus.Failure;
        }

	}
}