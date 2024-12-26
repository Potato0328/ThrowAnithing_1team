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
                // ��� ����� ���ڷ� ��ȯ
                // Normal : 0
                // Magic : 1
                // Rare : 2
                int rate = (int)Rate;

                // ��� ��� ���ڷ� �ֽ��ݰ� ���� �߰�
                // �ֽ���
                createitem.mEffect.Defense = UnityEngine.Random.Range((int)data[rate]["�ּ�"], (int)data[rate]["����"]);
                // ����
                createitem.Name = (string)data[rate]["�̸�"];
                createitem.Description = $"Defense : {createitem.mEffect.Defense.ToString()}";

                // �ν����� ��� 0���� ����
                createitem.mEffect.HP = 0;
                createitem.mEffect.Critical = 0;
                createitem.mEffect.AttackSpeed = 0;
                createitem.mEffect.Stemina = 0;
                createitem.mEffect.EquipRate = 0;
                createitem.mEffect.Damage = 0;
                createitem.mEffect.Speed = 0;
                createitem.mEffect.Mana = 0;

                // ��� ��� ���ڸ�ŭ ������ �ν��� �߰�
                List<int> list = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
                for (int i = 0; i < rate; i++)  // Noraml : 0ȸ �ݺ�, Magic : 1ȸ �ݺ�, Rare : 2ȸ �ݺ�
                {
                    int select = list[Random.Range(0, list.Count)];
                    list.Remove(select);

                    switch (select)
                    {
                        case 0:
                            createitem.mEffect.HP = (int)data[rate]["ü��"];
                            createitem.Description += $"\nHP : {createitem.mEffect.HP.ToString()}";
                            break;
                        case 1:
                            createitem.mEffect.Critical = (int)data[rate]["ġȮ"];
                            createitem.Description += $"\nCritical : {createitem.mEffect.Critical.ToString()}";
                            break;
                        case 2:
                            createitem.mEffect.AttackSpeed = (float)data[rate]["����"];
                            createitem.Description += $"\nAttackSpeed : {createitem.mEffect.AttackSpeed.ToString("F2")}";
                            break;
                        case 3:
                            createitem.mEffect.Stemina = (int)data[rate]["���׹̳�"];
                            createitem.Description += $"\nStemina : {createitem.mEffect.Stemina.ToString()}";
                            break;
                        case 4:
                            createitem.mEffect.EquipRate = (float)data[rate]["���ȹ��� ����"];
                            createitem.Description += $"\nEquipRate : {createitem.mEffect.EquipRate.ToString("F2")}";
                            break;
                        case 5:
                            createitem.mEffect.Damage = (int)data[rate]["���ݷ�"];
                            createitem.Description += $"\nDamage : {createitem.mEffect.Damage.ToString()}";
                            break;
                        case 6:
                            createitem.mEffect.Speed = (float)data[rate]["�̼�"];
                            createitem.Description += $"\nSpeed : {createitem.mEffect.Speed.ToString("F2")}";
                            break;
                        case 7:
                            createitem.mEffect.Mana = (int)data[rate]["����"];
                            createitem.Description += $"\nMana : {createitem.mEffect.Mana.ToString()}";
                            break;
                    }

                }
            }
            return createitem;
        }
    }
}
