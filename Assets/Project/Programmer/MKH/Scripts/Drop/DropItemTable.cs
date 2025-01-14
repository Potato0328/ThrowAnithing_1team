using BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    public class DropItemTable : ScriptableObject
    {
        [Header("DropList ��ũ���ͺ������Ʈ")]
        [SerializeField] public DropList[] dropLists;

        [Header("������ ���� ��� �ð�")]
        [SerializeField] public float _destroyItemTime;

        public virtual DropItemTable DropListTable1(GameObject obj, Vector3 pos, Quaternion rot)
        {
            return null;
        }

        public virtual DropItemTable DropListTable2(GameObject obj, Vector3 pos, Quaternion rot)
        {
            return null;
        }
    }
}
