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
