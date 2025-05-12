using UnityEngine;
using UnityEngine.UI;

namespace MKH
{
    // 인벤토리 한 칸(슬롯)을 표현하는 클래스
    public class InventorySlot : MonoBehaviour
    {
        // 슬롯의 아이템 아이콘 이미지
        [Header("슬롯에 있는 UI 오브젝트")]
        [SerializeField] private Image mItemImage;
        public Image ItemImage { get { return mItemImage; } set { mItemImage = value; } }

        // 장비 장착/해제 처리 담당 매니저
        [Header("장비 교체 매니저")]
        [SerializeField] private EquipActionManager equipActionManager;

        // 인벤토리 칸, 장비 칸 구별
        public bool isEquip;

        // 현재 슬롯에 들어 있는 아이템
        [Header("장비")]
        [SerializeField] private Item mItem;
        public Item Item
        {
            get { return mItem; }
            set
            {
                mItem = value;
                UpdateUI();
            }
        }

        /// <summary>
        /// 장비 이미지 셋팅
        /// </summary>
        private void UpdateUI()
        {
            if (mItem != null)
            {
                mItemImage.sprite = mItem.Image;
                SetColor(1f);
            }
            else
            {
                mItemImage.sprite = null;
                SetColor(0f);
            }
        }

        /// <summary>
        /// 장비 이미지 투명도 조절
        /// </summary>
        private void SetColor(float alpha)
        {
            Color color = mItemImage.color;
            color.a = alpha;
            mItemImage.color = color;
        }

        /// <summary>
        /// 장비 추가
        /// </summary>
        public void AddItem(Item item)
        {
            Item = item;
        }

        /// <summary>
        /// 장비 초기화
        /// </summary>
        public void ClearSlot()
        {
            Item = null;
        }

        /// <summary>
        /// 장비 여부 확인
        /// </summary>
        public bool IsEmpty()
        {
            return mItem == null;
        }

        /// <summary>
        /// 장비 사용
        /// </summary>
        public void UseItem()
        {
            if (mItem == null)
                return;

            if (mItem != null)
            {
                if (mItem.Type >= ItemType.Helmet && mItem.Type <= ItemType.Necklace)
                {
                    ChangeEquipmentSlot();
                }
            }
        }

        /// <summary>
        /// 장비를 인벤토리 -> 장비로 이동
        /// </summary>
        public void ChangeEquipmentSlot()
        {
            Item item = mItem;
            ClearSlot();
            equipActionManager.UseEquip(item);
        }

        public void RemoveItem()
        {
            Item item = mItem;
            ClearSlot();
            equipActionManager.UpdateInventorySlots();
        }
    }
}
