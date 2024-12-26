using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Assets.Project.Programmer.NSJ.RND.Script;

public class LightningNova : Action
{
	[SerializeField] int atkDamage;	// ���ݷ�
	[SerializeField] float range;	// ���� ������ ����
	private BaseEnemy enemy;

	public override void OnStart()
	{
		enemy = GetComponent<BaseEnemy>();
	}

	public override TaskStatus OnUpdate()
	{
		int hitCount = Physics.OverlapSphereNonAlloc(transform.position, range, enemy.overLapCollider);//, 1<<Layer.Player);

		for (int i = 0; i < hitCount; i++)
		{
			IHit hit = enemy.overLapCollider[i].GetComponent<IHit>();
			if (hit != null)
				hit.TakeDamage(atkDamage);
		}

		return TaskStatus.Success;
	}
}