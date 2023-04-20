using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acceleration : Gyro
{
    public float flickSpeed = 5.0f;
    public float flickLaunch = 10.0f;
    public float flickCooldownTime = 1.0f;

    private bool flickReady = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start ();
    }

    protected bool checkFlick ()
    {
        if (flickReady && gyro.userAcceleration.magnitude > flickSpeed) 
        {
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
