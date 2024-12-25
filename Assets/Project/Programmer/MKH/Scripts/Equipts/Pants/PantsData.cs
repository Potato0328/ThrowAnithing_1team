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
            List<Dictionary<string, object>> data = CSVReader.Read("Pants");

            Item_Equipment createitem = Instantiate(this);

            if (ItemType.Pants == Type)
            {
                if (RateType.Nomal == Rate)
                {
                    // �ֽ���
                    createitem.mEffect.Stemina = UnityEngine.Random.Range((int)data[0]["�ּ�"], (int)data[0]["���׹̳�"]);

                    // ����
                    createitem.Name = (string)data[0]["�̸�"];
                    createitem.Description = $"Stemina : {createitem.mEffect.Stemina.ToString("F2")}";
                }
                else if (RateType.Magic == Rate)
                {
                    // �ֽ���
                    createitem.mEffect.Stemina = UnityEngine.Random.Range((int)data[1]["�ּ�"], (int)data[1]["���׹̳�"]);

                    // ����
                    createitem.Name = (string)data[1]["�̸�"];
                    createitem.Description = $"Stemina : {createitem.mEffect.Stemina.ToString("F2")}";
                }
                else if (RateType.Rare == Rate)
                {
                    // �ֽ���
                    createitem.mEffect.Stemina = UnityEngine.Random.Range((int)data[2]["�ּ�"], (int)data[2]["���׹̳�"]);

                    // ����
                    createitem.Name = (string)data[2]["�̸�"];
                    createitem.Description = $"Stemina : {createitem.mEffect.Stemina.ToString("F2")}";
                }
            }
            return createitem;
        }
    }
}
