
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    public class EquipActionManager : MonoBehaviour
    {
        [Header("��� �κ��丮")]
        [SerializeField] private EquipmentInventory mEquipmentInventory;
        [SerializeField] private InventoryMain minventoryMain;

        /// <summary>
        /// ��� ����, ��ü
        /// ��� ȿ�� ������Ʈ
        /// </summary>
        public bool UseEquip(Item item)
        {
            InventorySlot equipmentSlot = mEquipmentInventory.GetEquipmentSlot(item.Type);
            InventorySlot inventorySlot = minventoryMain.IsCanAquireItem(item);

            Item tempItem = equipmentSlot.Item;

            equipmentSlot.AddItem(item);

            if (tempItem == null)
            {
                UpdateInventorySlots();
            }
            else if (tempItem != null)
            {
                inventorySlot.AddItem(tempItem);
            }

            mEquipmentInventory.CalculateEffect();
            UpdateInventorySlots();
            return true;
        }

        public void UpdateInventorySlots()
        {
            List<InventorySlot> slots = minventoryMain.GetInventorySlots();
            List<Item> items = new List<Item>(slots.Count);

            foreach (var slot in slots)
            {
                if (!slot.IsEmpty())
                {
                    items.Add(slot.Item);
                }
            }

            for (int i = 0; i < slots.Count; i++)
            {
                if (i < items.Count)
                {
                    slots[i].AddItem(items[i]);
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
        }

    }
}
