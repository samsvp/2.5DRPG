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

    private Sprite sprite;
    
    private Rigidbody rb;

#pragma warning disable IDE0051 // Remove unused private members
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        sprite = GetComponent<SpriteRenderer>().sprite;

        playerItemSlot = Player.itemSlot;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.CompareTag("Ground")) canHold = true;
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
            canHold = false;
        }
        else if (!canHold)
        {
            transform.parent = null;
            rb.useGravity = true;
        }
    }
}
