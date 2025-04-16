using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(menuName = "Add Item/Equipment_Ring", fileName = "Ring_")]
    public class RingData : ItemData
    {
        public string mainStatKey = "���ݷ�";

        public List<string> subStatKeys = new List<string>
        {
            "ü��", "����", "ġȮ", "����", "���׹̳�", "���ȹ��� ����", "�̼�", "����"
        };
    }
}
