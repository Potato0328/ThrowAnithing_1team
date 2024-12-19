using UnityEngine;

namespace MKH
{
    public class ItemTrigger : MonoBehaviour
    {
        private ItemPickUp mCurrentItem;

        [SerializeField] private InventoryMain mInventory;

        private void OnTriggerEnter(Collider other)
        {
            mCurrentItem = other.transform.GetComponent<ItemPickUp>();

            if (other.CompareTag("Item"))
            {
                print("��Ҵ�.1");
                if (mCurrentItem.Item.Type != ItemType.None)
                {
                    print("��Ҵ�.2");
                    mInventory.AcquireItem(mCurrentItem.Item);
                }
            }
        }
    }
}
