using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : Gyro
{
    GameObject pivot;
    GameObject player;

    Vector3 chargePositionMin;
    Vector3 chargePositionMax;

    Quaternion chargeRotationMin;
    Quaternion chargeRotationMax;

    public float followStrength = 0.01f; //max Speed

    public float followChargeSpeed = 0.5f;
    public float followReturnSpeed = 0.1f; //How quickly does the camera return to position after being launched.


    const int ROTATION_AMOUNT = 45;
    Quaternion targetRotation;

    bool touchDouble = false;
    Vector2 touchStartPos;
    Vector2 touchCurrentPos
    {
        get {
            if ( Input.touchCount == 2 )
            {
                return (Input.touches [0].position + Input.touches [1].position) / 2;
            }
            else { return Vector2.zero; }
        }
    }

    // Start is called before the first frame update
    protected override void Start ()
    {
        base.Start ();

        player = GameObject.Find (("Player"));

        pivot = Instantiate (new GameObject ("CameraAnchor"), player.transform.position, Quaternion.identity);

        GameObject camZoomed = transform.Find("SubCamera").gameObject;

        camZoomed.transform.parent = pivot.transform;

        transform.parent = pivot.transform;

        chargePositionMin = transform.localPosition;
        chargeRotationMin = transform.localRotation;

        chargePositionMax = camZoomed.transform.localPosition;
        chargeRotationMax = camZoomed.transform.localRotation;

        targetRotation = pivot.transform.rotation;

    }


    //Camera movement is done by flicking with two fingers.
    //Camera is flicked in increments of 45
    private void Update ()
    {
        if ( Input.touchCount == 2 )
        {
            //Did you put your second touch Down
            if ( !touchDouble && Input.touches [1].phase == TouchPhase.Began )
            {
                touchDouble = true;

                touchStartPos = touchCurrentPos;

                Debug.Log (touchDouble);
            }

            //While holding two fingers on the screen
            if ( touchDouble )
            {
                //Did you flick hard enough?
                Vector2 touchDelta = touchStartPos - touchCurrentPos;
                if (touchDelta.magnitude * Time.deltaTime >= GameSettings.flickThreshold)
                {
                    //Change the target rotation of pivot
                    targetRotation = pivot.transform.rotation * Quaternion.Euler (0, (touchDelta.x > 0) ? ROTATION_AMOUNT : -ROTATION_AMOUNT, 0);

                    touchDouble = false;
                }
            }
        }

        else
        {
            touchDouble = false;
        }

        //Lerp the rotation of the pivot to the desired location
        pivot.transform.rotation = Quaternion.Lerp (pivot.transform.rotation, targetRotation, followChargeSpeed);
    }

    private void FixedUpdate ()
    {
        pivot.transform.position = Vector3.Lerp (pivot.transform.position, player.transform.position, followStrength);

    }

    public void updateChargePosition (bool isCharging)
    {
        if ( isCharging )
        {
            transform.localPosition = Vector3.Lerp (transform.localPosition, chargePositionMax, followChargeSpeed);
            transform.localRotation = Quaternion.Lerp (transform.localRotation, chargeRotationMax, followChargeSpeed);

        }
        else
        {
            transform.localPosition = Vector3.Lerp (transform.localPosition, chargePositionMin, followReturnSpeed);
            transform.localRotation = Quaternion.Lerp (transform.localRotation, chargeRotationMin, followReturnSpeed);
        }

    }
}
