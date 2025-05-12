using System.Collections.Generic;

namespace MKH
{
    /// <summary>
    /// 인벤토리의 주요 동작을 처리하는 클래스. 
    /// 아이템 획득, 슬롯 정렬 등의 기능을 담당합니다.
    /// </summary>
    public class InventoryMain : InventoryBase
    {
        /// <summary>
        /// 부모 클래스의 Awake를 명시적으로 호출 (new 키워드로 숨김 처리됨).
        /// </summary>
        new public void Awake()
        {
            base.Awake();
        }

        /// <summary>
        /// 아이템을 획득하여 인벤토리의 빈 슬롯에 추가합니다.
        /// </summary>
        public void AcquireItem(ItemData item)
        {
            Item newItem = ItemSystemInitializer.Factory.Create(item); // 아이템 복제

            foreach (var slot in mSlots)
            {
                if (slot.IsEmpty())
                {
                    slot.AddItem(newItem);
                    return;
                }
            }
        }

        /// <summary>
        /// 아이템을 추가할 수 있는 빈 슬롯을 찾습니다.
        /// </summary>
        /// <returns>빈 슬롯이 있다면 해당 슬롯, 없다면 null 반환</returns>
        public InventorySlot IsCanAquireItem(Item item)
        {
            foreach (var slot in mSlots)
            {
                if (slot.IsEmpty())
                {
                    return slot;
                }
            }
            return null;
        }

        public List<InventorySlot> GetInventorySlots()
        {
            return mSlots;
        }
    }
}
