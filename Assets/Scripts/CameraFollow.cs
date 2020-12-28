using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    private Player player;
    public static float initialX;
    public static float initialY;
    private static float initialZ;
    private float initialPlayerY;
    public static float targetZ;
    public static bool followZ = false;

    public static float InitialZ { get => initialZ; }

#pragma warning disable IDE0051
    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
        initialPlayerY = player.transform.position.y;
        initialX = transform.position.x;
        initialY = transform.position.y;
        initialZ = transform.position.z;
        targetZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 5 * Time.deltaTime;
        float x = Mathf.Lerp(transform.position.x, initialX + player.transform.position.x, speed);
        float y = player.CharacterController.isGrounded && !CameraProjectionChange.isChanging ?
            Mathf.Lerp(transform.position.y, initialY - initialPlayerY + player.transform.position.y, speed) :
            transform.position.y;
        float z = followZ ?
            Mathf.Lerp(transform.position.z, initialZ + player.transform.position.z, speed) :
            Mathf.Lerp(transform.position.z, targetZ, speed);
        transform.position = new Vector3(x, y, z);
    }
#pragma warning restore IDE0051 // Remove unused private members

}
