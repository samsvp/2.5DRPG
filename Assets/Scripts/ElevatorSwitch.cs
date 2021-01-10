using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSwitch : Interactable
{

    [SerializeField]
    private Elevator elevator;

#pragma warning disable IDE0051
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
#pragma warning restore IDE0051

    protected override void Interact()
    {
        elevator.Switch();
    }

}
