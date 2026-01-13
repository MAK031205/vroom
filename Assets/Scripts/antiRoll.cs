using UnityEngine;

public class antiRoll : MonoBehaviour
{
    public WheelCollider WheelL;
    public WheelCollider WheelR;
    public float AntiRoll = 5000.0f; // Start high, tweak down if too stiff
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        WheelHit hit;
        float travelL = 1.0f;
        float travelR = 1.0f;

        // Calculate how much the Left wheel is compressed
        bool groundedL = WheelL.GetGroundHit(out hit);
        if (groundedL)
            travelL = (-WheelL.transform.InverseTransformPoint(hit.point).y - WheelL.radius) / WheelL.suspensionDistance;

        // Calculate how much the Right wheel is compressed
        bool groundedR = WheelR.GetGroundHit(out hit);
        if (groundedR)
            travelR = (-WheelR.transform.InverseTransformPoint(hit.point).y - WheelR.radius) / WheelR.suspensionDistance;

        // The difference is the force we need to counter
        float antiRollForce = (travelL - travelR) * AntiRoll;

        // Apply opposite forces to keep the car flat
        if (groundedL)
            rb.AddForceAtPosition(WheelL.transform.up * -antiRollForce, WheelL.transform.position); 
        if (groundedR)
            rb.AddForceAtPosition(WheelR.transform.up * antiRollForce, WheelR.transform.position); 
    }
}
