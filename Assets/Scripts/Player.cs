using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{

    public static Player instance;

    // Battle
    private Enemy enemy;
    public Enemy Enemy 
    { 
        get => enemy; 
        set 
        {
            if (GameManager.instance.isInBattle = value != null)
            {
                SetAllAnimMoveFalse();
            }
            enemy = value;
            // debug
            // if (value != null) enemy.TakeDamage(1000);
        } 
    }
    
    // Movement
    private CharacterController characterController;

    private float verticalVelocity = 0;
    private float gravity = 14.8f;
    private float jumpForce = 8;

    // Item
    public static Transform itemSlot;


#pragma warning disable IDE0051 // Remove unused private members
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        itemSlot = transform.GetChild(0);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        characterController = GetComponent<CharacterController>();
    }

    protected override void Update()
    {
        base.Update();
        
        if (CameraProjectionChange.isChanging) return;

        if (GameManager.instance.isInBattle)
        {
            print("Battle");
        }
        else
        {
            // Jump();
            Move();
        }
        
        ///DEBUG
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.LeftShift)) { }


        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
#endif
        ///DEBUG
    }
#pragma warning restore IDE0051 // Remove unused private members

    #region Movement

    private void Move()
    {
        // Only allow movement along the z axis if in 3D mode;
        int _x = (int)Input.GetAxisRaw("Horizontal");
        int _z = CameraProjectionChange.isCamera2D ? 0 : (int)Input.GetAxisRaw("Vertical");

        x = _x * speed;
        z = _z * speed; 

        WalkAnimation(_x, _z);

        characterController.Move(new Vector3(x, verticalVelocity, z) * Time.deltaTime);
    }

    private void Jump()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKey(KeyCode.Space)) verticalVelocity = jumpForce;
        }
        else verticalVelocity -= gravity * Time.deltaTime;
    }

    #endregion
    
    #region Battle

    #endregion
}
