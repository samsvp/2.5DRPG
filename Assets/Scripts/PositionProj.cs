using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionProj : MonoBehaviour
{
    private float lastZ;
#pragma warning disable IDE0051 // Remove unused private members
    // Start is called before the first frame update
    void Start()
    {
        CameraProjectionChange.AddCallback(ChangeZPosition);
        lastZ = transform.position.z;
    }

    void OnDestroy()
    {
        CameraProjectionChange.RemoveCallback(ChangeZPosition);
    }

#pragma warning restore IDE0051 // Remove unused private members

    /// <summary>
    /// Changes z position when the camera projection changes
    /// </summary>
    /// <param name="speed"></param>
    private IEnumerator IChangeZPosition(float speed, float targetZ)
    {
        while (Mathf.Abs(transform.position.z - targetZ) > 0.05f && CameraProjectionChange.isChanging)
        {
            Vector3 pos = transform.position;
            Vector3 targetPos = new Vector3(pos.x, pos.y, targetZ);
            transform.position = Vector3.Lerp(pos, targetPos, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, targetZ);
    }


    private void ChangeZPosition(float speed, bool changeTo2D)
    {
        if (Mathf.Abs(transform.position.z - Player.instance.transform.position.z) > 5.5f) 
            return;

        float targetZ = lastZ;
        if (changeTo2D)
        {
            targetZ = CameraFollow.targetZ - CameraFollow.InitialZ;
            lastZ = transform.position.z;
        }
        StartCoroutine(IChangeZPosition(speed, targetZ));
    }
}
