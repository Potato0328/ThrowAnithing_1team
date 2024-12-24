using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Helmet_", menuName = "Add Item/Equipment_Helmet")]
    public class HelmetData : Item_Equipment
    {
        public override Item Create()
        {
            Item_Equipment creatitem = Instantiate(this);

            if (ItemType.Helmet == Type)
            {
                creatitem.mEffect.HP = UnityEngine.Random.Range(10, 20);
                // �ּ� ��ġ �ִ��ġ �ֱ�
            }
            return creatitem;
        }

    }
}
