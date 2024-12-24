using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Shirts_", menuName = "Add Item/Equipment_Shirts")]
    public class ShirtsData : Item_Equipment
    {
        public override Item Create()
        {
            Item_Equipment creatitem = Instantiate(this);

            if (ItemType.Shirts == Type)
            {
                // �ֽ���
                creatitem.mEffect.Defense = UnityEngine.Random.Range(1, 1);
                // �ּ� ��ġ �ִ��ġ �ֱ�
            }
            return creatitem;
        }
    }
}
