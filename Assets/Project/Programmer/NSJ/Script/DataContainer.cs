using MKH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BaseEnemy;

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
    [SerializeField] private BlueChip _blueChipItem;
    public static BlueChip BlueChipItem { get { return Instance._blueChipItem; } }
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
        /// <summary>
        /// ��� ���Ϳ� ������
        /// </summary>
        public DropItemTable Normal;
        /// <summary>
        /// ����Ʈ ���Ϳ� ������
        /// </summary>
        public DropItemTable Mutant;
        /// <summary>
        /// ����Ʈ ���Ϳ� ������
        /// </summary>
        public DropItemTable Elite;
        /// <summary>
        /// �߰����� ���Ϳ� ������
        /// </summary>
        public DropItemTable SubBoss;
        /// <summary>
        /// ������������ ���Ϳ� ������
        /// </summary>
        public DropItemTable StageBoss;
    }
    [SerializeField] private ItemStruct _items;
    /// <summary>
    /// ������ ������
    /// </summary>
    public static ItemStruct Items { get { return Instance._items; } }
    public static List<DropList> ItemList = new List<DropList>();
    public static List<DropItemTable> ItemTableList = new List<DropItemTable>();
    private static GameObject _gameObject;

    [SerializeField] GameObject[] _itemPaticle;
    /// <summary>
    /// ������ ��ƼŬ ������
    /// </summary>
    public static GameObject[] ItemPaticle { get { return Instance._itemPaticle; } }

    [SerializeField] float _destroyItemTime = 0;
    /// <summary>
    /// ������ ������� �ð�
    /// </summary>
    public static float DestroyItemTime { get { return Instance._destroyItemTime; } }

    /// <summary>
    /// ��ô�� ������
    /// </summary>
    [SerializeField] ThrowObject[] _throwObjects;
    public static ThrowObject[] ThrowObjects { get { return Instance._throwObjects; } }
    private Dictionary<int, ThrowObject> _throwObjectDic = new Dictionary<int, ThrowObject>();
    private static Dictionary<int, ThrowObject> s_ThrowObjectDic { get { return Instance._throwObjectDic; } }
    /// <summary>
    /// ������ ������
    /// </summary>
    [SerializeField] ArmUnit[] _armUnits;
    private Dictionary<GlobalGameData.AmWeapon, ArmUnit> _armUnitDic = new Dictionary<GlobalGameData.AmWeapon, ArmUnit>();
    private static Dictionary<GlobalGameData.AmWeapon, ArmUnit> s_armUnitDic { get { return Instance._armUnitDic; } }

    [SerializeField] DamageText[] _damageTexts;
    private Dictionary<CrowdControlType, DamageText> _damageTextDic = new Dictionary<CrowdControlType, DamageText>();
    private static Dictionary<CrowdControlType, DamageText> s_damageTextDic { get { return Instance._damageTextDic; } }
    private void Awake()
    {
        Instance = this;
        InitSingleTon();

        for (int i = 0; i < _throwObjects.Length; i++)
        {
            _throwObjects[i].Data.ID = i;
            _throwObjectDic.Add(_throwObjects[i].Data.ID, _throwObjects[i]);
        }
        foreach (ArmUnit armUnit in _armUnits)
        {
            _armUnitDic.Add(armUnit.ArmType, armUnit);
        }
        foreach (DamageText damageText in _damageTexts)
        {
            _damageTextDic.Add(damageText.Type, damageText);
        }
        ItemList.Add(Items.NormalItems);
        ItemList.Add(Items.MagicItems);
        ItemList.Add(Items.RareItems);
        ItemTableList.Add(Items.Normal);
        ItemTableList.Add(Items.Mutant);
        ItemTableList.Add(Items.Elite);
        ItemTableList.Add(Items.SubBoss);
        ItemTableList.Add(Items.StageBoss);

    }

    /// <summary>
    /// ��ô������Ʈ ���
    /// </summary>
    public static ThrowObject GetThrowObject(int ID)
    {
        return s_ThrowObjectDic[ID];
    }
    /// <summary>
    /// ������ ���
    /// </summary>
    public static ArmUnit GetArmUnit(GlobalGameData.AmWeapon armUnit)
    {
        return s_armUnitDic[armUnit];
    }
    /// <summary>
    /// ������ UI ������ ���
    /// </summary>
    public static DamageText GetDamageText(CrowdControlType type)
    {
        return s_damageTextDic[type];
    }
    /// <summary>
    /// ���� ������ ������Ʈ ���
    /// </summary>
    public static GameObject GetItemPrefab(DropList dropList)
    {
        return dropList.itemList[Random.Range(0, dropList.Count)];
    }
    public static GameObject GetItemPrefab(Vector3 pos)
    {
        DropList dropList = ItemList[Random.Range(0, ItemList.Count)];
        CoroutineHandler.StartRoutine(CreateItem(pos, dropList));
        return dropList.itemList[Random.Range(0, dropList.Count)];
    }

    public static GameObject GetItemTablePrefab(Vector3 pos)
    {
        CoroutineHandler.StartRoutine(CreateItemTable(pos));
        return null;
    }

    public static BlueChip CreateRandomBlueChip(Vector3 pos, Quaternion rot)
    {
        BlueChip blueChip = Instantiate(BlueChipItem, pos, rot);
        return blueChip;
    }
    public static BlueChip CreateRandomBlueChip(Transform transform)
    {
        BlueChip blueChip = Instantiate(BlueChipItem, transform.position, transform.rotation);
        blueChip.transform.SetParent(transform, true);
        return blueChip;
    }
    private void InitSingleTon()
    {
        if (Instance == null)
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

    static IEnumerator CreateItem(Vector3 pos, DropList dropList)
    {
        GameObject startEffect = ObjectPool.GetPool(ItemPaticle[0], pos, Quaternion.Euler(-90f, 0, 0));

        yield return 0.3f.GetDelay();

        ObjectPool.ReturnPool(startEffect);

        GameObject dropPrefab = dropList.itemList[Random.Range(0, dropList.Count)];
        GameObject obj = Instantiate(dropPrefab, pos + new Vector3(0, 1, 0), Quaternion.identity);

        yield return Instance._destroyItemTime.GetDelay();
        if (obj != null)
        {
            Destroy(obj);

            GameObject endEffect = ObjectPool.GetPool(ItemPaticle[1], pos + new Vector3(0, 1, 0), Quaternion.Euler(-90f, 0, 0));

            yield return 0.5f.GetDelay();

            ObjectPool.ReturnPool(endEffect);
        }
    }

    public static BaseEnemy GetMonsterType(MonsterType type)
    {
        switch(type)
        {
            case MonsterType.Nomal:

                break;
            case MonsterType.Mutant:

                break;
            case MonsterType.Elite:

                break;
            case MonsterType.SubBoss:

                break;
            case MonsterType.Boss:

                break;
        }
        return null;
    }

    static IEnumerator CreateItemTable(Vector3 pos)
    {
        BaseEnemy enemy = new BaseEnemy();
        GameObject obj = _gameObject;

        GameObject startEffect = ObjectPool.GetPool(ItemPaticle[0], pos, Quaternion.Euler(-90f, 0, 0));

        yield return 0.3f.GetDelay();

        ObjectPool.ReturnPool(startEffect);

        switch (enemy.curMonsterType)
        {

            case MonsterType.Nomal:
                ItemTableList[0] = Items.Normal;
                Debug.Log(ItemTableList[0]);
                ItemTableList[0].DropListTable1(obj, pos + new Vector3(0, 1, 0), Quaternion.identity);
                Debug.Log("1");
                yield return Instance._destroyItemTime.GetDelay();
                if (obj != null)
                {
                    Destroy(obj);
                    Debug.Log($"{obj}�����");
                    GameObject endEffect = ObjectPool.GetPool(ItemPaticle[1], pos + new Vector3(0, 1, 0), Quaternion.Euler(-90f, 0, 0));
                    yield return 0.5f.GetDelay();
                    ObjectPool.ReturnPool(endEffect);
                }
                break;
            case MonsterType.Mutant:
                ItemTableList[1] = Items.Mutant;
                Debug.Log(ItemTableList[1]);
                ItemTableList[1].DropListTable2(obj, pos + new Vector3(0, 1, 0), Quaternion.identity);
                Debug.Log("1");
                yield return Instance._destroyItemTime.GetDelay();
                if (obj != null)
                {
                    Destroy(obj);
                    Debug.Log($"{obj}�����");
                    GameObject endEffect = ObjectPool.GetPool(ItemPaticle[1], pos + new Vector3(0, 1, 0), Quaternion.Euler(-90f, 0, 0));
                    yield return 0.5f.GetDelay();
                    ObjectPool.ReturnPool(endEffect);
                }
                break;
            case MonsterType.Elite:
                ItemTableList[2] = Items.Elite;
                Debug.Log(ItemTableList[2]);
                ItemTableList[2].DropListTable2(obj, pos + new Vector3(0, 1, 0), Quaternion.identity);
                Debug.Log("3");
                yield return Instance._destroyItemTime.GetDelay();
                if (obj != null)
                {
                    Destroy(obj);
                    Debug.Log($"{obj}�����");
                    GameObject endEffect = ObjectPool.GetPool(ItemPaticle[1], pos + new Vector3(0, 1, 0), Quaternion.Euler(-90f, 0, 0));
                    yield return 0.5f.GetDelay();
                    ObjectPool.ReturnPool(endEffect);
                }
                break;
            case MonsterType.SubBoss:
                ItemTableList[3] = Items.SubBoss;
                Debug.Log(ItemTableList[3]);
                ItemTableList[3].DropListTable2(obj, pos + new Vector3(0, 1, 0), Quaternion.identity);
                Debug.Log("4");
                yield return Instance._destroyItemTime.GetDelay();
                if (obj != null)
                {
                    Destroy(obj);
                    Debug.Log($"{obj}�����");
                    GameObject endEffect = ObjectPool.GetPool(ItemPaticle[1], pos + new Vector3(0, 1, 0), Quaternion.Euler(-90f, 0, 0));
                    yield return 0.5f.GetDelay();
                    ObjectPool.ReturnPool(endEffect);
                }
                break;
            case MonsterType.Boss:
                ItemTableList[4] = Items.StageBoss;
                Debug.Log(ItemTableList[4]);
                ItemTableList[4].DropListTable1(obj, pos + new Vector3(0, 1, 0), Quaternion.identity);
                Debug.Log($"{obj}�����");
                yield return Instance._destroyItemTime.GetDelay();
                if (obj != null)
                {
                    Destroy(obj);
                    Debug.Log("�����");
                    GameObject endEffect = ObjectPool.GetPool(ItemPaticle[1], pos + new Vector3(0, 1, 0), Quaternion.Euler(-90f, 0, 0));
                    yield return 0.5f.GetDelay();
                    ObjectPool.ReturnPool(endEffect);
                }
                break;
        }

       /* if (Monsters[Monsters.Length - 1].curMonsterType == MonsterType.Nomal)
        {
            GameObject obj = _gameObject;
            ItemTableList[0] = Items.Normal;
            Debug.Log(ItemTableList[0].name);
            Debug.Log(ItemTableList[0]);
            ItemTableList[0].DropListTable1(obj, pos + new Vector3(0, 1, 0), Quaternion.identity);
            Debug.Log("1");
            yield return Instance._destroyItemTime.GetDelay();
            if (obj != null)
            {
                Destroy(obj);
                Debug.Log($"{obj}�����");
                GameObject endEffect = ObjectPool.GetPool(ItemPaticle[1], pos + new Vector3(0, 1, 0), Quaternion.Euler(-90f, 0, 0));
                yield return 0.5f.GetDelay();
                ObjectPool.ReturnPool(endEffect);
            }
        }
        if (Monsters[Monsters.Length - 1].curMonsterType == MonsterType.Mutant)
        {
            GameObject obj = _gameObject;
            ItemTableList[1] = Items.Mutant;
            ItemTableList[1].DropListTable2(obj, pos + new Vector3(0, 1, 0), Quaternion.identity);
            Debug.Log("2");
            yield return Instance._destroyItemTime.GetDelay();
            if (obj != null)
            {
                Destroy(obj);
                Debug.Log($"{obj}�����");
                GameObject endEffect = ObjectPool.GetPool(ItemPaticle[1], pos + new Vector3(0, 1, 0), Quaternion.Euler(-90f, 0, 0));
                yield return 0.5f.GetDelay();
                ObjectPool.ReturnPool(endEffect);
            }
        }
        if (Monsters[Monsters.Length - 1].curMonsterType == MonsterType.Elite)
        {
            GameObject obj = _gameObject;
            ItemTableList[2] = Items.Elite;
            ItemTableList[2].DropListTable2(obj, pos + new Vector3(0, 1, 0), Quaternion.identity);
            Debug.Log("3");
            yield return Instance._destroyItemTime.GetDelay();
            if (obj != null)
            {
                Destroy(obj);
                Debug.Log($"{obj}�����");
                GameObject endEffect = ObjectPool.GetPool(ItemPaticle[1], pos + new Vector3(0, 1, 0), Quaternion.Euler(-90f, 0, 0));
                yield return 0.5f.GetDelay();
                ObjectPool.ReturnPool(endEffect);
            }
        }
        if (Monsters[Monsters.Length - 1].curMonsterType == MonsterType.SubBoss)
        {
            GameObject obj = _gameObject;
            ItemTableList[3] = Items.SubBoss;
            ItemTableList[3].DropListTable2(obj, pos + new Vector3(0, 1, 0), Quaternion.identity);
            Debug.Log("4");
            yield return Instance._destroyItemTime.GetDelay();
            if (obj != null)
            {
                Destroy(obj);
                Debug.Log($"{obj}�����");
                GameObject endEffect = ObjectPool.GetPool(ItemPaticle[1], pos + new Vector3(0, 1, 0), Quaternion.Euler(-90f, 0, 0));
                yield return 0.5f.GetDelay();
                ObjectPool.ReturnPool(endEffect);
            }
        }
        if (Monsters[Monsters.Length - 1].curMonsterType == MonsterType.Boss)
        {
            GameObject obj = _gameObject;
            ItemTableList[4] = Items.StageBoss;
            ItemTableList[4].DropListTable1(obj, pos + new Vector3(0, 1, 0), Quaternion.identity);
            Debug.Log($"{obj}�����");
            yield return Instance._destroyItemTime.GetDelay();
            if (obj != null)
            {
                Destroy(obj);
                Debug.Log("�����");
                GameObject endEffect = ObjectPool.GetPool(ItemPaticle[1], pos + new Vector3(0, 1, 0), Quaternion.Euler(-90f, 0, 0));
                yield return 0.5f.GetDelay();
                ObjectPool.ReturnPool(endEffect);
            }
        }*/
    }
}
