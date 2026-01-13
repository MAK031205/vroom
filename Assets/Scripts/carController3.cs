using UnityEngine;
using UnityEngine.InputSystem;

public class carController3 : MonoBehaviour
{
    Rigidbody rb;
    float originalMass;
    float originalDrag;
    float originalAngularDrag;
    bool originalUseGravity;
    RigidbodyInterpolation originalInterpolation;
    CollisionDetectionMode originalCollisionDetectionMode;
    RigidbodyConstraints originalConstraints;
    public float acceleration = 30f;
    public float maxSpeed = 45f;
    public float turnSpeed = 120f;
    public float gripForce = 0.9f;
    [SerializeField] GameObject realisitcCollider;
    [SerializeField] GameObject arcadeCollider;
    [SerializeField] InputAction accel;
    [SerializeField] InputAction turn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WheelCollider[] wheels = GetComponentsInChildren<WheelCollider>(true);
        foreach (WheelCollider wheel in wheels)
        {
            wheel.enabled = false;
        }
    }
    void Awake()
    {
        rb= GetComponent<Rigidbody>();
        accel.Enable();
        turn.Enable();
    }
    void OnEnable()
    {
        colliderChoose(false);
        if(!rb) rb = GetComponent<Rigidbody>();
        originalMass = rb.mass;
        originalDrag = rb.linearDamping;
        originalAngularDrag = rb.angularDamping;
        originalUseGravity = rb.useGravity;
        originalInterpolation = rb.interpolation;
        originalCollisionDetectionMode = rb.collisionDetectionMode;
        originalConstraints = rb.constraints;

        rb.mass = 1200f;
        rb.linearDamping = 0f;
        rb.angularDamping = 0.5f;
        rb.useGravity = true;

        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        rb.constraints =
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationZ;
    }
    void OnDisable()
    {

        colliderChoose(true);
        if (!rb) return;

        rb.mass = originalMass;
        rb.linearDamping = originalDrag;
        rb.angularDamping = originalAngularDrag;
        rb.useGravity = originalUseGravity;

        rb.interpolation = originalInterpolation;
        rb.collisionDetectionMode = originalCollisionDetectionMode;
        rb.constraints = originalConstraints;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        float throttleInput = accel.ReadValue<float>();
        Vector3 forwardVelocity = throttleInput * acceleration* transform.forward;
        rb.AddForce(forwardVelocity,ForceMode.Acceleration);

        Vector3 flatVel = rb.linearVelocity;
        flatVel.y = 0;
        if(flatVel.magnitude > maxSpeed)
        {
            flatVel = flatVel.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(flatVel.x, rb.linearVelocity.y, flatVel.z);
        }

        float steeringInput = turn.ReadValue<float>();
        if(flatVel.magnitude > 0.5f)
        {
            Quaternion turnRotation = Quaternion.Euler(0, steeringInput * turnSpeed * Time.fixedDeltaTime, 0);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
        if (Physics.Raycast(transform.position, Vector3.down, 1.2f))
        {
            Vector3 localVel = transform.InverseTransformDirection(rb.linearVelocity);
            localVel.x *= gripForce;
            rb.linearVelocity = transform.TransformDirection(localVel);
        }
    }
    private void colliderChoose(bool realisticMode)
    {
        realisitcCollider.SetActive(realisticMode);
        arcadeCollider.SetActive(!realisticMode);
    }
}
