using UnityEngine;

namespace MKH
{
    public class EquipActionManagerTest : MonoBehaviour
    {
        [Header("��� �κ��丮")]
        [SerializeField] private EquipmentInventoryTest mEquipmentInventory;

        // ��� ��ü
        public bool UseEquip(Item item)
        {
            InventorySlotTest equipmentSlot = mEquipmentInventory.GetEquipmentSlot(item.Type);

            Item tempItem = equipmentSlot.Item;

            if (tempItem != null)
            {
                equipmentSlot.ClearSlot();
                equipmentSlot.AddItem(item);
            }
            else
            {
                equipmentSlot.AddItem(item);
            }

            mEquipmentInventory.CalculateEffect();
            return true;
        }

        // ��� ����
        public bool RemoveEquip(Item item)
        {
            if (item != null)
            {
                InventorySlotTest equipmentSlot = mEquipmentInventory.GetEquipmentSlot(item.Type);

                equipmentSlot.ClearSlot();
            }

            mEquipmentInventory.CalculateEffect();
            return true;
        }
    }
}
