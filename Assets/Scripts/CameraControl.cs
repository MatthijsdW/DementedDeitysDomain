using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    public float lookSpeed;
    private float lookDirection = 0;

    private void Update()
    {
        transform.Rotate(Vector3.up, lookSpeed * lookDirection * Time.deltaTime);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookDirection = context.ReadValue<float>();
    }
}
