using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Flickable
{
    CameraMovement cam;
    // Start is called before the first frame update
    protected override void Start ()
    {
        base.Start ();

        cam = GameObject.Find ("Main Camera").GetComponent<CameraMovement> ();

        cam.followChargeSpeed = 1 / (flickLaunchBuildupMax * 100);

    }

    protected override void Update ()
    {
        base.Update ();


        //If the player is building up speed, move the camera closer to the
        //charge point
        if (touchSingle && touchActive )
        {
            cam.updateChargePosition (true);
        }
        else
        {
            //Otherwise bring it back to the default position quickly.
            cam.updateChargePosition(false);
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag ("PlatformMoving"))
        {
            transform.parent = other.gameObject.transform;
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if(other.CompareTag("PlatformMoving") && transform.parent == other.gameObject.transform)
        {
            transform.parent = null;
        }
    }
}
