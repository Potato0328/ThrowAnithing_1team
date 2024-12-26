using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Earring_", menuName = "Add Item/Equipment_Earring")]
    public class EarringData : Item_Equipment
    {
        public override Item Create()
        {
            List<Dictionary<string, object>> data = CSVReader.Read("Earring");

            Item_Equipment createitem = Instantiate(this);

            if (ItemType.Earring == Type)
            {
                if (RateType.Nomal == Rate)
                {
                    // �ֽ���
                    createitem.mEffect.EquipRate = UnityEngine.Random.Range((float)data[0]["�ּ�"], (float)data[0]["���ȹ��� ����"]);

                    // ����
                    createitem.Name = (string)data[0]["�̸�"];
                    createitem.Description = $"EquipRate : {createitem.mEffect.EquipRate.ToString("F2")}";
                }
                else if (RateType.Magic == Rate)
                {
                    // �ֽ���
                    createitem.mEffect.EquipRate = UnityEngine.Random.Range((float)data[1]["�ּ�"], (float)data[1]["���ȹ��� ����"]);

                    // ����
                    createitem.Name = (string)data[1]["�̸�"];
                    createitem.Description = $"EquipRate : {createitem.mEffect.EquipRate.ToString("F2")}";
                }
                else if (RateType.Rare == Rate)
                {
                    // �ֽ���
                    createitem.mEffect.EquipRate = UnityEngine.Random.Range((float)data[2]["�ּ�"], (float)data[2]["���ȹ��� ����"]);

                    // ����
                    createitem.Name = (string)data[2]["�̸�"];
                    createitem.Description = $"EqipRate : {createitem.mEffect.EquipRate.ToString("F2")}";
                }
            }

            return createitem;

        }
    }
}
