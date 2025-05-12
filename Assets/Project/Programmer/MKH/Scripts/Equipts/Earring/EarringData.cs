using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(menuName = "Add Item/Equipment_Earring", fileName = "Earring_")]
    public class EarringData : ItemData
    {
        public string mainStatKey = "장비획득률 증가";

        public List<string> subStatKeys = new List<string>
        {
            "체력", "방어력", "치확", "공속", "스테미나", "공격력", "이속", "마나"
        };
    }
}