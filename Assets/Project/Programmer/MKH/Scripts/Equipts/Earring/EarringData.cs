using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Earring_", menuName = "Add Item/Equipment_Earring")]
    public class EarringData : Item_Equipment
    {
        public override Item Create()
        {
            Item_Equipment creatitem = Instantiate(this);

            if (ItemType.Earring == Type)
            {
                // �ֽ���
                creatitem.mEffect.EquipRate = UnityEngine.Random.Range(0.01f, 0.1f);
                // �ּ� ��ġ �ִ��ġ �ֱ�
                creatitem.Description = "EquipRate : " + creatitem.mEffect.EquipRate.ToString("F2");
            }
            return creatitem;
        }
    }
}
