using UnityEngine;

namespace MKH
{
    public class InventoryMain : InventoryBase
    {
        [SerializeField] GameObject state;

        new public void Awake()
        {
            base.Awake();
        }

        public void AcquireItem(Item item)
        {
            Item _item = item.Create();

            for (int i = 0; i < mSlots.Length; i++)
            {
                //아이템이 없을 시 넣기
                if (mSlots[i].Item == null)
                {
                    mSlots[i].AddItem(_item);
                    return;
                }
            }
        }

        public InventorySlot IsCanAquireItem(Item item)
        {
            foreach (InventorySlot slot in mSlots)
            {
                if (slot.Item == null)
                {
                    return slot;
                }
            }
            return null;
        }

        // 아이템 빈칸 없이 당기기
        public void Sorting()
        {
            for (int i = 0; i < mSlots.Length - 1; i++)
            {
                if (mSlots[i].Item == null)
                {
                    Color color1 = mSlots[i + 1].ItemImage.color;
                    color1.a = 0f;

                    mSlots[i].ItemImage.sprite = mSlots[i + 1].ItemImage.sprite;
                    mSlots[i].ItemImage.color = mSlots[i + 1].ItemImage.color;
                    mSlots[i].Item = mSlots[i + 1].Item;
                    mSlots[i + 1].Item = null;
                    mSlots[i + 1].ItemImage.color = color1;
                    mSlots[i + 1].ItemImage.sprite = null;

                    if (mSlots[i].ItemImage.sprite == null)
                    {
                        Color color = mSlots[i].ItemImage.color;
                        color.a = 0;
                        mSlots[i].ItemImage.color = color;
                    }
                }
            }
        }
    }
}
