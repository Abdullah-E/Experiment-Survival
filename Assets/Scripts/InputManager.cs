using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    
    public PlayerInput playerInput;
    public PlayerInput.StandingActions standing;
    public PlayerInput.SittingActions sitting;

    public InputActionMap CurrentActionMap {get;private set;}
    public Bench activeBench {get;private set;}

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

        CurrentActionMap = standing;
    }

    private void FixedUpdate() {
        if(CurrentActionMap == standing.Get()){
        motor.ProcessMove(standing.Movement.ReadValue<Vector2>());
        }
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        // Vector2 input = standing.Look.ReadValue<Vector2>();
        Vector2 input;
        if(CurrentActionMap == standing.Get()){
            input = standing.Look.ReadValue<Vector2>();
        }else{
            input = sitting.Look.ReadValue<Vector2>();
        }
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

    public void SwitchToSitting(Bench bench){
        standing.Disable();
        sitting.Enable();
        CurrentActionMap = sitting;
        activeBench = bench;
    }

    public void SwitchToStanding(){
        sitting.Disable();
        standing.Enable();
        CurrentActionMap = standing;
        activeBench = null;
    }
}
