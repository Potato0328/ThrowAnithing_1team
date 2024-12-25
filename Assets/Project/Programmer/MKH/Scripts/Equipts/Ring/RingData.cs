using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Ring_", menuName = "Add Item/Equipment_Ring")]
    public class RingData : Item_Equipment
    {
        public override Item Create()
        {
            List<Dictionary<string, object>> data = CSVReader.Read("Ring");

            Item_Equipment createitem = Instantiate(this);

            if (ItemType.Ring == Type)
            {
                if (RateType.Nomal == Rate)
                {
                    // �ֽ���
                    createitem.mEffect.Damage = UnityEngine.Random.Range((int)data[0]["�ּ�"], (int)data[0]["���ݷ�"]);

                    // ����
                    createitem.Name = (string)data[0]["�̸�"];
                    createitem.Description = $"Damage : {createitem.mEffect.Damage.ToString("F2")}";
                }
                else if (RateType.Magic == Rate)
                {
                    // �ֽ���
                    createitem.mEffect.Damage = UnityEngine.Random.Range((int)data[1]["�ּ�"], (int)data[1]["���ݷ�"]);

                    // ����
                    createitem.Name = (string)data[1]["�̸�"];
                    createitem.Description = $"Damage : {createitem.mEffect.Damage.ToString("F2")}";
                }
                else if (RateType.Rare == Rate)
                {
                    // �ֽ���
                    createitem.mEffect.Damage = UnityEngine.Random.Range((int)data[2]["�ּ�"], (int)data[2]["���ݷ�"]);

                    // ����
                    createitem.Name = (string)data[2]["�̸�"];
                    createitem.Description = $"Damage : {createitem.mEffect.Damage.ToString("F2")}";
                }
            }
            return createitem;
        }
    }
}
