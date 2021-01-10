using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GrabbableObject : Interactable
{
    /*
     * Grab Variables
     */
    private bool canHold = true;
    private Transform playerItemSlot;
    private Transform parent;
    
    private Rigidbody rb;

#pragma warning disable IDE0051 // Remove unused private members
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();

        parent = transform.parent;
        playerItemSlot = Player.itemSlot;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (transform.parent == playerItemSlot)
        {
            if (Player.instance.facingDirection == "right")
                transform.position = new Vector3(playerItemSlot.position.x + 1.5f,
                    transform.position.y, transform.position.z);
            else
                transform.position = new Vector3(playerItemSlot.position.x - 1.5f,
                    transform.position.y, transform.position.z);
        }
    }
#pragma warning restore IDE0051 // Remove unused private members

    protected override void Interact()
    {
        if (canHold && isInRange && playerItemSlot.childCount == 0)
        {
            transform.parent = playerItemSlot;
            transform.localPosition = new Vector3(transform.localPosition.x, 0.25f,
                                                    transform.localPosition.z);
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            canHold = false;
        }
        else if (!canHold && transform.parent != null)
        {
            transform.parent = null;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            canHold = true;
            rb.useGravity = true;
        }
    }
}
