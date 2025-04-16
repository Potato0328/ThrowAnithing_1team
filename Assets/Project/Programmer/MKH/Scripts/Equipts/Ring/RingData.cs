using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(menuName = "Add Item/Equipment_Ring", fileName = "Ring_")]
    public class RingData : ItemData
    {
        public string mainStatKey = "공격력";

        public List<string> subStatKeys = new List<string>
        {
            "체력", "방어력", "치확", "공속", "스테미나", "장비획득률 증가", "이속", "마나"
        };
    }
}
