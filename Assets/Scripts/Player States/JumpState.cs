//This is a derived class of State
//This means it inherits fields and methods from State.cs


using NUnit.Framework.Internal;
using UnityEngine;

public class JumpState : State
{
    protected bool canDoubleJump;
    //protected Vector2 verticalSpeed;

    public JumpState(PlayerScript player, StateMachine sm) : base(player, sm)
    {
    }

    public override void Enter()
    {
        player.isGrounded = false;
        player.anim.Play("Jump");
        verticalSpeed.y = 8f;
        player.rb.linearVelocityY = verticalSpeed.y;
        player.jumpCount++;
        player.spriteRenderer = player.GetComponent<SpriteRenderer>();
    }

    public override void Exit()
    {
        //exit the jump state
    }

    public override void Update()
    {
        Debug.Log("jump count=" + player.jumpCount);

        ReadInput();

        if (player.moveAction.ReadValue<Vector2>().magnitude > 0.1f && player.isGrounded)
        {
            sm.ChangeState(sm.runState);
        }
        
        if (player.rb.linearVelocityY < 0)
        {
            sm.ChangeState(sm.fallingState);
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

        UIscript.ui.DrawText("*** This is the jumping state ***\n");
        UIscript.ui.DrawText("Left/Right arrows = Move State");
        UIscript.ui.DrawText("E = Idle State");
        UIscript.ui.DrawText("jump count=" + player.jumpCount);


    }

    public override void FixedUpdate()
    {
        //Fixed Update 
    }
}
