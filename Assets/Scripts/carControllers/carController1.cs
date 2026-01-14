using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class carController1 : MonoBehaviour
{
    public Transform centerOfMass;

    public Transform wheelRightBack;
    public Transform wheelLeftBack;
    public Transform wheelRightFront;
    public Transform wheelLeftFront;

    public WheelCollider wheelColliderRightBack;
    public WheelCollider wheelColliderLeftBack;
    public WheelCollider wheelColliderRightFront;
    public WheelCollider wheelColliderLeftFront;

    public float torque = 2f;
    public float maxTorque  = 4f;
    public float maxSteeringAngle = 30f;

    public float downForce = 50f;

    public float steerAssist = 0.5f;

    public float minCorneringSpeed = 20f;

    public float corneringBrakeFactor = 0.5f;

    public float rearDriftStiffness = 0.5f;

    public float normalRearStiffness = 0.8f;

    public float driftTorqueBoost = 1.4f;

    public float driftSteerAssist = 1.2f;

    public float driftSlipThreshold = 6f;

    public float yawDamping = 2.5f;

    public float driftYawDamping = 1.2f;

    [SerializeField] InputAction accel;
    [SerializeField] InputAction turn;
    [SerializeField] InputAction respawn;
    [SerializeField] InputAction restart;
    [SerializeField] TMP_Text speedText;

    private Rigidbody rb;

    private float currentTorqueMultiplier = 1.0f;

    bool isDrifting;

    void OnEnable()
    {
        accel.Enable();
        turn.Enable();
        respawn.Enable();
        restart.Enable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;

        // Initialize rear wheel grip
        applyFriction(wheelColliderRightBack, 0.8f);
        applyFriction(wheelColliderLeftBack, 0.8f);

        // Initialize front wheel grip (higher for steering)
        applyFriction(wheelColliderRightFront, 1.5f);
        applyFriction(wheelColliderLeftFront, 1.5f);
    }

    void Update()
    {
        if (respawn.WasPressedThisFrame())
        {
            recoverCar();
        }
        if (restart.WasPressedThisFrame())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        checkSurface();
        updateSpeedometerColor();
    }

    private void updateSpeedometerColor()
    {
        float realSpeed = rb.linearVelocity.magnitude * 3.6f ;
        float speedMultiplier = Mathf.Lerp(1.0f,1.5f,Mathf.InverseLerp(30f,120f,realSpeed));
        float displaySpeed = realSpeed * speedMultiplier;
        speedText.text = displaySpeed.ToString("0") + " km/h";
        speedText.color = GetSpeedColor(displaySpeed);
    }

    Color GetSpeedColor(float speed)
    {
        if (speed <= 30f)
        {
            float t = Mathf.InverseLerp(0f, 30f, speed);
            return Color.Lerp(Color.white, Color.green, t);
        }

        if (speed <= 60f)
        {
            float t = Mathf.InverseLerp(30f, 60f, speed);
            return Color.Lerp(Color.green, Color.yellow, t);
        }

        float highSpeedT = Mathf.InverseLerp(60f, 110f, speed);
        return Color.Lerp(Color.yellow, Color.red, highSpeedT);
    }
    void FixedUpdate()
    {
        animateWheels();

        processTurning();

        processAcceleration();

        addDownwardForce();

        applyCorneringBrake();

        updateDriftingStiffness();

        applyYawStability();
    }
    private void applyYawStability()
    {
        float yawVelocity = rb.angularVelocity.y;
        float speed = rb.linearVelocity.magnitude;
        float yawThreshold = isDrifting ? 1.2f : 2.0f;

        // Apply corrective torque only if rotation is excessive
        if (Mathf.Abs(yawVelocity) > yawThreshold)
        {
            float damping = isDrifting ? driftYawDamping : yawDamping;
            rb.AddTorque(Vector3.down * yawVelocity * damping, ForceMode.Acceleration);
        }
    }
    private void updateDriftingStiffness()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(rb.linearVelocity);
        float lateralSlip = Mathf.Abs(localVelocity.x);
        float steeringInput = Mathf.Abs(turn.ReadValue<float>());

        isDrifting =
            steeringInput > 0.2f &&
            lateralSlip > driftSlipThreshold;

        // Choose rear grip based on drift state
        float targetRearStiffness = isDrifting
            ? rearDriftStiffness
            : normalRearStiffness;

        // Smoothly transition rear grip to avoid snap behavior
        float currentRearStiffness = Mathf.Lerp(
            wheelColliderLeftBack.sidewaysFriction.stiffness,
            targetRearStiffness,
            5f * Time.fixedDeltaTime
        );

        // Apply updated rear grip
        applyFriction(wheelColliderRightBack, currentRearStiffness);
        applyFriction(wheelColliderLeftBack, currentRearStiffness);
    }
    private void checkSurface()
    {
        WheelHit hit;

        // Detect surface type (road vs grass)
        if (wheelColliderLeftBack.GetGroundHit(out hit) ||
            wheelColliderRightBack.GetGroundHit(out hit))
        {
            if (hit.collider.CompareTag("Grass"))
            {
                rb.linearDamping = 2f;
                currentTorqueMultiplier = 0.5f;
            }
            else
            {
                rb.linearDamping = 0.05f;
                currentTorqueMultiplier = 1.0f;
            }
        }
    }
    // Respawn the car if respawn input is pressed
    private void recoverCar()
    {
        transform.position += Vector3.up * 2f;
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

        // Stop all movement
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    // Apply downforce proportional to speed
    private void addDownwardForce()
    {
        rb.AddForce(-transform.up * downForce * rb.linearVelocity.magnitude);
    }

    private void applyFriction(WheelCollider wheel, float ss)
    {
        var side = wheel.sidewaysFriction;
        side.stiffness = ss;
        wheel.sidewaysFriction = side;

        var forward = wheel.forwardFriction;
        forward.stiffness = 1.6f;
        wheel.forwardFriction = forward;
    }

    private void animateWheels()
    {
        Vector3 wheelRightBackPosition = Vector3.zero;
        Quaternion wheelRightBackRotation = Quaternion.identity;
        wheelColliderRightBack.GetWorldPose(out wheelRightBackPosition, out wheelRightBackRotation);
        wheelRightBack.position = wheelRightBackPosition;
        wheelRightBack.rotation = wheelRightBackRotation;

        Vector3 wheelLeftBackPosition = Vector3.zero;
        Quaternion wheelLeftBackRotation = Quaternion.identity;
        wheelColliderLeftBack.GetWorldPose(out wheelLeftBackPosition, out wheelLeftBackRotation);
        wheelLeftBack.position = wheelLeftBackPosition;
        wheelLeftBack.rotation = wheelLeftBackRotation;

        Vector3 wheelRightFrontPosition = Vector3.zero;
        Quaternion wheelRightFrontRotation = Quaternion.identity;
        wheelColliderRightFront.GetWorldPose(out wheelRightFrontPosition, out wheelRightFrontRotation);
        wheelRightFront.position = wheelRightFrontPosition;
        wheelRightFront.rotation = wheelRightFrontRotation;

        Vector3 wheelLeftFrontPosition = Vector3.zero;
        Quaternion wheelLeftFrontRotation = Quaternion.identity;
        wheelColliderLeftFront.GetWorldPose(out wheelLeftFrontPosition, out wheelLeftFrontRotation);
        wheelLeftFront.position = wheelLeftFrontPosition;
        wheelLeftFront.rotation = wheelLeftFrontRotation;
    }

    private void processTurning()
    {
        float turnInput = turn.ReadValue<float>();
        float steeringAngle = maxSteeringAngle * turnInput;
        Vector3 localVelocity = transform.InverseTransformDirection(rb.linearVelocity);

        // Apply steering assist only when moving forward
        if (localVelocity.z > 5f)
        {
            float slipAngle = Mathf.Atan2(localVelocity.x, localVelocity.z) * Mathf.Rad2Deg;
            float speed = rb.linearVelocity.magnitude;
            float speedAssistFactor = Mathf.Lerp(
                1f,
                0.75f,
                Mathf.InverseLerp(30f, 55f, speed)
            );
            float assist = steerAssist * speedAssistFactor;
            if (isDrifting)
            {
                assist *= driftSteerAssist;
            }
            steeringAngle += slipAngle * assist;

            // Extra turn-in boost at lower speeds
            float turnInBoost = Mathf.Lerp(
                1.15f,
                1.0f,
                Mathf.InverseLerp(15f, 35f, speed)
            );

            steeringAngle *= turnInBoost;
        }

        // Extra steering at very low speeds
        if (rb.linearVelocity.magnitude < 15f)
        {
            steeringAngle *= 1.25f;
        }
        float finalAngle = Mathf.Clamp(steeringAngle, -maxSteeringAngle, maxSteeringAngle);
        wheelColliderLeftFront.steerAngle = finalAngle;
        wheelColliderRightFront.steerAngle = finalAngle;
    }
    private void processAcceleration()
    {
        float accelerationInput = accel.ReadValue<float>();
        float driftBoost = isDrifting ? driftTorqueBoost : 1f;
        float finalTorque = accelerationInput > 0
            ? torque * accelerationInput * currentTorqueMultiplier * driftBoost * 100f
            : torque * accelerationInput * currentTorqueMultiplier * 100f * 0.5f;

        wheelColliderLeftFront.motorTorque = 0;
        wheelColliderRightFront.motorTorque = 0;

        wheelColliderLeftBack.motorTorque = finalTorque;
        wheelColliderRightBack.motorTorque = finalTorque;
    }
    private void applyCorneringBrake()
    {
        float turnInput = Mathf.Abs(turn.ReadValue<float>());
        float currentSpeed = transform.InverseTransformDirection(rb.linearVelocity).z;

        // Apply drag only when cornering at speed
        if (currentSpeed > minCorneringSpeed && turnInput > 0.1f)
        {
            rb.linearDamping = Mathf.Lerp(0, corneringBrakeFactor, turnInput);
        }
        else
        {
            rb.linearDamping = 0;
        }
    }
}
