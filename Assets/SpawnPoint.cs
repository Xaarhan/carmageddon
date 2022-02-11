using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{


   
    public void reset() {

    }

    public bool isFree {
        get {
            return helper.isFree;
        }
    }


    private int _trigget_count;


    public Transform point;
    public SpawnPointHelper helper;


}
