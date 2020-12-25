using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shadow : MonoBehaviour
{
    private float initialY;
    // Start is called before the first frame update
    void Start()
    {
        initialY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(transform.position.x, initialY, transform.position.z);
        transform.position = pos;
    }
}
