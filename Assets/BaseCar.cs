using UnityEngine;
using System.Collections;

public class BaseCar : MonoBehaviour
{

    // Use this for initialization
    void Start() {
        body.centerOfMass = new Vector3(0, 0.1f, 0);
    }

    // Update is called once per frame
    void Update() {

    }

    public void setWheelSpeed( float value ) {
        //rightBackWheel.motorTorque = value;
      //  leftBackWheel.motorTorque = value;
        rightWheel.motorTorque = value;
        leftWheel.motorTorque = value;
    }

    public void setWeelRotation( float value ) {
        leftWheel.transform.localRotation = Quaternion.Euler(90, 90 + value, 0);
        rightWheel.transform.localRotation = Quaternion.Euler(90, 90 + value, 0);
        leftWheel.steerAngle = value;
        rightWheel.steerAngle = value;
    }


    public Transform attackPoint;
    public Rigidbody body;
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;

    public WheelCollider rightBackWheel;
    public WheelCollider leftBackWheel;
}
