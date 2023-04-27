using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Flickable : MonoBehaviour
{

    [SerializeField]
    private float
        flickThreshold = 0.18f, //How fast do you move your touch to flick?
        flickLaunchForce = 10, //What is the maximum strength of the lauch?
        flickLaunchBuildup = 0, //Current buildup of the flickPower
        flickLaunchBuildupMax = 4, //How long does it take to reach maximum strengh?
        flickLaunchTimeLimit = 5,//How long until your launch times out?
        flickLuanchDisperseTime; //How long have you been at max power?


    //Strech goal: 2nd Touch deactivates movement, but a 3rd touch can reactivate movement

    bool touchSingle = false; //Is there only one touch on the screen?
    bool touchActive = false; //Is this object in particular clicked on?

    Vector2 touchStartPos = Vector2.zero;
    Vector2 touchCurrentPos = Vector2.zero;

    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //A touch has happened
        if (Input.touchCount == 1)
        {
            if(!touchSingle && Input.touches[0].phase == TouchPhase.Began) 
            {
                touchSingle = true;

                touchStartPos = Input.touches[0].position;
                touchCurrentPos = Input.touches[0].position;
            }



            //While the object is being touched
            if (touchSingle)
            {
                if (touchActive)
                {
                    //Debug.Log (name + ": Touch Active!");

                    touchCurrentPos = Input.touches[0].position;

                    if ((touchCurrentPos - touchStartPos).magnitude * Time.deltaTime >= flickThreshold)
                    {
                        
                        DeactivateTouch();
                        moveByFlick ();
                    }

                    touchStartPos = touchCurrentPos;
                }

                //While touched: Slow down to a halt:
                rb.velocity = Vector3.MoveTowards(rb.velocity, Vector3.zero, flickLaunchForce * Time.deltaTime);
                rb.angularVelocity = Vector3.MoveTowards (rb.angularVelocity, Vector3.zero, flickLaunchForce * Time.deltaTime);


                //Are you still building up speed
                if (flickLaunchBuildup < flickLaunchBuildupMax)
                {
                    flickLaunchBuildup += Time.deltaTime;
                }

                //If you have, do you still have time in your timer?
                else if(flickLuanchDisperseTime < flickLaunchTimeLimit){
                    flickLuanchDisperseTime += Time.deltaTime;
                }

                //If you have hit your timeout:
                else
                {
                    Debug.Log ("Object timed out. deactivating");
                    DeactivateTouch ();
                }
            }

        }

        //Touch has been released
        else if (touchSingle)
        {
            touchSingle = false;


            Debug.Log ("Touch Released");
        }
    }

    //Player clicks on the object with their first touch, activate touch controls.
    private void OnMouseDown ()
    {
        if(Input.touchCount == 1)
        {
            touchActive = true;
            flickLaunchBuildup = 0;

        }
    }

    private void DeactivateTouch ()
    {
        touchSingle = false;
        touchActive = false;


    }

    private void moveByFlick ()
    {
        Vector2 dir = (touchCurrentPos - touchStartPos);

        Vector3 direction3 = new Vector3(dir.x, 0, dir.y);

        direction3 = Quaternion.AngleAxis (Camera.main.transform.rotation.eulerAngles.y, Vector3.up) * direction3;



        rb.angularVelocity = Vector3.zero;
        rb.velocity = direction3.normalized * flickLaunchForce * (flickLaunchBuildup / flickLaunchBuildupMax);
    }

}
