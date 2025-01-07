using UnityEngine;

namespace MKH
{
    public class InventoryMainTest : InventoryBaseTest
    {
        [SerializeField] GameObject state;

        new private void Awake()
        {
            base.Awake();
            state.SetActive(false);
        }

        public void AcquireItem(Item item)
        {
            Item _item = item.Create();

            for (int i = 0; i < mSlots.Length; i++)
            {
                //����ũ�� ����Ͽ� �ش� ������ ����ũ�� ���Ǵ� ��ġ�ΰ�쿡�� �������� ����ֵ��� �Ѵ�.
                if (mSlots[i].Item == null && mSlots[i].IsMask(_item))
                {
                    mSlots[i].AddItem(_item);
                    return;
                }
            }
        }
        public InventorySlotTest IsCanAquireItem(Item item)
        {
            foreach (InventorySlotTest slot in base.mSlots)
            {
                //����ִ� ������ �߰��Ѱ��
                if (slot.Item == null) { return slot; }

            }

            return null;
        }

        public void Sorting()
        {
            for (int i = 0; i < mSlots.Length - 1; i++)
            {
                if (mSlots[i].Item == null)
                {
                    mSlots[i].ItemImage.sprite = mSlots[i + 1].ItemImage.sprite;
                    mSlots[i].ItemImage.color = mSlots[i + 1].ItemImage.color;
                    mSlots[i].Item = mSlots[i + 1].Item;
                    mSlots[i + 1].Item = null;

                    if(mSlots[i].ItemImage.sprite == null)
                    {
                        Color color = mSlots[i].ItemImage.color;
                        color.a = 0f;
                        mSlots[i].ItemImage.color = color;
                    }
                }
            }
        }
    }
}


