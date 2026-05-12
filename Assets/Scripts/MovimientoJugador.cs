using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    [Header("Movement configuration")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundDistance = 1.2f;

    [Header("Camera configuration")]
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerCamera;

    private Rigidbody rb;
    private float moveX;
    private float moveZ;
    private float rotationX = 0f;

    private void Start()
    {
        InitializeComponent();
    }

    private void Update()
    {
        HandleInput();
        HandleCamera();
        HandleJump();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void InitializeComponent()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void HandleInput()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");
    }

    private void HandleCamera()
    {
        if (playerCamera == null) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundDistance);
    }

    private void MovePlayer()
    {
        Vector3 movement = transform.forward * moveZ + transform.right * moveX;
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}