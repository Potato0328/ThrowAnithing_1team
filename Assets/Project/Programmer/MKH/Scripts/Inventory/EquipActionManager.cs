using UnityEngine;

namespace MKH
{
    public class EquipActionManager : MonoBehaviour
    {
        [Header("��� �κ��丮")]
        [SerializeField] private EquipmentInventory mEquipmentInventory;

        // ��� ��ü
        public bool UseEquip(Item item)
        {
            InventorySlot equipmentSlot = mEquipmentInventory.GetEquipmentSlot(item.Type);

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
                InventorySlot equipmentSlot = mEquipmentInventory.GetEquipmentSlot(item.Type);

                equipmentSlot.ClearSlot();
            }

            mEquipmentInventory.CalculateEffect();
            return true;
        }
    }
}
