using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class carController2 : MonoBehaviour
{
    CharacterController controller;
    public float maxSpeed = 12f;
    public float turnSpeed = 220f;
    public float acceleration = 40f;
    public float gravity = 35f;
    public float downwardPull = 5f;
    public float airControl = 0.3f;
    public float launchBoost = 8f;
    public float coyoteTime = 0.08f;
    float currentSpeed;
    float verticalVelocity;
    float timeSinceGrounded;
    Vector3 airVelocity;  
    bool wasGroundedLastFrame ;
    [SerializeField] InputAction accel;
    [SerializeField] InputAction turn;
    void OnEnable()
    {
        controller = GetComponent<CharacterController>();
        accel.Enable();
        turn.Enable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if(controller != null)
        {
            controller.enabled = true;
        }

        WheelCollider[] wheels = GetComponentsInChildren<WheelCollider>(true);
        foreach (WheelCollider wheel in wheels)
        {
            wheel.enabled = false;
        }

        BoxCollider[] box = GetComponentsInChildren<BoxCollider>(true);
        foreach(BoxCollider b in box)
        {
            b.enabled = false;
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float throttle = accel.ReadValue<float>();
        float steering = turn.ReadValue<float>();
        if (controller.isGrounded)
        {
            timeSinceGrounded = 0f;
        }
        else
        {
            timeSinceGrounded += Time.deltaTime;
        }
        bool justLeftGround = wasGroundedLastFrame&&!controller.isGrounded&&timeSinceGrounded>coyoteTime&&currentSpeed>5f;
        currentSpeed = Mathf.MoveTowards(currentSpeed, throttle*maxSpeed,acceleration *Time.deltaTime);
        if(currentSpeed > 0.1f)
        {
            controller.transform.Rotate(0,steering*turnSpeed*Time.deltaTime,0);
        }
        if (controller.isGrounded)
        {
            if (!wasGroundedLastFrame)
            {
                verticalVelocity = -downwardPull;
            }
            airVelocity = transform.forward * currentSpeed;
        }
        else
        {
            if (justLeftGround)
            {
                verticalVelocity = launchBoost;
            }
            airVelocity = Vector3.Lerp(airVelocity,transform.forward * currentSpeed,airControl*Time.deltaTime);
            verticalVelocity -= gravity * Time.deltaTime;
        }
        wasGroundedLastFrame = controller.isGrounded;
        
        Vector3 move = airVelocity + Vector3.up * verticalVelocity;
        controller.Move(move * Time.deltaTime);
    }
}

