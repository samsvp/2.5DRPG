using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProjectionChange : MonoBehaviour
{
    public static bool isChanging = false;
    public static bool isCamera2D = false;
    public static readonly float speed = 5;

    private static List<Action<float, bool>> callbacks = new List<Action<float, bool>>();

#pragma warning disable IDE0051 // Remove unused private members
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !CameraFollow.followZ)
            StartCoroutine(ChangeProjection());
    }
#pragma warning restore IDE0051 // Remove unused private members

    private IEnumerator ChangeProjection()
    {
        isChanging = true;
        foreach (var callback in callbacks) callback(speed, !isCamera2D);

        float tol = 0.05f;
        float targetYPos = isCamera2D ? 8 : 0;
        Quaternion targetRot = isCamera2D ? 
            new Quaternion(0.2588192f, 0, 0, 0.9659258f) : // euler: x=30, y=0, z=0
            new Quaternion(-0.0348995f, 0, 0, 0.9993908f); // euler: x=-4, y=0, z=0
        float targetEulerXRot = isCamera2D ? 30 : 356; // 356 = -4 degrees

        while (Mathf.Abs(transform.position.y - targetYPos) > tol * 2 ||
               Mathf.Abs(transform.eulerAngles.x - targetEulerXRot) > tol)
        {
            float yPos = Mathf.Lerp(transform.position.y, targetYPos,  speed* Time.deltaTime);
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, speed * 2f * Time.deltaTime);
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, targetYPos, transform.position.z);
        transform.rotation = targetRot;

        isChanging = false;
        isCamera2D = !isCamera2D;
    }

    public static void AddCallback(Action<float, bool> callback)
    {
        callbacks.Add(callback);
    }

    public static void RemoveCallback(Action<float, bool> callback)
    {
        callbacks.Remove(callback);
    }
}
