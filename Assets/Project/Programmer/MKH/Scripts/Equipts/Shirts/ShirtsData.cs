using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Shirts_", menuName = "Add Item/Equipment_Shirts")]
    public class ShirtsData : Item_Equipment
    {
        public override Item Create()
        {
            List<Dictionary<string, object>> data = CSVReader.Read("Shirts");

            Item_Equipment createitem = Instantiate(this);

            if (ItemType.Shirts == Type)
            {
                if (RateType.Nomal == Rate)
                {
                    // �ֽ���
                    createitem.mEffect.Defense = UnityEngine.Random.Range((int)data[0]["�ּ�"], (int)data[0]["����"]);

                    // ����
                    createitem.Name = (string)data[0]["�̸�"];
                    createitem.Description = $"Defence : {createitem.mEffect.Defense.ToString("F2")}";
                }
                else if (RateType.Magic == Rate)
                {
                    // �ֽ���
                    createitem.mEffect.Defense = UnityEngine.Random.Range((int)data[1]["�ּ�"], (int)data[1]["����"]);

                    // ����
                    createitem.Name = (string)data[1]["�̸�"];
                    createitem.Description = $"Defence : {createitem.mEffect.Defense.ToString("F2")}";
                }
                else if (RateType.Rare == Rate)
                {
                    // �ֽ���
                    createitem.mEffect.Defense = UnityEngine.Random.Range((int)data[2]["�ּ�"], (int)data[2]["����"]);

                    // ����
                    createitem.Name = (string)data[2]["�̸�"];
                    createitem.Description = $"Defence : {createitem.mEffect.Defense.ToString("F2")}";
                }
            }
            return createitem;
        }
    }
}
