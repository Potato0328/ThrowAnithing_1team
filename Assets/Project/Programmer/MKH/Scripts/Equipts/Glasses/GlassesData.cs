using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(menuName = "Add Item/Equipment_Glasses", fileName = "Glasses_")]
    public class GlassesData : ItemData
    {
        public string mainStatKey = "ġȮ";

        public List<string> subStatKeys = new List<string>
        {
            "ü��", "����", "����", "���׹̳�", "���ȹ��� ����", "���ݷ�", "�̼�", "����"
        };
    }
}
