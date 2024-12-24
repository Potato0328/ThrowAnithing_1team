using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Necklace_", menuName = "Add Item/Equipment_Necklace")]
    public class NecklaceData : Item_Equipment
    {
        public override Item Create()
        {
            Item_Equipment creatitem = Instantiate(this);
            Item item = Instantiate(this);
            item.Type = ItemType.Necklace;

            if (ItemType.Necklace == Type)
            {
                // �ֽ���
                creatitem.mEffect.Mana = UnityEngine.Random.Range(10, 20);
                // �ּ� ��ġ �ִ��ġ �ֱ�
            }
            return creatitem;
        }
    }
}
