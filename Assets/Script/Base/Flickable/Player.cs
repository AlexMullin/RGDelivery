using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Flickable
{
    CameraMovement camScript;
    // Start is called before the first frame update
    protected override void Start ()
    {
        base.Start ();

        camScript = cam.GetComponent<CameraMovement> ();

        camScript.followChargeSpeed = 1 / (flickLaunchBuildupMax * 10);

    }

    protected override void Update ()
    {
        base.Update ();


        //If the player is building up speed, move the camera closer to the
        //charge point
        if (touchSingle && touchActive )
        {
            camScript.updateChargePosition (true);
        }
        else
        {
            //Otherwise bring it back to the default position quickly.
            camScript.updateChargePosition(false);
        }


        if (transform.position.y < -10 )
        {
            SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
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
