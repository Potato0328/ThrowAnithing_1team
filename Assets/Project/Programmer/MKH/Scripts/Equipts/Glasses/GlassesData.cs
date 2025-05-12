using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(menuName = "Add Item/Equipment_Glasses", fileName = "Glasses_")]
    public class GlassesData : ItemData
    {
        public string mainStatKey = "치확";

        public List<string> subStatKeys = new List<string>
        {
            "체력", "방어력", "공속", "스테미나", "장비획득률 증가", "공격력", "이속", "마나"
        };
    }
}
