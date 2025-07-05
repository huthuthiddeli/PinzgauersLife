using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{

    public Player player;

    InputSystem_Actions input;
    private Vector2 moveInput;
    private Vector2 lookInput;

    void Awake()
    {
        input = new InputSystem_Actions();

        if(input is null)
        {
            Debug.LogError("Input System Actions not initialized.");
            return;
        }

        input.Enable();

        input.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        input.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        input.Player.Look.canceled += ctx => lookInput = Vector2.zero;

        input.Player.Jump.performed += ctx => player.Jump();
        input.Player.Interact.performed += ctx => player.Interact();
        
    
    }

    void OnEnable() => input.Enable();
    void OnDisable() => input.Disable();

    void Update()
    {
        player.HandleMovement();
        player.HandleMouseLook();
    }
}
