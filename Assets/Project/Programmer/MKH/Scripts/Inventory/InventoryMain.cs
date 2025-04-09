using UnityEngine;

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
        public void AcquireItem(Item item)
        {
            Item _item = item.Create(); // 아이템 복제

            for (int i = 0; i < mSlots.Length; i++)
            {
                // 빈 슬롯이 있을 경우 아이템을 추가하고 종료
                if (mSlots[i].IsEmpty())
                {
                    mSlots[i].AddItem(_item);
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
            for (int i = 0; i < mSlots.Length; i++)
            {
                if (mSlots[i].IsEmpty())
                {
                    return mSlots[i];
                }
            }
            return null;
        }

        /// <summary>
        /// 인벤토리 슬롯을 정렬하여 빈 칸 없이 아이템을 당깁니다.
        /// 앞쪽이 비어 있고, 뒤에 아이템이 있으면 앞으로 이동시킴.
        /// </summary>
        public void Sorting()
        {
            for (int i = 0; i < mSlots.Length - 1; i++)
            {
                // 앞 슬롯이 비어있고, 뒷 슬롯에 아이템이 있는 경우
                if (mSlots[i].IsEmpty() && !mSlots[i + 1].IsEmpty())
                {
                    // 뒷 슬롯의 아이템을 앞 슬롯으로 옮기고, 뒷 슬롯은 비움
                    mSlots[i].AddItem(mSlots[i + 1].Item);
                    mSlots[i + 1].ClearSlot();
                }
            }
        }
    }
}
