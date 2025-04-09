using UnityEngine;


public enum ItemType { None, Helmet, Shirts, Glasses, Gloves, Pants, Earring, Ring, Shoes, Necklace }

public enum RateType { Nomal, Magic, Rare }

[CreateAssetMenu(fileName = "Item_", menuName = "Add Item/Equipment")]
public class Test_Item : ScriptableObject
{
    [Header("�̸�")] public string itemName;
    [Header("����"), Multiline] public string itemDescription;
    [Header("������ Ÿ��")] public ItemType itemType;
    [Header("������ ���")] public RateType rateType;
    [Header("�̹���")] public Sprite itemImage;
    [Header("��� ������ ȿ��")] public MEquipmentEffect effect;
}
