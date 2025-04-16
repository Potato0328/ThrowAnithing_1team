using MKH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarringCreator : IItemCreator
{
    public Item Create(ItemData rawData)
    {
        EarringData data = rawData as EarringData;
        Item item = ScriptableObject.CreateInstance<Item>();

        var csv = CsvManager.Instance.GetCsv(data.itemType);
        int rate = (int)data.rateType;
        var effect = new EquipmentEffect();

        float.TryParse(csv.GetData($"{rate}", "최소"), out float min);
        float.TryParse(csv.GetData($"{rate}", data.mainStatKey), out float mainValue);

        effect.EquipRate = Mathf.Round(Random.Range(min, mainValue) * 100f) / 100f;
        string name = csv.GetData($"{rate}", "이름");
        string desc = $"장비 획득률 + {effect.EquipRate * 100f}%";

        var list = new List<string>(data.subStatKeys);
        for(int i = 0; i < rate; i++)
        {
            string key = list[Random.Range(0, list.Count)];
            list.Remove(key);

            float.TryParse(csv.GetData($"{rate}", key), out float val);
            switch(key)
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
                case "공격력": 
                    effect.Damage = val; 
                    desc += $"\n공격력 + {val}"; 
                    break;
                case "이속": 
                    effect.Speed = val; 
                    desc += $"\n이동속도 + {val * 100}"; 
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
