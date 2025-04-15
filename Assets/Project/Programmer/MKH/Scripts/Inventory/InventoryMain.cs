using System.Collections.Generic;

namespace MKH
{
    /// <summary>
    /// �κ��丮�� �ֿ� ������ ó���ϴ� Ŭ����. 
    /// ������ ȹ��, ���� ���� ���� ����� ����մϴ�.
    /// </summary>
    public class InventoryMain : InventoryBase
    {
        /// <summary>
        /// �θ� Ŭ������ Awake�� ��������� ȣ�� (new Ű����� ���� ó����).
        /// </summary>
        new public void Awake()
        {
            base.Awake();
        }

        /// <summary>
        /// �������� ȹ���Ͽ� �κ��丮�� �� ���Կ� �߰��մϴ�.
        /// </summary>
        public void AcquireItem(Item item)
        {
            Item newItem = item.Create(); // ������ ����

            foreach (var slot in mSlots)
            {
                if (slot.IsEmpty())
                {
                    slot.AddItem(newItem);
                    return;
                }
            }
        }

        /// <summary>
        /// �������� �߰��� �� �ִ� �� ������ ã���ϴ�.
        /// </summary>
        /// <returns>�� ������ �ִٸ� �ش� ����, ���ٸ� null ��ȯ</returns>
        public InventorySlot IsCanAquireItem(Item item)
        {
            foreach (var slot in mSlots)
            {
                if (slot.IsEmpty())
                {
                    return slot;
                }
            }
            return null;
        }

        public List<InventorySlot> GetInventorySlots()
        {
            return mSlots;
        }
    }
}
