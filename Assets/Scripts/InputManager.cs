using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    
    private PlayerInput playerInput;
    private PlayerInput.StandingActions standing;

    private PlayerMotor motor;
    private PlayerLook look;
    void Awake() {
        playerInput = new PlayerInput();
        standing = playerInput.Standing;

        motor = GetComponent<PlayerMotor>();
        standing.Jump.performed += ctx => motor.Jump();

        look = GetComponent<PlayerLook>();
    }

    private void FixedUpdate() {
        motor.ProcessMove(standing.Movement.ReadValue<Vector2>());
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        Vector2 input = standing.Look.ReadValue<Vector2>();
        // Debug.Log(look);
        look.ProcessLook(input);
    }

    private void OnEnable() {
        standing.Enable();
    }
    private void OnDisable() {
        standing.Disable();
    }
}
