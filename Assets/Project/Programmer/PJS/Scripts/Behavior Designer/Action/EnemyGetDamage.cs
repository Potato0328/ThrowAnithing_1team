using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyGetDamage : Action
{
	[SerializeField] EnemyDamageText damageText;	// ������ Text
	[SerializeField] Transform textPos;		// ���� ��ġ

	private BaseEnemy enemy;

	// TODO : �ǰ� ������ �� ������ UI�� �����ֱ�
	public override void OnStart()
	{
		enemy = GetComponent<BaseEnemy>();
	}

	public override TaskStatus OnUpdate()
	{
		EnemyDamageText eDmg = GameObject.Instantiate(damageText, textPos);
		eDmg.transform.SetParent(textPos);	// UI ������ transform.parent���� SetParent�� ����ؾ� ������ �����
		eDmg.Damage = enemy.resultDamage;

		return TaskStatus.Success;
	}
}