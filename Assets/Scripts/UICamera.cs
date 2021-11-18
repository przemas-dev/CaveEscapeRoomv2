using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICamera : MonoBehaviour
{
    public Camera camera;
    private Vector3 offset = new Vector3(0.35f, 0.01f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        camera.fieldOfView = 24.0f;
        camera.nearClipPlane = 0.1f;
        camera.farClipPlane = 1.0f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttachCameraToObject(GameObject observedObject)
    {
        camera.transform.position = observedObject.transform.position + offset;
    }
}
