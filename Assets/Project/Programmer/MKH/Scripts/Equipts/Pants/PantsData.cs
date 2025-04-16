using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(menuName = "Add Item/Equipment_Pants", fileName = "Pants_")]
    public class PantsData : ItemData
    {
        public string mainStatKey = "���׹̳�";

        public List<string> subStatKeys = new List<string>
        {
            "ü��", "����", "ġȮ", "����", "���ȹ��� ����", "���ݷ�", "�̼�", "����"
        };
    }
}
