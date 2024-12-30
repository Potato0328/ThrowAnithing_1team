using UnityEngine;

namespace MKH
{
    public class ItemTriggerTest : MonoBehaviour
    {
        private ItemPickUp mCurrentItem;

        [SerializeField] private InventoryMainTest  mInventory;      // �κ��丮

        private void OnTriggerEnter(Collider other)
        {
            // other�� ������Ʈ���� ItemPickUp �ҷ�����
            mCurrentItem = other.transform.GetComponent<ItemPickUp>();

            // Item �±� ���� other ������Ʈ
            if (other.gameObject.tag == Tag.Item)
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
