using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(menuName = "Add Item/Equipment_Necklace", fileName = "Necklace_")]
    public class NecklaceData : ItemData
    {
        public string mainStatKey = "마나";

        public List<string> subStatKeys = new List<string>
        {
            "체력", "방어력", "치확", "공속", "스테미나", "장비획득률 증가", "공격력", "이속"
        };
    }
}
