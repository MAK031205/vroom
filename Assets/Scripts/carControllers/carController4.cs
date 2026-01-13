using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class carController4 : MonoBehaviour
{
    public Rigidbody motorShpere;
    public float throttle = 200f;
    public float steering = 120f;
    public float gravity = 10f;
    public float downwardPull = 3f;
    float throttleForce ;
    float steeringForce ;
    bool isGrounded ;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] InputAction accel;
    [SerializeField] InputAction turn;
    void Awake()
    {
        accel.Enable();
        turn.Enable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        motorShpere.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        float throttleInput = -accel.ReadValue<float>();
        throttleForce = throttleInput < 0 ? throttle * throttleInput : throttle*throttleInput * 0.5f;
        float steeringInput = turn.ReadValue<float>();
        steeringForce = steering * steeringInput *Time.deltaTime;
        if(throttleInput == 0){
            steeringForce = 0;
        }
        transform.Rotate(0,steeringForce,0, Space.World);
        transform.position = motorShpere.transform.position;
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position + Vector3.up*0.1f,-transform.up,out hit,1.5f,groundLayer);
        if (isGrounded)
        {
            Vector3 slopeForward = Vector3.ProjectOnPlane(transform.forward, hit.normal);
            Quaternion targetRotation = Quaternion.FromToRotation(slopeForward,hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation, 10f * Time.deltaTime);

        }
        else
        {
            
            Quaternion upright = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, upright, 2f * Time.deltaTime);
        }
        

    }
    void FixedUpdate()
    {
        if(isGrounded){
        motorShpere.AddForce(transform.forward * throttleForce , ForceMode.Acceleration);
        motorShpere.AddForce(-transform.up * downwardPull, ForceMode.Acceleration);
        }
        else
        {
            motorShpere.AddForce(transform.up * -gravity, ForceMode.Acceleration);
        }
    }
}
