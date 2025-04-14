
using UnityEngine;

namespace MKH
{
    public class EquipActionManager : MonoBehaviour
    {
        [Header("장비 인벤토리")]
        [SerializeField] private EquipmentInventory mEquipmentInventory;
        [SerializeField] private InventoryMain minventoryMain;

        /// <summary>
        /// 장비 장착, 교체
        /// 장비 효과 업데이트
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
