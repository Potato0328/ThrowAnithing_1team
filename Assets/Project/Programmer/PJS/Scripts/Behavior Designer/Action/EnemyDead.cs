using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDead : Action
{
    [SerializeField] Animator anim;
    [SerializeField] SharedFloat reward;
    [SerializeField] List<GameObject> dropItem;


    public override void OnAwake()
    {
        anim = GetComponent<Animator>();
    }

    public override TaskStatus OnUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Zombie Death") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            // ���Ͱ� �׾��� �� ������ ���
            int randNum = Random.Range(0, 101);
            Debug.Log(randNum);

            // TODO : Ȯ�� �ν����Ϳ��� ���ؼ� �� ��������
            if (randNum <= reward.Value)
            {
                Debug.Log("��ȭ ����");
            }

            return TaskStatus.Success;
        }

        anim.SetBool("Deadth", true);
        return TaskStatus.Running;
    }
}