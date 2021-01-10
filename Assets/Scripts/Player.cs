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
    public CharacterController CharacterController { get => characterController; }

    private float verticalVelocity = 0;
    [SerializeField]
    private float gravity = 11.8f;
    [SerializeField]
    private float jumpForce = 18;
    private bool isJumping = false;
    public bool canMove = true;
    public float lastZ = 0;

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
        
        if (!canMove) return;

        if (GameManager.instance.isInBattle)
        {
            print("Battle");
        }
        else
        {
            Jump();
            GetMoventInput();
            if (CameraProjectionChange.isCamera2D)
                transform.position = new Vector3(transform.position.x, transform.position.y, lastZ);
        }
        
        ///DEBUG
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
#endif
        ///DEBUG
    }
#pragma warning restore IDE0051 // Remove unused private members

    #region Movement

    protected override void Move(int _x, int _z)
    {
        base.Move(_x, _z);
        characterController.Move(new Vector3(x, verticalVelocity, z) * Time.deltaTime);
    }

    private void GetMoventInput()
    {
        // Only allow movement along the z axis if in 3D mode;
        int _x = (int)Input.GetAxisRaw("Horizontal");
        int _z = CameraProjectionChange.isCamera2D ? 0 : (int)Input.GetAxisRaw("Vertical");
        Move(_x, _z);
    }

    /// <summary>
    /// Function to control the player movement from another script
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public void ControlledMovement(int x, int z, float distance)
    {
        StartCoroutine(IControlledMovement(x, z, distance));
    }

    public IEnumerator IControlledMovement(int x, int z, float distance)
    {
        canMove = false;
        float travelledDistance = 0f;
        int _x = x != 0 ? (int)Mathf.Sign(x) : x;
        int _z = z != 0 ? (int)Mathf.Sign(z) : z;
        while (travelledDistance < distance)
        {
            Vector3 lastPos = transform.position;
            Move(_x, _z);
            travelledDistance += Vector3.Distance(lastPos, transform.position);
            yield return null;
        }
        canMove = true;
    }

    private void Jump()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKey(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
                isJumping = true;
            }
            else if (isJumping)
            {
                isJumping = false;
            }
        }
        else
        {
            float y = Mathf.Abs(verticalVelocity) > 1f ? 
                Mathf.Pow(Mathf.Abs(verticalVelocity), 0.5f) : gravity;
            verticalVelocity -= gravity * y * Time.deltaTime;
            if (!isJumping)
            {
                // Snap the player to the ground
                RaycastHit hitInfo = new RaycastHit();
                if (Physics.Raycast(new Ray(transform.position, Vector3.down), out hitInfo, float.PositiveInfinity))
                    if (Mathf.Abs(hitInfo.point.y - transform.position.y) < 3)
                        characterController.Move(hitInfo.point - transform.position);
                    else isJumping = true;
            }
        }
    }

    #endregion
    
    #region Battle

    #endregion
}
