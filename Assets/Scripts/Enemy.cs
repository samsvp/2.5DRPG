using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Character
{
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

    #region Battle

    public override void Die()
    {
        base.Die();
        Player.instance.Enemy = null;
        GameManager.instance.isInBattle = false;
        StartCoroutine(IDie());
    }

    private IEnumerator IDie()
    {
        yield return null;
        Destroy(gameObject);
    }

    #endregion

#pragma warning disable IDE0051 // Remove unused private members
    void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Player"))
        {
            Player.instance.Enemy = this;
        }
    }
#pragma warning restore IDE0051 // Remove unused private members

}
