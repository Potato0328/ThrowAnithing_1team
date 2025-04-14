using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MKH
{
    abstract public class InventoryBase : MonoBehaviour
    {
        [SerializeField] protected GameObject mInventorySlotsParent;
        [SerializeField] public List<InventorySlot> mSlots;

        protected void Awake()
        {
             mSlots = mInventorySlotsParent.GetComponentsInChildren<InventorySlot>().ToList();
        }
    }
}

