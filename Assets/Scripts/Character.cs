using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Character : MonoBehaviour
{
    // Health
    [SerializeField]
    protected int HP = 10;

    // Movement variables
    [SerializeField]
    protected float speed;
    protected float x, z;

    // Sprites and Animation
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected Sprite sprite;
    protected string[] animationBools = new string[] { "WalkUp", "WalkDown", "WalkRight", "WalkLeft" };

    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    #region Movement
    protected virtual void Move(int _x, int _z)
    {
        x = _x * speed;
        z = _z * speed;

        WalkAnimation(_x, _z);
    }

    /// <summary>
    /// Activates the right walk animation for the player direction
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    protected void WalkAnimation(int x, int z)
    {
        SetAllAnimMoveFalse();

        if (x == 0 && z == 0) return;
        animator.SetBool("Moving", true);
        if (x != 0)
        {
            if (x == 1) animator.SetBool("WalkRight", true);
            if (x == -1) animator.SetBool("WalkLeft", true);
        }
        else if (z != 0)
        {
            if (z == 1) animator.SetBool("WalkUp", true);
            if (z == -1) animator.SetBool("WalkDown", true);
        }
    }

    protected void SetAllAnimMoveFalse()
    {
        animator.SetBool("Moving", false);
        foreach (var bl in animationBools) animator.SetBool(bl, false);
    }
    #endregion

    #region Battle

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            Die();
        }
    }

    public virtual void Die()
    {
        // Play death animation
    }

    #endregion

}
