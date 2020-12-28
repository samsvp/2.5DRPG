using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shadow : MonoBehaviour
{
    public float offset = 0.1f;
    public LayerMask _layerMask;
    private float lastY;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        lastY = transform.position.y;
    }

    void Update()
    {
        Ray ray = new Ray(transform.parent.position, -Vector3.up);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 1000f, _layerMask)) lastY = hitInfo.point.y + offset;

        transform.position = new Vector3(transform.position.x, lastY, transform.position.z);
        sr.enabled = !CameraProjectionChange.isCamera2D;
    }

}
