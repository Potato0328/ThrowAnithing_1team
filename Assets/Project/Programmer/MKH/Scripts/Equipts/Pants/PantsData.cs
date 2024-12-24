using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Pants_", menuName = "Add Item/Equipment_Pants")]
    public class PantsData : Item_Equipment
    {
        public override Item Create()
        {
            Item_Equipment creatitem = Instantiate(this);
            Item item = Instantiate(this);
            item.Type = ItemType.Pants;

            if (ItemType.Pants == Type)
            {
                // �ֽ���
                creatitem.mEffect.Stemina = UnityEngine.Random.Range(10, 30);
                // �ּ� ��ġ �ִ��ġ �ֱ�
            }
            return creatitem;
        }
    }
}
