using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Flickable
{
    // Start is called before the first frame update
    protected override void Start ()
    {
        base.Start ();
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
