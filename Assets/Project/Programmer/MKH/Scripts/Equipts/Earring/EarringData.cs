using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Earring_", menuName = "Add Item/Equipment_Earring")]
    public class EarringData : Item
    {
        public override Item Create()
        {
            Item item = Instantiate(this);

            // 장비 등급을 숫자로 변환
            // Normal : 0
            // Magic : 1
            // Rare : 2
            int rate = (int)Rate;
            var csv = CsvManager.Instance.earring;

            float.TryParse(csv.GetData($"{rate}", "최소"), out float min);
            float.TryParse(csv.GetData($"{rate}", "이속"), out float speed);
            int.TryParse(csv.GetData($"{rate}", "체력"), out int hp);
            int.TryParse(csv.GetData($"{rate}", "방어력"), out int defense);
            int.TryParse(csv.GetData($"{rate}", "치확"), out int critical);
            float.TryParse(csv.GetData($"{rate}", "공속"), out float attackSpeed);
            int.TryParse(csv.GetData($"{rate}", "스테미나"), out int stemina);
            float.TryParse(csv.GetData($"{rate}", "장비획득률 증가"), out float equipRate);
            float.TryParse(csv.GetData($"{rate}", "공격력"), out float damage);
            int.TryParse(csv.GetData($"{rate}", "마나"), out int mana);

            // 장비 등급 숫자로 주스텟과 설명 추가
            // 주스텟
            item.mEffect.EquipRate = Mathf.Round(Random.Range(min, equipRate) * 100f) / 100f;
            // 설명
            item.Name = csv.GetData($"{rate}", "이름");
            item.Description = $"장비 획득률 + {item.mEffect.EquipRate * 100f}%";

            // 부스텟을 모두 0으로 변경
            item.mEffect.HP = 0;
            item.mEffect.Defense = 0;
            item.mEffect.Critical = 0;
            item.mEffect.AttackSpeed = 0;
            item.mEffect.Stemina = 0;
            item.mEffect.Damage = 0;
            item.mEffect.Speed = 0;
            item.mEffect.Mana = 0;

            // 장비 등급 숫자만큼 랜덤한 부스텟 추가
            List<int> list = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
            for (int i = 0; i < rate; i++)  // Noraml : 0회 반복, Magic : 1회 반복, Rare : 2회 반복
            {
                int select = list[Random.Range(0, list.Count)];
                list.Remove(select);

                switch (select)
                {
                    case 0:
                        item.mEffect.HP = hp;
                        item.Description += $"\n체력 + {hp}";
                        break;
                    case 1:
                        item.mEffect.Defense = defense;
                        item.Description += $"\n방어력 + {defense}";
                        break;
                    case 2:
                        item.mEffect.Critical = critical;
                        item.Description += $"\n치명타 확률 + {critical}%";
                        break;
                    case 3:
                        item.mEffect.AttackSpeed = attackSpeed;
                        item.Description += $"\n공격속도 + {(attackSpeed * 100f)}%";
                        break;
                    case 4:
                        item.mEffect.Stemina = stemina;
                        item.Description += $"\n스테미나 + {stemina}";
                        break;
                    case 5:
                        if (rate == 1)
                        {
                            item.mEffect.Damage = damage;
                            item.Description += $"\n공격력 + {damage}";
                        }
                        else if (rate == 2)
                        {
                            item.mEffect.Damage = (int)damage;
                            item.Description += $"\n공격력 + {(int)damage}";
                        }
                        break;
                    case 6:
                        item.mEffect.Speed = speed;
                        item.Description += $"\n이동속도 + {speed * 100}";
                        break;
                    case 7:
                        item.mEffect.Mana = mana;
                        item.Description += $"\n마나 + {mana}";
                        break;
                }
            }
            return item;
        }
    }
}