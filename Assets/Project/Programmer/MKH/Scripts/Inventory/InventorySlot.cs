using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MKH
{
    public class InventorySlot : MonoBehaviour
    {
        // ������
        private Item mItem;
        public Item Item { get { return mItem; } set { mItem = value; } }

        [Header("�ش� ���Կ� �� �� �ִ� Ÿ��")]
        [SerializeField] private ItemType mSlotMask;

        [Header("���Կ� �ִ� UI ������Ʈ")]
        [SerializeField] private Image mItemImage;
        public Image ItemImage { get {  return mItemImage; } set { mItemImage = value; } }

        [Header("��� ��ü �Ŵ���")]
        [SerializeField] private EquipActionManager equipActionManager;

        // ������ �̹��� ���� ����
        private void SetColor(float _alpha)
        {
            Color color = mItemImage.color;
            color.a = _alpha;

            // ��޿� ���� �̹��� ��
            if (mItem.Rate == RateType.Nomal)
            {
                color.r = 0f;
                color.g = 0f;
                color.b = 0f;
            }
            else if (mItem.Rate == RateType.Magic)
            {
                color.r = 129f / 255f;
                color.g = 193f / 255f;
                color.b = 71f / 255f;
            }
            else if (mItem.Rate == RateType.Rare)
            {
                color.r = 0f;
                color.g = 0f;
                color.b = 255f / 255f;
            }

            mItemImage.color = color;
        }

        // ������ �� Ÿ��
        public bool IsMask(Item item)
        {
            return ((int)item.Type & (int)mSlotMask) == 0 ? false : true;
        }

        // ������ ����
        public void AddItem(Item item)
        {
            mItem = item;
            mItemImage.sprite = mItem.Image;
            SetColor(1);
        }

        // ������ ����
        public void ClearSlot()
        {
            mItem = null;
            mItemImage.sprite = null;
        }

        // ������ ���
        public void UseItem()
        {
            if (mItem != null)
            {
                if(mItem.Type >= ItemType.Helmet && mItem.Type <= ItemType.Necklace)
                {
                    ChangeEquipmentSlot();
                }
            }
        }

        // ��� ��ü
        public void ChangeEquipmentSlot()
        {
            Item item = mItem;
            ClearSlot();
            equipActionManager.UseEquip(item);
        }

        // ��� ����
        public void RemoveEquipmentSlot()
        {
            equipActionManager.RemoveEquip(mItem);
        }
    }
}
