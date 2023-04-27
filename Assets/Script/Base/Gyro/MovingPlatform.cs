using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class MovingPlatform : Gyro
{

    public float speedMax = 5, speedChange = 1, angleMin = 15, angleMax = 45;

    Rigidbody rb;

    // Start is called before the first frame update
    protected override void Start ()
    {
        base.Start ();

        rb = GetComponent<Rigidbody> ();
    }

    // Update is called once per frame
    void Update()
    {
        //if gyro enabled:

        if (gyroEnabled)
        {
            Quaternion tilt = GyroToUnity ();

            Vector3 movement = new Vector3(-compensateAngle(tilt.eulerAngles.x), 0, -compensateAngle(tilt.eulerAngles.y));

            movement = Quaternion.AngleAxis (Camera.main.transform.rotation.eulerAngles.y, Vector3.up)  * movement;

            if (movement.magnitude > angleMin && movement.magnitude < angleMax)
            {

                rb.velocity = Vector3.MoveTowards(rb.velocity, (movement.normalized * speedMax), speedChange * Time.deltaTime);
            }
            else
            {
                rb.velocity = Vector3.MoveTowards(rb.velocity, Vector3.zero, speedChange * Time.deltaTime);
            }

            
        }

        //Get a vector 2 of the X and Y rotations of the phone

        //Rotate that vector to be in line with the camera

        //If the phone is at a more extreme angle than level, move the platform
        //in that direction relative to the camera.
    }

    //Angles below 0 wrap back around to 360 and I dont want that
    //I'm subtracting 180 from them so they start at 180, and then Im gonna get the difference between
    //the phone's tilt and then for the angle I want.
    float compensateAngle (float angle)
    {
        //if I'm wrapping around to 360, return the difference between angle and 360
        if(angle > 180)
        {
            return angle - 360;
        }

        //if I'm tilting up from 0, return the difference between angle and 0
        else
        {
            return angle - 0;
        }
    }
}

