using UnityEngine;

public class Player : MonoBehaviour
{
    // Player-stats
    [Header("Player Stats")]
    [Range(0, 100)]
    public float hp = 100f;
    [Range(0, 100)]
    public float hungriness = 100f;
    [Range(0, 100)]
    public float thirstiness = 100f;
    [Range(0, 100)]
    public float stamina = 100f;
    [Range(0, 100)]
    public float alcoholLevel = 0f;
    [Range(0, 100)]
    public float pissLevel = 100f;
    [Range(0, 100)]
    public float dirtiness = 0f;
    public float money = 0f;
    public float moneyBank = 0f;

    [Header("Player modifiers")]
    public float hungerModifier = 0.1f;
    public float thirstModifier = 0.1f;
    public float alcoholModifier = 0.1f;
    public float pissModifier = 0.1f;
    public float dirtinessModifier = 0.001f;
    public float starvingModifier = 0.5f;
    public float interactRange = 3f;


    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float sprintSpeed = 20f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public bool isSlowed = false;
    public float slowModifier = 0f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 500f;
    public Transform playerCamera;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.01f;
    public LayerMask groundMask;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float xRotation = 0f;

    [Header("Referencess")]
    public UIElement healthBar;
    public UIElement waterBar;
    public UIElement hungerBar;
    public UIElement pissBar;
    public UIElement moneyLabel;
    public Camera miniMapCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Hide and lock cursor
        controller = gameObject.AddComponent<CharacterController>();

        InitPlayerVariables();
    }

    void InitPlayerVariables()
    {
        healthBar.SetMaxValue(hp);
        waterBar.SetMaxValue(thirstiness);
        hungerBar.SetMaxValue(hungriness);
        pissBar.SetMaxValue(pissLevel);
        moneyLabel.SetText("Money: " + money.ToString("F2"));

        pissLevel = 0.0f;
        thirstiness = 0.0f;
        hungriness = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerStats();


        HandleMovement();
        HandleMouseLook();

        // Interacting with environment
        HandleInput();
        HandlePlayerStats();
    }

    void HandleMovement()
    {
        // Ground check
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Move input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * sprintSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(move * moveSpeed * Time.deltaTime);
        }

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Vertical rotation (camera only)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent flipping

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal rotation (player body)
        transform.Rotate(Vector3.up * mouseX);
    }


    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            this.hp -= 20;
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            this.hungriness += 10;
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            this.thirstiness += 10;
            this.money += 10;
        }
    }

    void Interact()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactRange))
        {
            IConsumable consumable = hit.collider.GetComponent<IConsumable>();
            if (consumable != null)
            {
                consumable.Consume(gameObject);
            }
        }
    }

    void HandlePlayerStats()
    {
        this.waterBar.SetValue(this.thirstiness);
        this.hungerBar.SetValue(this.hungriness);
        this.healthBar.SetValue(this.hp);
        this.moneyLabel.SetText("Money: " + this.money.ToString("F2") + "$");
    }

    void UpdatePlayerStats()
    {
        float dt = Time.deltaTime;

        if (hp <= 0)
        {
            Debug.Log("Player is dead!");
            // Handle player death (e.g., respawn, game over)
            return;
        }

        if (hungriness <= 0 || thirstiness <= 0)
        {
            Debug.Log("Player is starving!");
            this.hp -= starvingModifier * dt;
        }

        if( pissLevel >= 100f)
        {
            Debug.Log("Your player pissed yourself!");
            // PissInPants();
            this.pissLevel = 0f;
        }

        hungriness += hungerModifier * dt;
        thirstiness += thirstModifier * dt;
        alcoholLevel -= alcoholModifier * dt;
        dirtiness += dirtinessModifier * dt;
    }

    void MiniMapTrackPlayr()
    {
        this.miniMapCamera.transform.position = new Vector3(this.transform.position.x, this.miniMapCamera.transform.position.y + 30, this.transform.position.z);
    }
}
