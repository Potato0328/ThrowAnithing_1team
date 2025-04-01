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
            Item_Equipment createitem = Instantiate(this);

            if (ItemType.Necklace == Type)
            {
                // ��� ����� ���ڷ� ��ȯ
                // Normal : 0
                // Magic : 1
                // Rare : 2
                int rate = (int)Rate;

                int.TryParse(CsvManager.Instance.necklace.GetData($"{rate}", "�ּ�"), out int min);
                float.TryParse(CsvManager.Instance.necklace.GetData($"{rate}", "�̼�"), out float speed);
                int.TryParse(CsvManager.Instance.necklace.GetData($"{rate}", "ü��"), out int hp);
                int.TryParse(CsvManager.Instance.necklace.GetData($"{rate}", "����"), out int defense);
                int.TryParse(CsvManager.Instance.necklace.GetData($"{rate}", "ġȮ"), out int critical);
                float.TryParse(CsvManager.Instance.necklace.GetData($"{rate}", "����"), out float attackSpeed);
                int.TryParse(CsvManager.Instance.necklace.GetData($"{rate}", "���׹̳�"), out int stemina);
                float.TryParse(CsvManager.Instance.necklace.GetData($"{rate}", "���ȹ��� ����"), out float equipRate);
                float.TryParse(CsvManager.Instance.necklace.GetData($"{rate}", "���ݷ�"), out float damage);
                int.TryParse(CsvManager.Instance.necklace.GetData($"{rate}", "����"), out int mana);

                // ��� ��� ���ڷ� �ֽ��ݰ� ���� �߰�
                // �ֽ���
                createitem.mEffect.Mana = Random.Range(min, mana);
                // ����
                createitem.Name = CsvManager.Instance.necklace.GetData($"{rate}", "�̸�");
                createitem.Description = $"���� + {createitem.mEffect.Mana.ToString()}";

                // �ν����� ��� 0���� ����
                createitem.mEffect.HP = 0;
                createitem.mEffect.Defense = 0;
                createitem.mEffect.Critical = 0;
                createitem.mEffect.AttackSpeed = 0;
                createitem.mEffect.Stemina = 0;
                createitem.mEffect.EquipRate = 0;
                createitem.mEffect.Damage = 0;
                createitem.mEffect.Speed = 0;
                

                // ��� ��� ���ڸ�ŭ ������ �ν��� �߰�
                List<int> list = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
                for (int i = 0; i < rate; i++)  // Noraml : 0ȸ �ݺ�, Magic : 1ȸ �ݺ�, Rare : 2ȸ �ݺ�
                {
                    int select = list[Random.Range(0, list.Count)];
                    list.Remove(select);

                    switch (select)
                    {
                        case 0:
                            createitem.mEffect.HP = hp;
                            createitem.Description += $"\nü�� + {createitem.mEffect.HP.ToString()}";
                            break;
                        case 1:
                            createitem.mEffect.Defense = defense;
                            createitem.Description += $"\n���� + {createitem.mEffect.Defense.ToString()}";
                            break;
                        case 2:
                            createitem.mEffect.Critical = critical;
                            createitem.Description += $"\nġ��Ÿ Ȯ�� + {createitem.mEffect.Critical.ToString()}%";
                            break;
                        case 3:
                            createitem.mEffect.AttackSpeed = attackSpeed;
                            createitem.Description += $"\n���ݼӵ� + {(createitem.mEffect.AttackSpeed * 100f).ToString()}%";
                            break;
                        case 4:
                            createitem.mEffect.Stemina = stemina;
                            createitem.Description += $"\n���׹̳� + {createitem.mEffect.Stemina.ToString()}";
                            break;
                        case 5:
                            createitem.mEffect.EquipRate = equipRate;
                            createitem.Description += $"\n���ȹ��� + {(createitem.mEffect.EquipRate * 100f).ToString()}%";
                            break;
                        case 6:
                            if (rate == 1)
                            {
                                createitem.mEffect.Damage = damage;
                                createitem.Description += $"\n���ݷ� + {createitem.mEffect.Damage.ToString()}";
                            }
                            else if (rate == 2)
                            {
                                createitem.mEffect.Damage = (int)damage;
                                createitem.Description += $"\n���ݷ� + {createitem.mEffect.Damage.ToString()}";
                            }
                            break;
                        case 7:
                            createitem.mEffect.Speed = speed;
                            createitem.Description += $"\n�̵��ӵ� + {(createitem.mEffect.Speed * 100f).ToString()}";
                            break;
                    }

                }
            }
            return createitem;
        }
    }
}
