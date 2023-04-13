using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyro : MonoBehaviour
{
    protected bool gyroEnabled = false;
    protected Gyroscope gyro;

    private Quaternion baseRotation = Quaternion.identity;

    // Start is called before the first frame update
    protected virtual void Start ()
    {


        //If there's an error, tell me
        if (! EnableGyro ())
        {
            enabled = false;
            
            Debug.Log (name + ": Gyro not found");
        }
    }


    //Enable the gyro and set some important parameters
    private bool EnableGyro ()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyroEnabled = true;
            gyro = Input.gyro;

            Input.gyro.enabled = true;

            baseRotation = Quaternion.Euler(0, 0, 0);
            
            return true;
        }

        return false;
    }

    protected Quaternion GyroToUnity ()
    {
        Quaternion q = gyro.attitude;

        return q * baseRotation;
    }
}
