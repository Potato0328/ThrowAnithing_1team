using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyGetDamage : Action
{
	// �浹�� ������Ʈ, �ش� ������Ʈ�� ���ݷ�
	// �� �ڽ��� hp
	[SerializeField] EnemyDamage damageText;
	[SerializeField] Transform textPos;
	private BaseEnemy enemy;

	// TODO : �ǰ� ������ �� ������ UI�� �����ֱ�
	public override void OnStart()
	{
		enemy = GetComponent<BaseEnemy>();
	}

	public override TaskStatus OnUpdate()
	{
		EnemyDamage eDmg = GameObject.Instantiate(damageText, textPos);
		eDmg.transform.SetParent(textPos);
		eDmg.Damage = enemy.resultDamage;

		return TaskStatus.Success;
	}
}