using MKH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    public string itemName;
    [TextArea] public string description;
    public ItemType itemType;
    public RateType rateType;
    public Sprite icon;
    public GameObject prefab;
}
