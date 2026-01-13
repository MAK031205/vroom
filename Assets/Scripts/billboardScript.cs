using UnityEngine;

public class billboardScript : MonoBehaviour
{
    Camera cameraMain;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraMain = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + cameraMain.transform.rotation * Vector3.forward);
    }
}
