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
            Item_Equipment createitem = Instantiate(this);

            if (ItemType.Shirts == Type)
            {
                // 장비 등급을 숫자로 변환
                // Normal : 0
                // Magic : 1
                // Rare : 2
                int rate = (int)Rate;

                int.TryParse(CsvManager.Instance.shirts.GetData($"{rate}", "최소"), out int min);
                float.TryParse(CsvManager.Instance.shirts.GetData($"{rate}", "이속"), out float speed);
                int.TryParse(CsvManager.Instance.shirts.GetData($"{rate}", "체력"), out int hp);
                int.TryParse(CsvManager.Instance.shirts.GetData($"{rate}", "방어력"), out int defense);
                int.TryParse(CsvManager.Instance.shirts.GetData($"{rate}", "치확"), out int critical);
                float.TryParse(CsvManager.Instance.shirts.GetData($"{rate}", "공속"), out float attackSpeed);
                int.TryParse(CsvManager.Instance.shirts.GetData($"{rate}", "스테미나"), out int stemina);
                float.TryParse(CsvManager.Instance.shirts.GetData($"{rate}", "장비획득률 증가"), out float equipRate);
                float.TryParse(CsvManager.Instance.shirts.GetData($"{rate}", "공격력"), out float damage);
                int.TryParse(CsvManager.Instance.shirts.GetData($"{rate}", "마나"), out int mana);

                // 장비 등급 숫자로 주스텟과 설명 추가
                // 주스텟
                createitem.mEffect.Defense = Random.Range(min, defense);
                // 설명
                createitem.Name = CsvManager.Instance.shirts.GetData($"{rate}", "이름");
                createitem.Description = $"방어력 + {createitem.mEffect.Defense.ToString()}";

                // 부스텟을 모두 0으로 변경
                createitem.mEffect.HP = 0;
                createitem.mEffect.Critical = 0;
                createitem.mEffect.AttackSpeed = 0;
                createitem.mEffect.Stemina = 0;
                createitem.mEffect.EquipRate = 0;
                createitem.mEffect.Damage = 0;
                createitem.mEffect.Speed = 0;
                createitem.mEffect.Mana = 0;

                // 장비 등급 숫자만큼 랜덤한 부스텟 추가
                List<int> list = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
                for (int i = 0; i < rate; i++)  // Noraml : 0회 반복, Magic : 1회 반복, Rare : 2회 반복
                {
                    int select = list[Random.Range(0, list.Count)];
                    list.Remove(select);

                    switch (select)
                    {
                        case 0:
                            createitem.mEffect.HP = hp;
                            createitem.Description += $"\n체력 + {createitem.mEffect.HP.ToString()}";
                            break;
                        case 1:
                            createitem.mEffect.Critical = critical;
                            createitem.Description += $"\n치명타 확률 + {createitem.mEffect.Critical.ToString()}%";
                            break;
                        case 2:
                            createitem.mEffect.AttackSpeed = attackSpeed;
                            createitem.Description += $"\n공격속도 + {(createitem.mEffect.AttackSpeed * 100f).ToString()}%";
                            break;
                        case 3:
                            createitem.mEffect.Stemina = stemina;
                            createitem.Description += $"\n스테미나 + {createitem.mEffect.Stemina.ToString()}";
                            break;
                        case 4:
                            createitem.mEffect.EquipRate = equipRate;
                            createitem.Description += $"\n장비 획득률 + {(createitem.mEffect.EquipRate * 100f).ToString()}%";
                            break;
                        case 5:
                            if (rate == 1)
                            {
                                createitem.mEffect.Damage = damage;
                                createitem.Description += $"\n공격력 + {createitem.mEffect.Damage.ToString()}";
                            }
                            else if (rate == 2)
                            {
                                createitem.mEffect.Damage = (int)damage;
                                createitem.Description += $"\n공격력 + {createitem.mEffect.Damage.ToString()}";
                            }
                            break;
                        case 6:
                            createitem.mEffect.Speed = speed;
                            createitem.Description += $"\n이동속도 + {(createitem.mEffect.Speed * 100f).ToString()}";
                            break;
                        case 7:
                            createitem.mEffect.Mana = mana;
                            createitem.Description += $"\n마나 + {createitem.mEffect.Mana.ToString()}";
                            break;
                    }

                }
            }
            return createitem;
        }
    }
}
