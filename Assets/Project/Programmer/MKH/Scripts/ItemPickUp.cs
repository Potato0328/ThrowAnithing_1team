using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    public class ItemPickUp : MonoBehaviour
    {
        [Header("�ش� ������Ʈ�� �Ҵ�Ǵ� ������")]
        [SerializeField] private Item mItem;
        public Item Item { get { return mItem; } }

        //private void OnTriggerEnter(Collider other)
        //{
        //    if(other.CompareTag("Player"))
        //    {
        //        Destroy(gameObject);
        //    }
        //}
    }
}
