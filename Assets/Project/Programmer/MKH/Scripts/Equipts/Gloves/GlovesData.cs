using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Gloves_", menuName = "Add Item/Equipment_Gloves")]
    public class GlovesData : Item_Equipment
    {
        public override Item Create()
        {
            Item_Equipment creatitem = Instantiate(this);
            Item item = Instantiate(this);
            item.Type = ItemType.Gloves;

            if (ItemType.Gloves == Type)
            {
                // �ֽ���
                creatitem.mEffect.AttackSpeed = UnityEngine.Random.Range(0.1f, 0.2f);
                // �ּ� ��ġ �ִ��ġ �ֱ�

            }
            return creatitem;
        }
    }
}
