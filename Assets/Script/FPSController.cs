using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField]
    float mouseSensitivity = 3.0f;

    [SerializeField]
    float moveSpeed = 5.0f;

    CharacterController characterController;
    Transform cameraXForm;
    Vector3 velocity;
    Vector3 look;

    float GetMouseX()
    {
        return UnityEngine.Input.GetAxis("Mouse X");
    }

    float GetMouseY()
    {
        return UnityEngine.Input.GetAxis("Mouse Y");
    }

    float GetForwardBack()
    {
        return UnityEngine.Input.GetAxis("Vertical");
    }

    float GetLeftRight()
    {
        return UnityEngine.Input.GetAxis("Horizontal");
    }

    void UpdateGravity()
    {
        var gravity = Physics.gravity * Time.deltaTime;
        velocity.y = characterController.isGrounded ? -1.0f : velocity.y + gravity.y;
    }

    void UpdateMovement()
    {
        var x = GetLeftRight();
        var y = GetForwardBack();

        var input = new Vector3();
        input += transform.forward * y;
        input += transform.right * x;
        input = input.normalized;

        characterController.Move((input * moveSpeed + velocity) * Time.deltaTime);
    }

    void UpdateLook()
    {
        look.x += GetMouseX() * mouseSensitivity;
        look.y += GetMouseY() * mouseSensitivity;

        look.y = Mathf.Clamp(look.y, -89.0f, 89.0f);

        cameraXForm.localRotation = Quaternion.Euler(-look.y, 0.0f, 0.0f);
        transform.localRotation = Quaternion.Euler(0.0f, look.x, 0.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        cameraXForm = transform.Find("CameraFPS");

        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGravity();
        UpdateMovement();
        UpdateLook();
    }
}
