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
            List<Dictionary<string, object>> data = CSVReader.Read("Necklace");

            Item_Equipment createitem = Instantiate(this);

            if (ItemType.Necklace == Type)
            {
                if (RateType.Nomal == Rate)
                {
                    // �ֽ���
                    createitem.mEffect.Mana = UnityEngine.Random.Range((int)data[0]["�ּ�"], (int)data[0]["����"]);

                    // ����
                    createitem.Name = (string)data[0]["�̸�"];
                    createitem.Description = $"Mana : {createitem.mEffect.Mana.ToString("F2")}";
                }
                else if (RateType.Magic == Rate)
                {
                    // �ֽ���
                    createitem.mEffect.Mana = UnityEngine.Random.Range((int)data[1]["�ּ�"], (int)data[1]["����"]);

                    // ����
                    createitem.Name = (string)data[1]["�̸�"];
                    createitem.Description = $"Mana : {createitem.mEffect.Mana.ToString("F2")}";
                }
                else if (RateType.Rare == Rate)
                {
                    // �ֽ���
                    createitem.mEffect.Mana = UnityEngine.Random.Range((int)data[2]["�ּ�"], (int)data[2]["����"]);

                    // ����
                    createitem.Name = (string)data[2]["�̸�"];
                    createitem.Description = $"Mana : {createitem.mEffect.Mana.ToString("F2")}";
                }
            }
            return createitem;
        }
    }
}
