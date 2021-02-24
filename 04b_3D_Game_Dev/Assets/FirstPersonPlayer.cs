using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonPlayer : MonoBehaviour {
    [Header("General")]
    [SerializeField] private float movementSpeed = 15f;

    [Header("Falling")]
    [SerializeField] private float gravityFactor = 1f;
    [SerializeField] private Transform groundPosition;
    [SerializeField] private LayerMask groundLayer;

    [Header("Jumping")]
    [SerializeField] private float jumpSpeed = 7f;
    [SerializeField] private bool canAirControl = true;

    [Header("Looking")]
    [SerializeField] private Transform camera;
    [SerializeField] private float mouseSensitivity = 300f;

    private CharacterController controller;

    private float verticalRotation = 0f;
    public float verticalSpeed = 0f;
    public bool isGrounded = false;

    private void Awake() {
        controller = GetComponent<CharacterController>();
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        // are we on the ground?
        RaycastHit collision;
        if (Physics.Raycast(groundPosition.position, Vector3.down, out collision, 0.2f, groundLayer)) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }

        // update vertical speed
        if (!isGrounded) {
            verticalSpeed += gravityFactor * -9.81f * Time.deltaTime;
        } else {
            verticalSpeed = 0f;
        }

        // adjust the rotations based on the mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        camera.localEulerAngles = new Vector3(verticalRotation, 0f, 0f);

        Vector3 x = Vector3.zero;
        Vector3 y = Vector3.zero;
        Vector3 z = Vector3.zero;

        // handle jumping
        if (isGrounded && Input.GetButtonDown("Jump")) {
            verticalSpeed = jumpSpeed;
            isGrounded = false;
            y = transform.up * verticalSpeed;
        } else if (!isGrounded) {
            y = transform.up * verticalSpeed;
        }

        // handle walking/running
        if (isGrounded || canAirControl) {
            x = transform.right * Input.GetAxis("Horizontal") * movementSpeed;
            z = transform.forward * Input.GetAxis("Vertical") * movementSpeed;
        }

        // handle motion
        Vector3 movement = (x + y + z) * Time.deltaTime;
        controller.Move(movement);
    }
}
