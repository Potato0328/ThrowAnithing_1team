using MKH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoesCreator : IItemCreator
{
    public Item Create(ItemData rawData)
    {
        ShoesData data = rawData as ShoesData;
        Item item = ScriptableObject.CreateInstance<Item>();

        var csv = CsvManager.Instance.GetCsv(data.itemType);
        int rate = (int)data.rateType;
        var effect = new EquipmentEffect();

        float.TryParse(csv.GetData($"{rate}", "최소"), out float min);
        float.TryParse(csv.GetData($"{rate}", data.mainStatKey), out float mainValue);

        // 장비 등급 숫자로 주스텟과 설명 추가
        // 주스텟
        effect.Speed = Mathf.Round(Random.Range(min, mainValue) * 100f) / 100f;
        // 설명
        string name = csv.GetData($"{rate}", "이름");
        string desc = $"이동속도 + {effect.Speed * 100f}";

        // 장비 등급 숫자만큼 랜덤한 부스텟 추가
        var list = new List<string>(data.subStatKeys);
        for (int i = 0; i < rate; i++)  // Noraml : 0회 반복, Magic : 1회 반복, Rare : 2회 반복
        {
            string key = list[Random.Range(0, list.Count)];
            list.Remove(key);

            float.TryParse(csv.GetData($"{rate}", key), out float val);
            switch (key)
            {
                case "체력":
                    effect.HP = val;
                    desc += $"\n체력 + {val}";
                    break;
                case "방어력":
                    effect.Defense = val;
                    desc += $"\n방어력 + {val}";
                    break;
                case "치확":
                    effect.Critical = val;
                    desc += $"\n치명타 확률 + {val}%";
                    break;
                case "공속":
                    effect.AttackSpeed = val;
                    desc += $"\n공격속도 + {val * 100}%";
                    break;
                case "스테미나":
                    effect.Stemina = val;
                    desc += $"\n스테미나 + {val}";
                    break;
                case "장비획득률 증가":
                    effect.Critical = val;
                    desc += $"\n장비 획득률 + {val * 100}%";
                    break;
                case "공격력":
                    effect.Damage = val;
                    desc += $"\n공격력 + {val}";
                    break;
                case "마나":
                    effect.Mana = val;
                    desc += $"\n마나 + {val}";
                    break;

            }
        }

        item.SetBase(data, effect, name, desc);
        return item;
    }
}
