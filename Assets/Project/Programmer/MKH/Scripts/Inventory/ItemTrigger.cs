using UnityEngine;

namespace MKH
{
    public class ItemTrigger : MonoBehaviour
    {
        private ItemPickUp mCurrentItem;

        [SerializeField] private InventoryMain mInventory;      // �κ��丮

        private void OnTriggerEnter(Collider other)
        {
            // other�� ������Ʈ���� ItemPickUp �ҷ�����
            mCurrentItem = other.transform.GetComponent<ItemPickUp>();

            // Item �±� ���� other ������Ʈ
            if (other.CompareTag("Item"))
            {
                // ������ Ÿ���� None�� �ƴ� �� 
                if (mCurrentItem.Item.Type != ItemType.None)
                {
                    // �κ��丮�� ������ �߰�
                    mInventory.AcquireItem(mCurrentItem.Item);
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
