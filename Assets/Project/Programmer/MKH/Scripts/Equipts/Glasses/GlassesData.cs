using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Glasses_", menuName = "Add Item/Equipment_Glasses")]
    public class GlassesData : Item_Equipment
    {
        public override Item Create()
        {
            Item_Equipment creatitem = Instantiate(this);

            if (ItemType.Glasses == Type)
            {
                // �ֽ���
                creatitem.mEffect.Critical = UnityEngine.Random.Range(3, 8);
                // �ּ� ��ġ �ִ��ġ �ֱ�
            }
            return creatitem;
        }

    }
}
