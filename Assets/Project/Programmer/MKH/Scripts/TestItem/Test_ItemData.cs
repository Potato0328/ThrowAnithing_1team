using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test_ItemData : MonoBehaviour
{
    public string _name;
    [SerializeField] Test_Item item;

    private void Update()
    {
        State();
    }

    private void State()
    {
        _name = item.itemName;
    }
}
