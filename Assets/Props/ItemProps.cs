using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class ItemProps
{
    [SerializeField]
    public ItemTypes type;
    public int count;
    public BuffTypes buffType;
    public TargetTypes targetType;
    public string name;

    // не используется
    public string icon;

    public ItemProps() {
        name = "def_item";
    }


    public ItemProps clone() {
        ItemProps clon = new ItemProps();
        clon.type = type;
        clon.count = count;
        clon.name = name;
        clon.buffType = buffType;
        clon.targetType = targetType;
        clon.icon = icon;
        return clon;
    }
    
}
