using MKH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingCreator : IItemCreator
{
    public Item Create(ItemData rawData)
    {
        RingData data = rawData as RingData;
        Item item = ScriptableObject.CreateInstance<Item>();

        var csv = CsvManager.Instance.GetCsv(data.itemType);
        int rate = (int)data.rateType;
        var effect = new EquipmentEffect();

        int.TryParse(csv.GetData($"{rate}", "�ּ�"), out int min);
        int.TryParse(csv.GetData($"{rate}", data.mainStatKey), out int mainValue);

        // ��� ��� ���ڷ� �ֽ��ݰ� ���� �߰�
        // �ֽ���
        effect.Damage = Random.Range(min, mainValue);
        // ����
        string name = csv.GetData($"{rate}", "�̸�");
        string desc = $"���ݷ� + {effect.Damage}";

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
                case "ġȮ":
                    effect.Critical = val;
                    desc += $"\nġ��Ÿ Ȯ�� + {val}%";
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
                    effect.Critical = val;
                    desc += $"\n��� ȹ��� + {val * 100}%";
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
