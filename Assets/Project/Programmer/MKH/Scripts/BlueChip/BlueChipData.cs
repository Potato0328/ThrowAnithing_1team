using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Item", menuName = "Add Item/BlueChip")]
    public class BlueChipData : ScriptableObject
    {
        [Header("���� ���̵� (�ߺ�X)")]
        [SerializeField] private int mItmeID;
        public int ItemID { get { return mItmeID; } }

        [Header("�̸�")]
        [SerializeField] private string mName;
        public string Name { get { return mName; } }

        [Header("����")]
        [SerializeField] private string mDescription;
        public string Description { get { return mDescription; } }

        [Header("�κ��丮���� �������� �̹���")]
        [SerializeField] private Sprite mItemImage;
        public Sprite Image { get { return mItemImage; } }

        [Header("������ ������Ʈ�� ������ �������� ������")]
        [SerializeField] private GameObject mItemPrefab;
        public GameObject Prefab { get { return mItemPrefab; } }

    }
}
