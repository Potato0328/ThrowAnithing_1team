using UnityEngine;
using UnityEngine.UI;

namespace MKH
{
    // �κ��丮 �� ĭ(����)�� ǥ���ϴ� Ŭ����
    public class InventorySlot : MonoBehaviour
    {
        // ������ ������ ������ �̹���
        [Header("���Կ� �ִ� UI ������Ʈ")]
        [SerializeField] private Image mItemImage;
        public Image ItemImage { get { return mItemImage; } set { mItemImage = value; } }

        // ��� ����/���� ó�� ��� �Ŵ���
        [Header("��� ��ü �Ŵ���")]
        [SerializeField] private EquipActionManager equipActionManager;

        // �κ��丮 ĭ, ��� ĭ ����
        public bool isEquip;

        // ���� ���Կ� ��� �ִ� ������
        [Header("���")]
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
        /// ��� �̹��� ����
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
        /// ��� �̹��� ���� ����
        /// </summary>
        private void SetColor(float alpha)
        {
            Color color = mItemImage.color;
            color.a = alpha;
            mItemImage.color = color;
        }

        /// <summary>
        /// ��� �߰�
        /// </summary>
        public void AddItem(Item item)
        {
            Item = item;
        }

        /// <summary>
        /// ��� �ʱ�ȭ
        /// </summary>
        public void ClearSlot()
        {
            Item = null;
        }

        /// <summary>
        /// ��� ���� Ȯ��
        /// </summary>
        public bool IsEmpty()
        {
            return mItem == null;
        }

        /// <summary>
        /// ��� ���
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
        /// ��� �κ��丮 -> ���� �̵�
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
