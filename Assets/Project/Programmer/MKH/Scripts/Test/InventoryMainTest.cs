using TMPro;
using UnityEngine;

namespace MKH
{
    public class InventoryMainTest : InventoryBaseTest
    {
        public static bool IsInventoryActive = false;
        [SerializeField] GameObject state;

        [SerializeField] GameObject blueChipPanel;


        new private void Awake()
        {
            base.Awake();
            state.SetActive(false);
        }

        private void Update()
        {
            //TryOpenInventory();
            //TryCloseInventory();
        }

        private void TryOpenInventory()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (!IsInventoryActive)
                    OpenInventory();
            }

        }

        private void TryCloseInventory()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (blueChipPanel.activeSelf)
                {
                    Debug.Log("1");
                    return;
                }
                
                if (IsInventoryActive && !blueChipPanel.activeSelf)
                {
                    CloseInventory();
                }
            }
        }

        private void OpenInventory()
        {
            mInventoryBase.SetActive(true);
            IsInventoryActive = true;
            state.SetActive(true);
        }

        private void CloseInventory()
        {
            mInventoryBase.SetActive(false);
            IsInventoryActive = false;
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
    }
}
