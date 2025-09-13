using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 15f;
    public float jumpForce = 5f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    [Header("Camera Settings")]
    public Transform cameraTransform;
    public float cameraDistance = 5f;
    public float cameraHeight = 3f;
    public float mouseSensitivity = 100f;

    private Rigidbody rb;
    private bool isGrounded;

    private float yaw;   
    private float pitch; 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
      
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -20f, 60f); 

        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        
        Quaternion camRot = Quaternion.Euler(pitch, yaw, 0);
        Vector3 camDir = camRot * Vector3.forward;

      
        Vector3 camPos = transform.position - camDir * cameraDistance + Vector3.up * cameraHeight;
        cameraTransform.position = camPos;
        cameraTransform.rotation = camRot;

       
        Vector3 moveDir = (camRot * new Vector3(moveX, 0, moveZ)).normalized;
        moveDir.y = 0;

        rb.linearVelocity = new Vector3(moveDir.x * moveSpeed, rb.linearVelocity.y, moveDir.z * moveSpeed);

       
        if (moveDir.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(moveDir);
        }
    }
}
