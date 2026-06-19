using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform orientation;
    private CharacterController controller;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("Jump & Gravity")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 2f;

    private Vector3 velocity;
    private Vector3 moveDir;

    private float horizontalInput;
    private float verticalInput;
    private bool jumpInput;

    public bool isThirdPersonCamActive = true;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();
        ApplyGravity();
        ApplyFinalMovement();
    }

    private void HandleInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        jumpInput = Input.GetButtonDown("Jump");
    }

    private void HandleMovement()
    {
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
        moveDir.y = 0f;
    }

    private void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // keeps player grounded
        }

        if (jumpInput && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
    }

    private void ApplyFinalMovement()
    {
        Vector3 finalMove = moveDir.normalized * moveSpeed + velocity;
        controller.Move(finalMove * Time.deltaTime);
    }
}
