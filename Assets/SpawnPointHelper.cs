using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointHelper : MonoBehaviour
{
    public void OnTriggerEnter(Collider other) {
        _trigget_count++;
    }

    public void OnTriggerExit(Collider other) {
        _trigget_count--;
    }

    public bool isFree {
        get {
            return _trigget_count == 0;
        }
    }


    private int _trigget_count;
}
