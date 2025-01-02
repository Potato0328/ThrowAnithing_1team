using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItem : Action
{
    [SerializeField] SharedFloat reward;
    [SerializeField] int maxPersent;
    [SerializeField] List<GameObject> dropItems;
    public override void OnStart()
    {
        if (reward.Value > maxPersent)
            reward.Value = reward.Value / maxPersent;
    }

    public override TaskStatus OnUpdate()
    {
        // ���Ͱ� �׾��� �� ������ ���
        int randNum = Random.Range(0, maxPersent + 1);
        Debug.Log(randNum);
        GameObject.Instantiate(dropItems[0], transform.position, transform.rotation);

        // TODO : Ȯ�� �ν����Ϳ��� ���ؼ� �� ��������
        /*if (randNum <= reward.Value)
        {
            Debug.Log("��ȭ ����");
            if (dropItems == null)
            {
                return TaskStatus.Success;
            }

            foreach (var item in dropItems)
            {
                GameObject.Instantiate(item, transform.position, transform.rotation);
            }
        }*/

        return TaskStatus.Success;
    }
}