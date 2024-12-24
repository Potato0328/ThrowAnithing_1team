using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Ring_", menuName = "Add Item/Equipment_Ring")]
    public class RingData : Item_Equipment
    {
        public override Item Create()
        {
            Item_Equipment creatitem = Instantiate(this);

            if (ItemType.Ring == Type)
            {
                // �ֽ���
                creatitem.mEffect.Damage = UnityEngine.Random.Range(5, 10);
                // �ּ� ��ġ �ִ��ġ �ֱ�
            }
            return creatitem;
        }
    }
}
