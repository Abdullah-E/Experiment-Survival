using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : BaseState
{   
    private float losePlayerTimer;
    private float moveTimer;
    public override void Enter()
    {
        
    }

    public override void Execute()
    {
        if(controller.PlayerVisible()){
            losePlayerTimer = 0;
            controller.Agent.SetDestination(player.transform.position);
            
        }
        else{
            losePlayerTimer += Time.deltaTime;
            if(losePlayerTimer > 5){
                stateMachine.SwitchState(new Patrol());
            }
        }
    }

    public override void Exit()
    {
        
    }
}
