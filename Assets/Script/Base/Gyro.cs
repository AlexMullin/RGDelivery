using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyro : MonoBehaviour
{
    protected bool gyroEnabled = false;
    protected Gyroscope gyro;

    public static bool 
        flipX = false, 
        flipY = false, 
        flipZ = false;


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

        Vector3 temp = currentRotation.eulerAngles;
        temp = new Vector3
            (
                temp.x * ((flipX) ? -1 : 1),
                temp.y * ((flipY) ? -1 : 1),
                temp.z * ((flipZ) ? -1 : 1)
            );

        currentRotation = Quaternion.Euler (temp);

        return currentRotation;
    }

    public void reverseInputX () { flipX = !flipX; }

    public void reverseInputY () { flipY = !flipY; }

    public void reverseInputZ () { flipZ = !flipZ; }

    public void CalibrateGyro()
    {
        //Debug.Log ("Current Rotation: " + baseRotation);
        //baseRotation = gyro.attitude;
        //Debug.Log ("New Rotation: " + baseRotation);
    }
}
