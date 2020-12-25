using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public bool isInBattle = false;

#pragma warning disable IDE0051 // Remove unused private members
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
#pragma warning restore IDE0051 // Remove unused private members

}
