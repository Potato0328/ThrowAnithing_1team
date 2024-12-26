using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using BehaviorDesigner.Runtime;

public class LightningNova : Action
{
	[SerializeField] int atkDamage;	// ���ݷ�
	[SerializeField] float range;   // ���� ������ ����
	[SerializeField] float coolTime;
	[SerializeField] SharedBool atkAble;
	
	private BaseEnemy enemy;

	public override void OnStart()
	{
		enemy = GetComponent<BaseEnemy>();
	}

	public override TaskStatus OnUpdate()
	{
		if(atkAble.Value == false)
			return TaskStatus.Failure;

		enemy.TakeChargeBoom(range, atkDamage);

		StartCoroutine(CoolTimeRoutine());

		return TaskStatus.Success;
	}

	IEnumerator CoolTimeRoutine()
	{
		atkAble.SetValue(false);
		Debug.Log("��Ÿ�� ����");
		yield return new WaitForSeconds(coolTime);
		atkAble.SetValue(true);
		Debug.Log("��Ÿ�� ��");
	}
}