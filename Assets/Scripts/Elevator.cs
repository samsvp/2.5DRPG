using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private float initialY;

    [SerializeField]
    private float height = 5;
    [SerializeField]
    private float speed = 5;

    private static float tol = 0.01f;

#pragma warning disable IDE0051
    // Start is called before the first frame update
    void Start()
    {
        initialY = transform.position.y;   
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        print("Elevator Debugging is on!");
        if (Input.GetKeyDown(KeyCode.Tab)) Move();
#endif
    }
#pragma warning restore IDE0051 // Remove unused private members

    public Coroutine Move()
    {
        return StartCoroutine(IMove());
    }

    private IEnumerator IMove()
    {
        while (true)
        {
            yield return IMove(initialY);
            yield return IMove(transform.position.y + height);
            yield return null;
        }
    }

    private IEnumerator IMove(float targetY)
    {
        while (Mathf.Abs(transform.position.y - targetY) > tol)
        {
            float y = Mathf.Lerp(transform.position.y, targetY, speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
    }

}
