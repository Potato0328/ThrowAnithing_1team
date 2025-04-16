using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(menuName = "Add Item/Equipment_Earring", fileName = "Earring_")]
    public class EarringData : ItemData
    {
        public string mainStatKey = "���ȹ��� ����";

        public List<string> subStatKeys = new List<string>
        {
            "ü��", "����", "ġȮ", "����", "���׹̳�", "���ݷ�", "�̼�", "����"
        };
    }
}