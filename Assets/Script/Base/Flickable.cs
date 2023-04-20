using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flickable : MonoBehaviour
{

    [SerializeField]
    private float
        flickThreshold = 0,
        flickLaunchForce = 10,
        flickLaunchBuildTime = 4,
        flickLaunchTimeout = 5;


    //Strech goal: 2nd Touch deactivates movement, but a 3rd touch can reactivate movement

    bool touchSingle = false; //Is there only one touch on the screen?
    bool touchActive = false; //Is this object in particular clicked on?

    Vector2 touchStartPos = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
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
            }



            //While the object is being touched
            if (touchSingle)
            {
                if (touchActive)
                {
                    //Debug.Log (name + ": Touch Active!");
                }
                //If the object is active, build up speed.

                    //On flick, send the object in the direction of the flick and deactivate

                    //if you wait too long or let go, deactivate the object
            }

        }

        //Touch has been released
        else if (touchSingle)
        {
            touchSingle = false;


            Debug.Log ("Touch Released");
        }
    }

    //Player clicks on the object
    private void OnMouseDown ()
    {
        if(Input.touchCount == 1)
        {
            Debug.Log ("I am touched!");
            touchActive = true;
        }
    }

    private void DeactivateTouch ()
    {
        Debug.Log (name + ": Touch deactivated");
        touchSingle = false;
        touchActive = false;


    }
}
