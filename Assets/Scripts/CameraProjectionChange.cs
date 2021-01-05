using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraProjectionChange : MonoBehaviour
{
    private Camera mainCamera;
    public static bool isChanging = false;
    public static bool isCamera2D = false;
    public static readonly float speed = 5;

    [SerializeField]
    private PositionProj positionProj;
    // Used to align the player with the z axis
    [SerializeField]
    private float playerAlignZ;
    public int movementDistance = 5;

#pragma warning disable IDE0051 // Remove unused private members
    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.LeftShift) && !CameraFollow.followZ)
            StartCoroutine(ChangeProjection());
#endif
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(ChangeProjection());
            Vector3 pos = Player.instance.transform.position;
            int direction = (int)Mathf.Sign(col.transform.position.x - transform.position.x);
            Player.instance.transform.position = new Vector3(pos.x, pos.y, playerAlignZ);
            Player.instance.ControlledMovement(-direction, 0, movementDistance);
            Player.instance.lastZ = playerAlignZ;
        }
    }
#pragma warning restore IDE0051 // Remove unused private members

    private IEnumerator ChangeProjection()
    {
        if (isChanging) yield break;

        isChanging = true;
        Player.instance.canMove = false;
        if (positionProj != null) positionProj.TriggerChange(speed, !isCamera2D);
        else Debug.LogWarning("\"positionProj\" variable is null");

        float tol = 0.05f;
        float targetYPos = isCamera2D ? mainCamera.transform.position.y + 7 :
             mainCamera.transform.position.y - 7;
        Quaternion targetRot = isCamera2D ?
            new Quaternion(0.2588192f, 0, 0, 0.9659258f) : // euler: x=30, y=0, z=0
            new Quaternion(-0.0348995f, 0, 0, 0.9993908f); // euler: x=-4, y=0, z=0
        float targetEulerXRot = isCamera2D ? 30 : 356; // 356 = -4 degrees
        while (Mathf.Abs(mainCamera.transform.position.y - targetYPos) > tol * 2 ||
               Mathf.Abs(mainCamera.transform.eulerAngles.x - targetEulerXRot) > tol)
        {
            float yPos = Mathf.Lerp(mainCamera.transform.position.y, targetYPos, speed * Time.deltaTime);
            mainCamera.transform.position = new Vector3(
                mainCamera.transform.position.x, yPos, mainCamera.transform.position.z);
            mainCamera.transform.rotation = Quaternion.Lerp(
                mainCamera.transform.rotation, targetRot, speed * 2f * Time.deltaTime);
            yield return null;
        }

        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, targetYPos, mainCamera.transform.position.z);
        mainCamera.transform.rotation = targetRot;

        var pos = Player.instance.transform.position;
        Player.instance.transform.position = new Vector3(pos.x + 3, pos.y, pos.z);

        isChanging = false;
        isCamera2D = !isCamera2D;
        // Change initial camera Y position depending on the current perspective
        CameraFollow.initialY = isCamera2D ? 6 : 13;

        Player.instance.canMove = true;
    }

}
