using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(menuName = "Add Item/Equipment_Helmet", fileName = "Helmet_")]
    public class HelmetData : ItemData
    {
        public string mainStatKey = "ü��";

        public List<string> subStatKeys = new List<string>
        {
            "����", "ġȮ", "����", "���׹̳�", "���ȹ��� ����", "���ݷ�", "�̼�", "����"
        };
    }
}
