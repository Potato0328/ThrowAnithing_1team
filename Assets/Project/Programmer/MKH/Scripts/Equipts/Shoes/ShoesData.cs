using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Shoes_", menuName = "Add Item/Equipment_Shoes")]
    public class ShoesData : Item_Equipment
    {
        public override Item Create()
        {
            Item_Equipment creatitem = Instantiate(this);

            if (ItemType.Shoes == Type)
            {
                // �ֽ���
                creatitem.mEffect.Speed = UnityEngine.Random.Range(0.05f, 0.1f);
                // �ּ� ��ġ �ִ��ġ �ֱ�
            }
            return creatitem;
        }
    }
}
