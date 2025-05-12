using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MKH;

public class ItemFactory
{
    private Dictionary<Type, IItemCreator> _creators = new Dictionary<Type, IItemCreator>();

    public void Register<T>(IItemCreator creator) where T : ItemData
    {
        _creators[typeof(T)] = creator;
    }

    public Item Create(ItemData data)
    {
        Type type = data.GetType();

        if(_creators.TryGetValue(type, out var creator))
        {
            return creator.Create(data);
        }

        return null;
    }
}
