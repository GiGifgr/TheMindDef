using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomGravity : MonoBehaviour
{
    [Header("Float configuration")]
    [SerializeField] private float floatForce = 5f;
    [SerializeField] private float maxUpwardSpeed = 10f;

    [Header("Tracking")]
    [SerializeField] private string playerTag = "Player";

    private Rigidbody rb;
    private bool isFloating = false;

    private void Start()
    {
        InitializeComponent();
    }

    private void FixedUpdate()
    {
        ApplyFloatPhysics();
    }

    private void OnCollisionEnter(Collision collision)
    {
        CheckPlayerCollision(collision);
    }

    private void InitializeComponent()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void CheckPlayerCollision(Collision collision)
    {
        if (!isFloating && collision.gameObject.CompareTag(playerTag))
        {
            StartFloating();
        }
    }

    private void StartFloating()
    {
        isFloating = true;
        rb.useGravity = false;
    }

    private void ApplyFloatPhysics()
    {
        if (!isFloating || rb == null)
        {
            return;
        }

        if (rb.linearVelocity.y < maxUpwardSpeed)
        {
            rb.AddForce(Vector3.up * floatForce, ForceMode.Acceleration);
        }
    }
}