using System;
using UnityEngine;

namespace MKH
{
    public enum ItemType { Helmet, Shirts, Glasses, Gloves, Pants, Earring, Ring, Shoes, Necklace }

    public enum RateType { Nomal, Magic, Rare }

    public class Item : ScriptableObject
    {
        public string Name;
        public string Description;
        public ItemType Type;
        public RateType Rate;
        public Sprite Image;
        public GameObject Prefab;
        public EquipmentEffect Effect;

        public void SetBase(ItemData data, EquipmentEffect effect, string name,  string desc)
        {
            Name = name;
            Description = desc;
            Type = data.itemType;
            Rate = data.rateType;
            Image = data.icon;
            Prefab = data.prefab;
            Effect = effect;
        }
    }
}
