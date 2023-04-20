using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(BoxCollider))]
public class test_Accel : Acceleration
{
    Rigidbody rb;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();

        Debug.Log (gyroEnabled);
    }

    // Update is called once per frame
    void Update()
    {

        if (checkFlick ())
        {
            Debug.Log (gyro.userAcceleration.magnitude);
            rb.AddForce (Vector3.up * flickLaunch, ForceMode.VelocityChange);
        }
    }
}
