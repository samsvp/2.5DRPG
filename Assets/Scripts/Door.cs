using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Door : Interactable
{
    public bool isLocked = false;
    private SpriteRenderer sr;
    private SpriteRenderer[] wallsRenderers;
    private BoxCollider bcSolid;
    private BoxCollider bcTriggerIn;
    private BoxCollider bcTriggerOut;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        int childCount = transform.childCount;
        wallsRenderers = new SpriteRenderer[childCount];
        for (int i = 0; i < childCount; i++)
        {
            wallsRenderers[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }

        sr = GetComponent<SpriteRenderer>();

        var bcs = GetComponents<BoxCollider>();
        bcSolid = bcs[0];
        bcTriggerIn = bcs[1];
        bcTriggerOut = bcs[2];
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (bcTriggerOut.enabled && !isInRange)
        {
            TogglePlayerInside(false);
        }
    }

    protected override void Interact()
    {
        TogglePlayerInside(true);
    }

    protected void TogglePlayerInside(bool isInside)
    {
        Color color = isInside ? TransparencyCollider.transparency : Color.white;

        sr.enabled = !isInside;
        bcSolid.enabled = !isInside;
        bcTriggerIn.enabled = !isInside;
        bcTriggerOut.enabled = isInside;

        StopAllCoroutines();
        foreach (var wallRenderer in wallsRenderers)
        {
            TransparencyCollider.ChangeColor(wallRenderer, color, this);
        }
    }
}
