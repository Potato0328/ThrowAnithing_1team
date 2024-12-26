using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEditor.UIElements;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Helmet_", menuName = "Add Item/Equipment_Helmet")]
    public class HelmetData : Item_Equipment
    {
        public override Item Create()
        {
            List<Dictionary<string, object>> data = CSVReader.Read("Helmet");

            Item_Equipment createitem = Instantiate(this);

            if (ItemType.Helmet == Type)
            {
                if (RateType.Nomal == Rate)
                {
                    // �ֽ���
                    createitem.mEffect.HP = UnityEngine.Random.Range((int)data[0]["�ּ�"], (int)data[0]["ü��"]);

                    // ����
                    createitem.Name = (string)data[0]["�̸�"];
                    createitem.Description = $"HP : {createitem.mEffect.HP.ToString("F2")}";
                }
                else if (RateType.Magic == Rate)
                {
                    createitem.mEffect.HP = UnityEngine.Random.Range((int)data[1]["�ּ�"], (int)data[1]["ü��"]);

                    // ����
                    createitem.Name = (string)data[1]["�̸�"];
                    createitem.Description = $"HP : {createitem.mEffect.HP.ToString("F2")}";
                }
                else if (RateType.Rare == Rate)
                {
                    createitem.mEffect.HP = UnityEngine.Random.Range((int)data[2]["�ּ�"], (int)data[2]["ü��"]);

                    // ����
                    createitem.Name = (string)data[2]["�̸�"];
                    createitem.Description = $"HP : {createitem.mEffect.HP.ToString("F2")}";
                }
            }
            return createitem;
        }

    }
}
