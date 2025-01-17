using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;

public class EnemyBoom : BaseAction
{
	[SerializeField] SharedBool isBoom;
    [SerializeField] SharedFloat attackDist;    // ���� ����
    [SerializeField] ParticleSystem paticle;
    public List<AudioClip> deathClips = new List<AudioClip>();

    public override void OnAwake()
    {
        baseEnemy = GetComponent<BaseEnemy>();

        foreach (AudioClip clip in baseEnemy.GetDaethClips())
        {
            deathClips.Add(clip);
        }
    }

	public override TaskStatus OnUpdate()
	{
        // ���� ���� - true => �̹� ������, false => �������� ����
        if (isBoom.Value == true) 
            return TaskStatus.Failure;

        // ����
        baseEnemy.TakeChargeBoom(attackDist.Value, baseEnemy.Damage);

        if(baseEnemy.CurHp > 0)
            baseEnemy.CurHp = -1;

        SoundManager.PlaySFX(baseEnemy.ChoiceAudioClip(deathClips));
        paticle.Play();
        isBoom.SetValue(true);

        return TaskStatus.Success;
	}
}