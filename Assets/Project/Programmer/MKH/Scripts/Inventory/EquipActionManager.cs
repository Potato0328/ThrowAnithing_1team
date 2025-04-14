
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

            if (equipmentSlot == null)
            {
                return false;
            }

            Item tempItem = equipmentSlot.Item;

            equipmentSlot.AddItem(item);

            if (tempItem != null && inventorySlot != null)
            {
                inventorySlot.AddItem(tempItem);
            }

            mEquipmentInventory.CalculateEffect();
            return true;
        }
    }
}
