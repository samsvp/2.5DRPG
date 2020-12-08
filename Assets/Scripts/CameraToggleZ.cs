using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToggleZ : MonoBehaviour
{    
    public bool sFollowZ = false;
    public float targetZ;

#pragma warning disable IDE0051
    
    void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Player"))
        {
            CameraFollow.followZ = sFollowZ;
            if (!sFollowZ) CameraFollow.targetZ = targetZ + CameraFollow.InitialZ;
        }
    }

#pragma warning restore IDE0051 // Remove unused private members

}
