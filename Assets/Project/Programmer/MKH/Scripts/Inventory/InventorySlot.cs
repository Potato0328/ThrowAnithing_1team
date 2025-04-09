using UnityEngine;
using UnityEngine.UI;

namespace MKH
{
    // �κ��丮 �� ĭ(����)�� ǥ���ϴ� Ŭ����
    public class InventorySlot : MonoBehaviour
    {
        // ���� ���Կ� ��� �ִ� ������
        private Item mItem;
        public Item Item { get { return mItem; } set { mItem = value; } }

        // ������ ������ ������ �̹���
        [Header("���Կ� �ִ� UI ������Ʈ")]
        [SerializeField] private Image mItemImage;
        public Image ItemImage { get { return mItemImage; } set { mItemImage = value; } }

        // ��� ����/���� ó�� ��� �Ŵ���
        [Header("��� ��ü �Ŵ���")]
        [SerializeField] private EquipActionManager equipActionManager;

        // �κ��丮 ĭ, ��� ĭ ����
        public bool isEquip;

        /// <summary>
        /// ������ �̹����� ������ �����մϴ�.
        /// </summary>
        private void SetColor(float _alpha)
        {
            Color color = mItemImage.color;
            color.a = _alpha;
            mItemImage.color = color;
        }

        /// <summary>
        /// ���Կ� �������� �߰��ϰ� UI �̹����� �����մϴ�.
        /// </summary>
        public void AddItem(Item item)
        {
            mItem = item;
            mItemImage.sprite = mItem.Image;
            SetColor(1); // ���̰� ����
        }

        /// <summary>
        /// ������ ���� UI�� �ʱ�ȭ�մϴ�.
        /// </summary>
        public void ClearSlot()
        {
            mItem = null;
            mItemImage.sprite = null;
            SetColor(0); // �����ϰ�
        }

        /// <summary>
        /// ������ ����ִ��� ���θ� ��ȯ�մϴ�.
        /// </summary>
        public bool IsEmpty()
        {
            return mItem == null;
        }

        /// <summary>
        /// ������ ��� ��û. ��� �������̸� ���� �õ��մϴ�.
        /// </summary>
        public void UseItem()
        {
            if (mItem != null)
            {
                if (mItem.Type >= ItemType.Helmet && mItem.Type <= ItemType.Necklace)
                {
                    ChangeEquipmentSlot();
                }
            }
        }

        /// <summary>
        /// ��� ���� �������� �̵���ŵ�ϴ�.
        /// ���� ������ ���� ��� �Ŵ������� �����մϴ�.
        /// </summary>
        public void ChangeEquipmentSlot()
        {
            Item item = mItem;
            ClearSlot();
            equipActionManager.UseEquip(item);
        }

        /// <summary>
        /// ��� �����մϴ�.
        /// </summary>
        public void RemoveEquipmentSlot()
        {
            equipActionManager.RemoveEquip(mItem);
        }
    }
}
