using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Assets.Project.Programmer.NSJ.RND.Script;

public class EnemyGetDamage : Action
{
	// �浹�� ������Ʈ, �ش� ������Ʈ�� ���ݷ�
	// �� �ڽ��� hp
	public SharedGameObject triggerObj;

	private BaseEnemy enemy;
	private float playerDamage;

	public override void OnStart()
	{
		enemy = GetComponent<BaseEnemy>();

		// TODO : �÷��̾� ��ô ������Ʈ ��ũ��Ʈ Ȯ�� �� ����
		//damage = triggerObj.Value.GetComponent<TestCodeData>().Atk;
	}

	public override TaskStatus OnUpdate()
	{
		return enemy.GetDamage(playerDamage) ? TaskStatus.Success : TaskStatus.Failure;
	}
}