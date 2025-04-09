using UnityEngine;


public enum ItemType { None, Helmet, Shirts, Glasses, Gloves, Pants, Earring, Ring, Shoes, Necklace }

public enum RateType { Nomal, Magic, Rare }

[CreateAssetMenu(fileName = "Item_", menuName = "Add Item/Equipment")]
public class Test_Item : ScriptableObject
{
    [Header("이름")] public string itemName;
    [Header("설명"), Multiline] public string itemDescription;
    [Header("아이템 타입")] public ItemType itemType;
    [Header("아이템 등급")] public RateType rateType;
    [Header("이미지")] public Sprite itemImage;
    [Header("장비 아이템 효과")] public MEquipmentEffect effect;
}
