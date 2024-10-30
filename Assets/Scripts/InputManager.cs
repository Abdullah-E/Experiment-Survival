using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    
    private PlayerInput playerInput;
    public PlayerInput.StandingActions standing;
    public PlayerInput.SittingActions sitting;

    private PlayerMotor motor;
    private PlayerLook look;
    void Awake() {
        //initialization
        playerInput = new PlayerInput();
        standing = playerInput.Standing;
        sitting = playerInput.Sitting;

        motor = GetComponent<PlayerMotor>();
        //callback attached:
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
        sitting.Disable();
    }
    private void OnDisable() {
        standing.Disable();
        sitting.Disable();
    }

    public void SwitchToSitting(){
        standing.Disable();
        sitting.Enable();
    }

    public void SwitchToStanding(){
        sitting.Disable();
        standing.Enable();
    }
}
