using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Ring_", menuName = "Add Item/Equipment_Ring")]
    public class RingData : Item
    {
        public override Item Create()
        {
            Item item = Instantiate(this);

            // ��� ����� ���ڷ� ��ȯ
            // Normal : 0
            // Magic : 1
            // Rare : 2
            int rate = (int)Rate;
            var csv = CsvManager.Instance.ring;

            int.TryParse(csv.GetData($"{rate}", "�ּ�"), out int min);
            float.TryParse(csv.GetData($"{rate}", "�̼�"), out float speed);
            int.TryParse(csv.GetData($"{rate}", "ü��"), out int hp);
            int.TryParse(csv.GetData($"{rate}", "����"), out int defense);
            int.TryParse(csv.GetData($"{rate}", "ġȮ"), out int critical);
            float.TryParse(csv.GetData($"{rate}", "����"), out float attackSpeed);
            int.TryParse(csv.GetData($"{rate}", "���׹̳�"), out int stemina);
            float.TryParse(csv.GetData($"{rate}", "���ȹ��� ����"), out float equipRate);
            int.TryParse(csv.GetData($"{rate}", "���ݷ�"), out int damage);
            int.TryParse(csv.GetData($"{rate}", "����"), out int mana);

            // ��� ��� ���ڷ� �ֽ��ݰ� ���� �߰�
            // �ֽ���
            item.mEffect.Damage = Random.Range(min, damage);
            // ����
            item.Name = csv.GetData($"{rate}", "�̸�");
            item.Description = $"���ݷ� + {item.mEffect.Damage}";

            // �ν����� ��� 0���� ����
            item.mEffect.HP = 0;
            item.mEffect.Defense = 0;
            item.mEffect.Critical = 0;
            item.mEffect.AttackSpeed = 0;
            item.mEffect.Stemina = 0;
            item.mEffect.EquipRate = 0;
            item.mEffect.Speed = 0;
            item.mEffect.Mana = 0;

            // ��� ��� ���ڸ�ŭ ������ �ν��� �߰�
            List<int> list = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
            for (int i = 0; i < rate; i++)  // Noraml : 0ȸ �ݺ�, Magic : 1ȸ �ݺ�, Rare : 2ȸ �ݺ�
            {
                int select = list[Random.Range(0, list.Count)];
                list.Remove(select);

                switch (select)
                {
                    case 0:
                        item.mEffect.HP = hp;
                        item.Description += $"\nü�� + {hp}";
                        break;
                    case 1:
                        item.mEffect.Defense = defense;
                        item.Description += $"\n���� + {defense}";
                        break;
                    case 2:
                        item.mEffect.Critical = critical;
                        item.Description += $"\nġ��Ÿ Ȯ�� + {critical}%";
                        break;
                    case 3:
                        item.mEffect.AttackSpeed = attackSpeed;
                        item.Description += $"\n���ݼӵ� + {attackSpeed * 100f}%";
                        break;
                    case 4:
                        item.mEffect.Stemina = stemina;
                        item.Description += $"\n���׹̳� + {stemina}";
                        break;
                    case 5:
                        item.mEffect.EquipRate = equipRate;
                        item.Description += $"\n��� ȹ��� + {equipRate * 100f}%";
                        break;
                    case 6:
                        item.mEffect.Speed = speed;
                        item.Description += $"\n�̵��ӵ� + {speed * 100f}";
                        break;
                    case 7:
                        item.mEffect.Mana = mana;
                        item.Description += $"\n���� + {mana}";
                        break;
                }
            }
            return item;
        }
    }
}
