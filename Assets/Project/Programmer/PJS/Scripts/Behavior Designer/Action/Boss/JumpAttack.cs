using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class JumpAttack : Action
{
	public SharedGameObject player;
	public float dis;
	public string testText;
	public float speed;

	private Rigidbody rigid;

    public override void OnAwake()
    {
        rigid = GetComponent<Rigidbody>();
    }


	public override TaskStatus OnUpdate()
	{
        dis = (transform.position - player.Value.transform.position).magnitude;

		if (dis >= 8 && dis <= 10)
		{
			testText = "����";
			transform.position = Vector3.Slerp(transform.position, player.Value.transform.position, speed * Time.deltaTime);
			
			return TaskStatus.Running;
		}
		else
			testText = "������";

		return TaskStatus.Success;
	}
}