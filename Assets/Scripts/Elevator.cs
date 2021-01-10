using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private float initialY;

    // How far along the Y axis the elevator must go
    [SerializeField]
    private float height = 5;
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float centerYOffset = 3;

    private Rigidbody rb;

    [HideInInspector]
    public bool isOn = false;
    private bool movingToCenter = false;
    private bool isCentered = false;
    
    private static float tol = 0.08f;

#pragma warning disable IDE0051
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the parent tag isn't "ElevatorBase" then all elevator functions
        // should be ignored and its variables reset
        if (transform.parent == null || !transform.parent.CompareTag("ElevatorBase"))
        {
            StopAllCoroutines();
            isCentered = false;
            isOn = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("ElevatorBase")) return;
        if (movingToCenter) return;

        // Freeze all constraints so gravity and collision doesn't affect the elevator
        rb.constraints = RigidbodyConstraints.FreezeAll;
        
        transform.parent = collision.transform;
        StartCoroutine(MoveToBase(collision.transform.position));
    }
#pragma warning restore IDE0051 // Remove unused private members

    /// <summary>
    /// Moves the elevator platform to the bases center.
    /// </summary>
    /// <param name="baseCenter"></param>
    /// <returns></returns>
    private IEnumerator MoveToBase(Vector3 baseCenter)
    {
        movingToCenter = true;

        Vector3 target = new Vector3(baseCenter.x, baseCenter.y + centerYOffset, baseCenter.z);
        initialY = target.y;
        while (Vector3.Distance(transform.position, target) > tol)
        {
            transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;
        
        movingToCenter = false;
        isCentered = true;
        Switch();
    }

    /// <summary>
    /// Switches the elevator "on" and "off" if it is centered.
    /// </summary>
    /// <returns></returns>
    public bool? Switch()
    {
        if (!isCentered) return null;

        StopAllCoroutines();

        // Returns the elevator to its initial position
        if (isOn) StartCoroutine(IStop());
        // Starts the coroutine to move the elevator up and down.
        else StartCoroutine(IMove());

        isOn = !isOn;
        return isOn;
    }

    private IEnumerator IStop()
    {
        yield return IMove(initialY);
    }

    private IEnumerator IMove()
    {
        yield return new WaitWhile(() => movingToCenter);
        if (!isCentered) yield break;

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
