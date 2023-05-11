using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Flickable
{
    CameraMovement camScript;
    // Start is called before the first frame update

    public GameObject [] Rings;

    public Material mat;

    public Color [] ChargeEmission;
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
            foreach (GameObject ring in Rings )
            {
                MeshRenderer col = ring.GetComponent<MeshRenderer> ();

                col.material.SetColor
                    (
                    "_EmissionColor",
                    Color.Lerp (col.material.GetColor ("_EmissionColor"), ChargeEmission [1], (1 / flickLaunchBuildupMax) * Time.deltaTime)
                    );
            }

        }
        else
        {
            //Otherwise bring it back to the default position quickly.
            camScript.updateChargePosition(false);

            foreach ( GameObject ring in Rings )
            {
                MeshRenderer col = ring.GetComponent<MeshRenderer> ();

                col.material.SetColor
                    (
                    "_EmissionColor",
                    Color.Lerp (col.material.GetColor ("_EmissionColor"), ChargeEmission [0], (1 / flickLaunchBuildupMax) * Time.deltaTime)
                    //ChargeEmission [0]
                    );
            }
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
