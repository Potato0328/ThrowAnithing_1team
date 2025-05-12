using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(menuName = "Add Item/Equipment_Shirts", fileName = "Shirts_")]
    public class ShirtsData : ItemData
    {
        public string mainStatKey = "방어력";

        public List<string> subStatKeys = new List<string>
        {
            "체력", "치확", "공속", "스테미나", "장비획득률 증가", "공격력", "이속", "마나"
        };
    }
}
