using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(menuName = "Add Item/Equipment_Gloves", fileName = "Gloves_")]
    public class GlovesData : ItemData
    {
        public string mainStatKey = "����";

        public List<string> subStatKeys = new List<string>
        {
            "ü��", "����", "ġȮ", "���׹̳�", "���ȹ��� ����", "���ݷ�", "�̼�", "����"
        };
    }
}
