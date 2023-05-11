using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acceleration : Gyro
{
    public float flickCooldownTime = 1.0f;

    protected bool flickReady = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start ();
    }

    protected bool checkFlick ()
    {
        
        if (flickReady && gyro.userAcceleration.magnitude > GameSettings.shakeThreshold) 
        {
            Debug.Log ("Phone Flicked");

            StartCoroutine (flickCooldown ());
            return true;
        }

        return false;
    }

    IEnumerator flickCooldown ()
    {
        flickReady = false;

        yield return new WaitForSeconds (flickCooldownTime);

        flickReady = true;
    }
}
