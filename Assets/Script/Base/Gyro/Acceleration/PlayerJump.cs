using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : Acceleration
{
    public float jumpHeight = 5;

    // Update is called once per frame
    void Update()
    {
        if ( checkFlick () )
        {
            GetComponent<Rigidbody> ().AddForce( Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }
}
