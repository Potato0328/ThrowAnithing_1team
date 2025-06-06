using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(menuName = "Add Item/Equipment_Pants", fileName = "Pants_")]
    public class PantsData : ItemData
    {
        public string mainStatKey = "스테미나";

        public List<string> subStatKeys = new List<string>
        {
            "체력", "방어력", "치확", "공속", "장비획득률 증가", "공격력", "이속", "마나"
        };
    }
}
