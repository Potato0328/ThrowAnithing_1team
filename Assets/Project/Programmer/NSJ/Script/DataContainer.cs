using MKH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContainer : MonoBehaviour
{
    public static DataContainer Instance;
    [SerializeField] BaseEnemy[] _monsters;
    /// <summary>
    /// ���� ������
    /// </summary>
    public static BaseEnemy[] Monsters { get { return Instance._monsters; } }
    [SerializeField] AdditionalEffect[] _blueChips;
    /// <summary>
    /// ���Ĩ ������
    /// </summary>
    public static AdditionalEffect[] BlueChips { get { return Instance._blueChips; } }
    [SerializeField]private TestBlueChip _blueChipItem;
    public static TestBlueChip BlueChipItem { get { return Instance._blueChipItem; } }
    [System.Serializable]
    public struct ItemStruct
    {
        /// <summary>
        /// �븻 ������ (1�ܰ�)
        /// </summary>
        public DropList NormalItems;
        /// <summary>
        /// ���� ������ (2�ܰ�)
        /// </summary>
        public DropList MagicItems;
        /// <summary>
        /// ���� ������ (3�ܰ�)
        /// </summary>
        public DropList RareItems;
    }
    [SerializeField] private ItemStruct _items;
    /// <summary>
    /// ������ ������
    /// </summary>
    public static ItemStruct Items { get { return Instance._items; } }
    public static List<DropList> ItemList = new List<DropList>();

    [SerializeField] ThrowObject[] _throwObjects;
    public static ThrowObject[] ThrowObjects { get { return Instance._throwObjects; } }
    private Dictionary<int, ThrowObject> _throwObjectDic = new Dictionary<int, ThrowObject>();
    private static Dictionary<int, ThrowObject> s_ThrowObjectDic { get { return Instance._throwObjectDic; } }
    [SerializeField] ArmUnit[] _armUnits;
    private Dictionary<GlobalGameData.AmWeapon, ArmUnit> _armUnitDic = new Dictionary<GlobalGameData.AmWeapon, ArmUnit>();
    private static Dictionary<GlobalGameData.AmWeapon, ArmUnit> s_armUnitDic { get { return Instance._armUnitDic; } }
    private void Awake()
    {
        InitSingleTon();
   
        foreach (ThrowObject throwObject in _throwObjects)
        {
            _throwObjectDic.Add(throwObject.Data.ID, throwObject);
        }
        foreach(ArmUnit armUnit in _armUnits)
        {
            _armUnitDic.Add(armUnit.ArmType, armUnit);
        }
        ItemList.Add(Items.NormalItems);
        ItemList.Add(Items.MagicItems);
        ItemList.Add(Items.RareItems);
    }

    /// <summary>
    /// ��ô������Ʈ ���
    /// </summary>
    public static ThrowObject GetThrowObject(int ID)
    {
        return  s_ThrowObjectDic[ID];
    }
    /// <summary>
    /// ������ ���
    /// </summary>
    public static ArmUnit GetArmUnit(GlobalGameData.AmWeapon armUnit)
    {
        return s_armUnitDic[armUnit];
    }
    /// <summary>
    /// ���� ������ ������Ʈ ���
    /// </summary>
    public static GameObject GetItemPrefab(DropList dropList)
    {
        return dropList.itemList[Random.Range(0, dropList.Count)];
    }
    public static GameObject GetItemPrefab()
    {
        DropList dropList = ItemList[Random.Range(0, ItemList.Count)];
        return dropList.itemList[Random.Range(0, dropList.Count)];
    }

    public static TestBlueChip CreateRandomBlueChip(Vector3 pos, Quaternion rot)
    {
        TestBlueChip testBlueChip = Instantiate(BlueChipItem, pos, rot);
        testBlueChip.Effect = BlueChips[Random.Range(0, BlueChips.Length)];
        testBlueChip.SetBlueChip(testBlueChip.Effect);
        return testBlueChip;
    }
    public static TestBlueChip CreateRandomBlueChip(Transform transform)
    {
        TestBlueChip testBlueChip = Instantiate(BlueChipItem, transform.position, transform.rotation);
        testBlueChip.transform.SetParent(transform, true);
        testBlueChip.Effect = BlueChips[Random.Range(0, BlueChips.Length)];
        testBlueChip.SetBlueChip(testBlueChip.Effect);
        return testBlueChip;
    }
    private void InitSingleTon()
    {
        if(Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
