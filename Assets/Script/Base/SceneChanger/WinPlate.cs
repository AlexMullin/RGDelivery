using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPlate : SceneChanger
{
    private void OnTriggerEnter (Collider other)
    {
        if ( other.CompareTag ("Player") )
        {
            ChangeScene ();
        }
    }
}
