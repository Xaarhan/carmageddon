using UnityEngine;
using System.Collections;
using System;


[Serializable]
public class BuffProps 
{

    // Use this for initialization
    public BuffProps() {

    }


    public BuffProps clone() {
        BuffProps clon = new BuffProps();
        clon.id = id;
        clon.duration = duration;
        clon.animname = animname;
        clon.type = type;
        return clon;
    }

    public int id;
    public int duration;
    public float value;
    public string animname;
    public BuffTypes type;

}
