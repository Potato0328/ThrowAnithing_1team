using UnityEngine;
using Zenject;

namespace MKH
{
    public class ItemTrigger : MonoBehaviour
    {
        [Inject]
        private PlayerData playerData;

        private ItemPickUp mCurrentItem;

        [SerializeField] private InventoryMain mInventory;      // �κ��丮

        private void Awake()
        {
            mInventory = playerData.Inventory.InventoryMain;
        }

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
