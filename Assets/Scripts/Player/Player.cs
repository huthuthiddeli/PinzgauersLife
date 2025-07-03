using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;

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
    public float pissLevel = 0f;
    [Range(0, 100)]
    public float shitLevel = 0f;
    [Range(0, 100)]
    public float dirtiness = 0f;

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
    public HealthBar healthBar;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Hide and lock cursor
        controller = gameObject.AddComponent<CharacterController>();

        healthBar.SetMaxHealth(hp);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleMouseLook();

        // Interacting with environment
        HandleInput();
        HandleHealth();
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
    }

    void Interact()
    {
        LayerMask layerMask = LayerMask.GetMask("Character", "Beer");
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))

        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }


        this.alcoholLevel += 10;
    }

    void HandleHealth()
    {
        this.healthBar.SetHealth(this.hp);
    }

}
