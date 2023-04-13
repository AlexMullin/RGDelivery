using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_Gyro : Gyro
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start ();

        Debug.Log (gyroEnabled);
    }

    // Update is called once per frame
    void Update()
    {
        if (gyroEnabled)
        {
            transform.rotation = GyroToUnity();
        }
    }
}
