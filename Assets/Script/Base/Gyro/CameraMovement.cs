using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : Gyro
{
    GameObject pivot;
    GameObject player;



    // Start is called before the first frame update
    protected override void Start ()
    {
        base.Start ();

        player = GameObject.Find (("Player"));

        pivot = Instantiate (new GameObject ("CameraAnchor"), player.transform.position, Quaternion.identity);

        transform.parent = pivot.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
