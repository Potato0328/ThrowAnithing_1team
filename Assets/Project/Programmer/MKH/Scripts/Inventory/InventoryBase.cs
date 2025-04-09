using UnityEngine;

namespace MKH
{
    abstract public class InventoryBase : MonoBehaviour
    {
        [SerializeField] protected GameObject mInventorySlotsParent;
        [SerializeField] public InventorySlot[] mSlots;

        protected void Awake()
        {
             mSlots = mInventorySlotsParent.GetComponentsInChildren<InventorySlot>();
        }
    }
}

