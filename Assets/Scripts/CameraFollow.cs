using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    private Player player;
    private static float initialZ;
    public static float targetZ;
    public static bool followZ = false;

    public static float InitialZ { get => initialZ; }

#pragma warning disable IDE0051
    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
        initialZ = transform.position.z;
        targetZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        float x = player.transform.position.x;
        float zSpeed = 5 * Time.deltaTime;
        float z = followZ ?
            Mathf.Lerp(transform.position.z, initialZ + player.transform.position.z, zSpeed) :
            Mathf.Lerp(transform.position.z, targetZ, zSpeed);
        transform.position = new Vector3(x, transform.position.y, z);
    }
#pragma warning restore IDE0051 // Remove unused private members

}
