using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Assets.Project.Programmer.NSJ.RND.Script;

public class EnemyBoom : Action
{
	[SerializeField] SharedBool isBoom;
    [SerializeField] SharedFloat attackDist;    // ���� ����
    [SerializeField] ParticleSystem paticle;

    private BaseEnemy enemy;

	public override void OnStart()
	{
		enemy = GetComponent<BaseEnemy>();
	}

	public override TaskStatus OnUpdate()
	{
        // ���� ���� - true => �̹� ������, false => �������� ����
        if (isBoom.Value == true) 
            return TaskStatus.Failure;

        // ����
        enemy.TakeChargeBoom(attackDist.Value, enemy.Damage);

        if(enemy.CurHp > 0)
            enemy.CurHp = -1;

        paticle.Play();
        isBoom.SetValue(true);

        return TaskStatus.Success;
	}
}