using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyro : MonoBehaviour
{
    protected bool gyroEnabled = false;
    protected Gyroscope gyro;


    // Start is called before the first frame update
    protected virtual void Start ()
    {
        //baseRotation = Quaternion.Euler (Vector3.zero);

        //If there's an error, tell me
        if (! EnableGyro ())
        {
            enabled = false;
            
            Debug.Log (name + ": Gyro not found");
        }


        if(GameObject.Find("GyroContainer") == null)
        {
            Instantiate (new GameObject ("GyroContainer"), new Vector3 (0, 0, 0), Quaternion.Euler(90, 0, 90));
        }

        transform.parent = GameObject.Find ("GyroContainer").transform;

    }


    //Enable the gyro and set some important parameters
    private bool EnableGyro ()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyroEnabled = true;
            gyro = Input.gyro;

            Input.gyro.enabled = true;
            
            return true;
        }

        return false;
    }

    protected Quaternion GyroToUnity ()
    {
        Quaternion q = gyro.attitude;

        Quaternion currentRotation = new Quaternion (q.x, q.y, -q.z, -q.w);


        return currentRotation;
    }

    public void CalibrateGyro()
    {
        //Debug.Log ("Current Rotation: " + baseRotation);
        //baseRotation = gyro.attitude;
        //Debug.Log ("New Rotation: " + baseRotation);
    }
}
