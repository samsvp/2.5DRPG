using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    private Player player;
    private static float initialX;
    private static float initialZ;
    public static float targetZ;
    public static bool followZ = false;

    public static float InitialZ { get => initialZ; }

#pragma warning disable IDE0051
    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
        initialX = transform.position.x;
        initialZ = transform.position.z;
        targetZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 5 * Time.deltaTime;
        float x = Mathf.Lerp(transform.position.x, initialX + player.transform.position.x, speed);
        float z = followZ ?
            Mathf.Lerp(transform.position.z, initialZ + player.transform.position.z, speed) :
            Mathf.Lerp(transform.position.z, targetZ, speed);
        transform.position = new Vector3(x, transform.position.y, z);
    }
#pragma warning restore IDE0051 // Remove unused private members

}
