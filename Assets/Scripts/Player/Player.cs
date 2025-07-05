using UnityEngine;

public class Player : MonoBehaviour
{
    // Player-stats
    [Header("Player Stats")]
    [SerializeField]
    public string playerName = "Peter";
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
    public float moneyBank = 69f;

    [Header("Player modifiers")]
    public float hungerModifier = 0.1f;
    public float thirstModifier = 0.1f;
    public float alcoholModifier = 0.1f;
    public float pissModifier = 0.1f;
    public float dirtinessModifier = 0.001f;
    public float starvingModifier = 0.5f;
    public float interactRange = 3f;
    public float maxShakeIntensity = 0.5f;   // Max shake distance
    public float shakeFrequency = 20f;       // Shake speed multiplier


    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float sprintSpeed = 20f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public bool isSlowed = false;
    public float slowModifier = 0f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 500f;

    private Vector3 velocity;
    private Vector3 initialPosition;
    private bool isGrounded;
    private float xRotation = 0f;

    [Header("Referencess")]
    public UIElement healthBar;
    public UIElement waterBar;
    public UIElement hungerBar;
    public UIElement pissBar;
    public UIElement moneyLabel;
    public Camera playerCamera;
    

    private CharacterController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Hide and lock cursor
        controller = gameObject.AddComponent<CharacterController>();
        initialPosition = transform.position;

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
        // Interacting with environment
        HandleInput();
        HandlePlayerStats();
    }

    public void Jump()
    {

        if (controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void HandleMovement()
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

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Vertical rotation (camera only)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent flipping

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal rotation (player body)
        transform.Rotate(Vector3.up * mouseX);
    }


    void HandleInput()
    {
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.alcoholLevel = 100;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            playerCamera.enabled = !playerCamera.enabled;
        }
    }

    public void Interact()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactRange))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            
            if (interactable != null)
            {
                interactable.Interact(gameObject);
            }
        }
    }

    void HandlePlayerStats()
    {
        this.waterBar.SetValue(this.thirstiness);
        this.hungerBar.SetValue(this.hungriness);
        this.healthBar.SetValue(this.hp);
        this.moneyLabel.SetText("Money: " + this.money.ToString("F2") + "€");
    }

    void UpdatePlayerStats()
    {
        float dt = Time.deltaTime;
        IntoxicateScreen();

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


    //TODO: INTOXICATION EFFECT TO CAMERA AND PLAYER
    public void IntoxicateScreen()
    {
        float intensity = Mathf.Clamp01(this.alcoholLevel / 100f);
        if (intensity > 0f)
        {
            float shakeAmount = intensity * maxShakeIntensity;

            // Real shake = random rapid jitter, not smooth
            Vector3 shakeOffset = new Vector3(
                (Random.value - 0.5f) * 2f,
                (Random.value - 0.5f) * 2f,
                0f
            ) * shakeAmount;

            transform.localPosition = initialPosition + shakeOffset;
        }
        else
        {
            transform.localPosition = initialPosition;
        }


    }
}
