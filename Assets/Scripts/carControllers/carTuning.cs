using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class carTuning : MonoBehaviour
{
    [SerializeField]carController1 car;
    public Slider torqueSlider;
    public Slider maxTorqueSlider;
    public Slider steeringAngleSlider;
    public Slider downwardForceSlider;
    public Slider rearDriftStiffnessSlider;
    public Slider driftSteerAssistSlider;
    public Slider steerAssistSlider;
    public TMP_Text torqueValueText;
    public TMP_Text maxTorqueValueText;
    public TMP_Text steeringAngleValueText;
    public TMP_Text downwardForceValueText;
    public TMP_Text rearDriftStiffnessValueText;
    public TMP_Text driftSteerAssistValueText;
    public TMP_Text steerAssistValueText;
    
    [System.Serializable]
    public struct CarPreset
    {
        public float torque;
        public float maxTorque;
        public float steerAngle;
        public float downPull;
        public float rearDriftStiffness;
        public float steerAssist;
        public float driftSteerAssist;
    }
    public CarPreset defaultPreset;
    public CarPreset gripPreset;
    public CarPreset driftPreset;
    public CarPreset arcadePreset;

    // Start is called once before the first
    void Start()
    {
        if(!car) return;
        torqueSlider.value = car.torque;
        maxTorqueSlider.value = car.maxTorque;
        steeringAngleSlider.value = car.maxSteeringAngle;
        downwardForceSlider.value = car.downForce;
        rearDriftStiffnessSlider.value = car.rearDriftStiffness;
        driftSteerAssistSlider.value = car.driftSteerAssist;
        steerAssistSlider.value = car.steerAssist;
    }

    // Update is called once per frame
    void Update()
    {
        if(!car) return;
        car.torque = torqueSlider.value;
        torqueValueText.text = car.torque.ToString("F2");
        car.maxTorque = maxTorqueSlider.value;
        maxTorqueValueText.text = car.maxTorque.ToString("F2");
        car.maxSteeringAngle = steeringAngleSlider.value;
        steeringAngleValueText.text = car.maxSteeringAngle.ToString("F0");
        car.downForce = downwardForceSlider.value;
        downwardForceValueText.text = car.downForce.ToString("F0");
        car.rearDriftStiffness = rearDriftStiffnessSlider.value;
        rearDriftStiffnessValueText.text = car.rearDriftStiffness.ToString("F2");
        car.driftSteerAssist = driftSteerAssistSlider.value;
        driftSteerAssistValueText.text = car.driftSteerAssist.ToString("F2");
        car.steerAssist = steerAssistSlider.value;
        steerAssistValueText.text = car.steerAssist.ToString("F2");
    }
    void ApplyPreset(CarPreset p)
    {
        car.torque = p.torque;
        car.maxTorque = p.maxTorque;
        car.maxSteeringAngle = p.steerAngle;
        car.downForce = p.downPull;
        car.rearDriftStiffness = p.rearDriftStiffness;
        car.steerAssist = p.steerAssist;
        car.driftSteerAssist = p.driftSteerAssist;

        torqueSlider.value = p.torque;
        maxTorqueSlider.value = p.maxTorque;
        steeringAngleSlider.value = p.steerAngle;
        downwardForceSlider.value = p.downPull;
        rearDriftStiffnessSlider.value = p.rearDriftStiffness;
        steerAssistSlider.value = p.steerAssist;
        driftSteerAssistSlider.value = p.driftSteerAssist;
    }
    public void ApplyDefaultPreset()
    {
        ApplyPreset(defaultPreset);
    }

    public void ApplyGripPreset()
    {
        ApplyPreset(gripPreset);
    }

    public void ApplyDriftPreset()
    {
        ApplyPreset(driftPreset);
    }

    public void ApplyArcadePreset()
    {
        ApplyPreset(arcadePreset);
    }
}
