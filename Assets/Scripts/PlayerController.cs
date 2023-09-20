using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera camera;
    private CharacterController _characterController;
    private float _verticalSpeed;
    private float _mouseX;
    private float _mouseY;
    private float _currentAngleX;
    private const float GravityScale = 9.8f;
    private const float SpeedScale = 5f;
    private const float JumpForce = 8f; 
    private const float TurnSpeed = 240f;

    public PlayerController()
    {
        _verticalSpeed = 0f;
        _mouseX = 0f;
        _mouseY = 0f;
        _currentAngleX = 0f;
    }
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        RotateCharacter();
        MoveCharacter();
    }

    private void RotateCharacter()
    {
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(new Vector3(0f, _mouseX * TurnSpeed * Time.deltaTime, 0f));
        _currentAngleX += _mouseY * TurnSpeed * Time.deltaTime * -1f;
        _currentAngleX = Mathf.Clamp(_currentAngleX, -90f, 90f);
        camera.transform.localEulerAngles = new Vector3(_currentAngleX, 0f, 0f);
    }

    private void MoveCharacter()
    {
        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0f,
            Input.GetAxis("Vertical"));
        velocity = transform.TransformDirection(velocity) * SpeedScale;
        if (_characterController.isGrounded)
        {
            _verticalSpeed = 0f;
            if (Input.GetButton("Jump"))
            {
                _verticalSpeed = JumpForce;
            }
        }
        _verticalSpeed -= GravityScale * Time.deltaTime;
        velocity.y = _verticalSpeed;
        _characterController.Move(velocity * Time.deltaTime);
    }
}
