using UnityEngine;
using UnityEngine.InputSystem;

public class devUIManage : MonoBehaviour
{
    public GameObject devUI;
    [SerializeField] InputAction openTuningMenu;
    void OnEnable()
    {
        openTuningMenu.Enable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float tuningMenuInput = openTuningMenu.ReadValue<float>();
        if(tuningMenuInput != 0)
        {
            devUI.SetActive(!devUI.activeSelf);
        }
    }
}
