using UnityEngine;
using System.Collections;

public class FallState : State
{
    //private bool canDoubleJump = true;
    public FallState(PlayerScript player, StateMachine sm) : base(player, sm)
    {
    }

    public override void Enter()
    {
        Debug.Log("entering falling state");
        hasDoubleJumped = false;
        player.anim.Play("Fall");
        player.spriteRenderer = player.GetComponent<SpriteRenderer>();
    }
    public override void Exit()
    {
        //exit the double jump state
    }
    public override void Update()
    {
        ReadInput();

        if ( player.isGrounded)
        {
            sm.ChangeState(sm.idleState);
        }
        
        if (player.jumpAction.IsPressed() && player.jumpCount < 2)
        {
            sm.ChangeState(sm.jumpState);
        }
        
        if (player.moveAction.ReadValue<Vector2>().magnitude > 0.1f && player.isGrounded)
        {
            sm.ChangeState(sm.runState);
        }

        // Movement here
        Vector2 input = player.moveAction.ReadValue<Vector2>();
        player.rb.linearVelocityX = input.x * player.horizontalSpeed.x;

        if (input.x > 0)
        {
            player.spriteRenderer.flipX = false;
        }

        else if (input.x < 0)
        {
            player.spriteRenderer.flipX = true;
        }

        UIscript.ui.DrawText("*** This is the falling state ***\n");
        UIscript.ui.DrawText("Left/Right arrows = Move State");
        UIscript.ui.DrawText("E = Idle State");


    }

    public override void FixedUpdate()
    {
        //Fixed Update 
    }
}