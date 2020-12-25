using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Interactable : MonoBehaviour
{
#pragma warning disable IDE0051
    protected bool isInRange = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E)) Interact();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Player")) isInRange = true;
    }

    void OnTriggerExit(Collider col)
    {
        if (col.transform.CompareTag("Player")) isInRange = false;
    }
#pragma warning restore IDE0051 // Remove unused private members

    protected abstract void Interact();
}
