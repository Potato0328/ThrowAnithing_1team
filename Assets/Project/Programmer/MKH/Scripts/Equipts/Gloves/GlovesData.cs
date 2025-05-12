using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(menuName = "Add Item/Equipment_Gloves", fileName = "Gloves_")]
    public class GlovesData : ItemData
    {
        public string mainStatKey = "공속";

        public List<string> subStatKeys = new List<string>
        {
            "체력", "방어력", "치확", "스테미나", "장비획득률 증가", "공격력", "이속", "마나"
        };
    }
}
