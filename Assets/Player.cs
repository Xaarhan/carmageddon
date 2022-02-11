using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseMob
{
    void Start() {

    }

    protected override void onUpdate() {
        base.onUpdate();
        if ( Input.GetMouseButtonDown(0) ) {
             _mpos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0)) {
            _screen_vector = Input.mousePosition - _mpos;
            if (_screen_vector.x != 0) {
                float WheelRotateAngle = _screen_vector.x / Screen.width * 90f;
                car.setWeelRotation(WheelRotateAngle);
            }
        }
    }

    void FixedUpdate() {
        Vector3 v = Camera.main.transform.localPosition;
        v.x = transform.localPosition.x;
        v.z = transform.localPosition.z;
       // Camera.main.transform.localPosition = v;
      
        if ( Input.GetMouseButton(0)) {
             _force = true;
             car.setWheelSpeed(wheelSpeed); 
        } else {
            if ( !_force ) {
                 return;
            }
            _force = false;
            car.setWheelSpeed(0);
        }
    }

    private bool _force;


    private Vector3 _mpos;
    private Vector3 _screen_vector;

 


    public float wheelSpeed;
}
