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
        private Item mItem;
        public Item Item
        {
            get { return mItem; }
            set
            {
                mItem = value;
                UpdateUI();
            }
        }

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
        /// 아이템 이미지의 투명도를 설정합니다.
        /// </summary>
        private void SetColor(float alpha)
        {
            Color color = mItemImage.color;
            color.a = alpha;
            mItemImage.color = color;
        }

        /// <summary>
        /// 슬롯에 아이템을 추가하고 UI 이미지를 설정합니다.
        /// </summary>
        public void AddItem(Item item)
        {
            Item = item;
        }

        /// <summary>
        /// 슬롯을 비우고 UI를 초기화합니다.
        /// </summary>
        public void ClearSlot()
        {
            Item = null;
        }

        /// <summary>
        /// 슬롯이 비어있는지 여부를 반환합니다.
        /// </summary>
        public bool IsEmpty()
        {
            return mItem == null;
        }

        /// <summary>
        /// 아이템 사용 요청. 장비 아이템이면 장착 시도합니다.
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
        /// 장비를 장착 슬롯으로 이동시킵니다.
        /// 현재 슬롯은 비우고 장비 매니저에게 전달합니다.
        /// </summary>
        public void ChangeEquipmentSlot()
        {
            Item item = mItem;
            ClearSlot();
            equipActionManager.UseEquip(item);
        }
    }
}
