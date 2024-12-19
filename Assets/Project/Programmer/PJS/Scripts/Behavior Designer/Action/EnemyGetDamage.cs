using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyGetDamage : Action
{
	// �浹�� ������Ʈ, �ش� ������Ʈ�� ���ݷ�
	// �� �ڽ��� hp
	public SharedGameObject triggerObj;

	private BaseEnemy enemy;
	private float damage;

	public override void OnStart()
	{
		enemy = GetComponent<BaseEnemy>();

		// TODO : ������Ʈ ��ũ��Ʈ Ȯ�� �� ����
		damage = triggerObj.Value.GetComponent<TestCodeData>().Atk;
	}

	public override TaskStatus OnUpdate()
	{
		enemy.GetDamage(damage);
		return TaskStatus.Success;
	}
}