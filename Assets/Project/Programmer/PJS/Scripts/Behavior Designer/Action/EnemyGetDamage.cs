using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyGetDamage : Action
{
	[SerializeField] SharedBool takeDamage;

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
		//Debug.Log("����");
		//EnemyDamageText eDmg = GameObject.Instantiate(damageText, textPos.position, textPos.rotation);
		//Debug.Log(eDmg);
		//eDmg.transform.SetParent(textPos);	// UI ������ transform.parent���� SetParent�� ����ؾ� ������ �����
		//eDmg.Damage = enemy.resultDamage;

		return TaskStatus.Success;
	}
}