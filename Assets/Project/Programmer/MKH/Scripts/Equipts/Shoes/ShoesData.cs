using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Shoes_", menuName = "Add Item/Equipment_Shoes")]
    public class ShoesData : Item_Equipment
    {
        public override Item Create()
        {
            List<Dictionary<string, object>> data = CSVReader.Read("Shoes");

            Item_Equipment createitem = Instantiate(this);

            if (ItemType.Shoes == Type)
            {
                if (RateType.Nomal == Rate)
                {
                    // �ֽ���
                    createitem.mEffect.Speed = UnityEngine.Random.Range((float)data[0]["�ּ�"], (float)data[0]["�̼�"]);

                    // ����
                    createitem.Name = (string)data[0]["�̸�"];
                    createitem.Description = $"Speed : {createitem.mEffect.Speed.ToString("F2")}";
                }
                else if (RateType.Magic == Rate)
                {
                    // �ֽ���
                    createitem.mEffect.Speed = UnityEngine.Random.Range((float)data[1]["�ּ�"], (float)data[1]["�̼�"]);

                    // ����
                    createitem.Name = (string)data[1]["�̸�"];
                    createitem.Description = $"Speed : {createitem.mEffect.Speed.ToString("F2")}";
                }
                else if (RateType.Rare == Rate)
                {
                    // �ֽ���
                    createitem.mEffect.Speed = UnityEngine.Random.Range((float)data[2]["�ּ�"], (float)data[2]["�̼�"]);

                    // ����
                    createitem.Name = (string)data[2]["�̸�"];
                    createitem.Description = $"Speed : {createitem.mEffect.Speed.ToString("F2")}";
                }
            }
            return createitem;
        }
    }
}
