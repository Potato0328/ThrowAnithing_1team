using UnityEngine;

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
            Item _item = item.Create(); // ������ ����

            for (int i = 0; i < mSlots.Length; i++)
            {
                // �� ������ ���� ��� �������� �߰��ϰ� ����
                if (mSlots[i].IsEmpty())
                {
                    mSlots[i].AddItem(_item);
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
            for (int i = 0; i < mSlots.Length; i++)
            {
                if (mSlots[i].IsEmpty())
                {
                    return mSlots[i];
                }
            }
            return null;
        }

        /// <summary>
        /// �κ��丮 ������ �����Ͽ� �� ĭ ���� �������� ���ϴ�.
        /// ������ ��� �ְ�, �ڿ� �������� ������ ������ �̵���Ŵ.
        /// </summary>
        public void Sorting()
        {
            for (int i = 0; i < mSlots.Length - 1; i++)
            {
                // �� ������ ����ְ�, �� ���Կ� �������� �ִ� ���
                if (mSlots[i].IsEmpty() && !mSlots[i + 1].IsEmpty())
                {
                    // �� ������ �������� �� �������� �ű��, �� ������ ���
                    mSlots[i].AddItem(mSlots[i + 1].Item);
                    mSlots[i + 1].ClearSlot();
                }
            }
        }
    }
}
