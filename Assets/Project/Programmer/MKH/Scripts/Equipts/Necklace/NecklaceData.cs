using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(menuName = "Add Item/Equipment_Necklace", fileName = "Necklace_")]
    public class NecklaceData : ItemData
    {
        public string mainStatKey = "����";

        public List<string> subStatKeys = new List<string>
        {
            "ü��", "����", "ġȮ", "����", "���׹̳�", "���ȹ��� ����", "���ݷ�", "�̼�"
        };
    }
}
