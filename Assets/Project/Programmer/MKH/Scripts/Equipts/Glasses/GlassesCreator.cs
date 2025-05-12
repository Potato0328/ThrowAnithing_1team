using MKH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesCreator : IItemCreator
{
    public Item Create(ItemData rawData)
    {
        GlassesData data = rawData as GlassesData;
        Item item = ScriptableObject.CreateInstance<Item>();

        var csv = CsvManager.Instance.GetCsv(data.itemType);
        int rate = (int)data.rateType;
        var effect = new EquipmentEffect();

        int.TryParse(csv.GetData($"{rate}", "�ּ�"), out int min);
        int.TryParse(csv.GetData($"{rate}", data.mainStatKey), out int mainValue);

        effect.Critical = Random.Range(min, mainValue);
        string name = csv.GetData($"{rate}", "�̸�");
        string desc = $"ġ��Ÿ Ȯ�� + {effect.Critical}%";

        // ��� ��� ���ڸ�ŭ ������ �ν��� �߰�
        var list = new List<string>(data.subStatKeys);
        for (int i = 0; i < rate; i++)  // Noraml : 0ȸ �ݺ�, Magic : 1ȸ �ݺ�, Rare : 2ȸ �ݺ�
        {
            string key = list[Random.Range(0, list.Count)];
            list.Remove(key);

            float.TryParse(csv.GetData($"{rate}", key), out float val);
            switch (key)
            {
                case "ü��":
                    effect.HP = val;
                    desc += $"\nü�� + {val}";
                    break;
                case "����":
                    effect.Defense = val;
                    desc += $"\n���� + {val}";
                    break;
                case "����":
                    effect.AttackSpeed = val;
                    desc += $"\n���ݼӵ� + {val * 100}%";
                    break;
                case "���׹̳�":
                    effect.Stemina = val;
                    desc += $"\n���׹̳� + {val}";
                    break;
                case "���ȹ��� ����":
                    effect.EquipRate = val;
                    desc += $"\n��� ȹ��� + {val * 100}%";
                    break;
                case "���ݷ�":
                    effect.Damage = val;
                    desc += $"\n���ݷ� + {val}";
                    break;
                case "�̼�":
                    effect.Speed = val;
                    desc += $"\n�̵��ӵ� + {val * 100}";
                    break;
                case "����":
                    effect.Mana = val;
                    desc += $"\n���� + {val}";
                    break;
            }
        }

        item.SetBase(data, effect, name, desc);
        return item;
    }
}
