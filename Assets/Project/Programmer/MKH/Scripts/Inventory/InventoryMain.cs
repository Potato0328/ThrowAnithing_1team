using UnityEngine;

namespace MKH
{
    public class InventoryMain : InventoryBase
    {
        public static bool IsInventoryActive = false;


        new private void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            TryOpenInventory();
        }

        private void TryOpenInventory()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!IsInventoryActive)
                    OpenInventory();
                else
                    CloseInventory();
            }
        }

        private void OpenInventory()
        {
            mInventoryBase.SetActive(true);
            IsInventoryActive = true;
        }

        private void CloseInventory()
        {
            mInventoryBase.SetActive(false);
            IsInventoryActive = false;
        }

        public void AcquireItem(Item item)
        {
            for (int i = 0; i < mSlots.Length; i++)
            {
                //����ũ�� ����Ͽ� �ش� ������ ����ũ�� ���Ǵ� ��ġ�ΰ�쿡�� �������� ����ֵ��� �Ѵ�.
                if (mSlots[i].Item == null && mSlots[i].IsMask(item))
                {
                    mSlots[i].AddItem(item);
                    return;
                }
            }

        }
    }
}
