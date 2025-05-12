using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(menuName = "Add Item/Equipment_Shirts", fileName = "Shirts_")]
    public class ShirtsData : ItemData
    {
        public string mainStatKey = "����";

        public List<string> subStatKeys = new List<string>
        {
            "ü��", "ġȮ", "����", "���׹̳�", "���ȹ��� ����", "���ݷ�", "�̼�", "����"
        };
    }
}
