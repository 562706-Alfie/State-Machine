
//This is a derived class of State
//This means it inherits fields and methods from State.cs

using UnityEngine;
using UnityEngine.InputSystem;

public class RunState : State
{
    protected float rotationSpeed;

    public RunState(PlayerScript player, StateMachine sm) : base(player, sm)
    {
    }

    public override void Enter()
    {
        //player.horizontalSpeed.x = 3f;
        base.Enter();
        horizontalInput = verticalInput = 0.0f;
        Debug.Log("entering running state");
        player.anim.Play("Run");
        player.spriteRenderer = player.GetComponent<SpriteRenderer>();
        player.jumpCount = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }



    public override void Update()
    {
        ReadInput();

        if (player.jumpAction.IsPressed() && player.jumpCount < 2)
        {
            player.jumpCount++;
            sm.ChangeState(sm.jumpState);
        }
        
        if (player.GroundCheck())
        {
            if (player.rb.linearVelocityX < 0.1f && player.rb.linearVelocityX > -0.1f)
            {
                sm.ChangeState(sm.idleState);
            }
        }
        
        if (!player.GroundCheck() && !player.jumpAction.IsPressed()) // For use when running of platforms, so the player can't jump twice
        {
            player.jumpCount++;
            sm.ChangeState(sm.fallingState);
        }
        
        if (player.rb.linearVelocityX == 0f)
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

        UIscript.ui.DrawText("*** This is the running state ***\n");
        UIscript.ui.DrawText("Left/Right arrows = Move Sprite");
        UIscript.ui.DrawText("E = Idle State");
        UIscript.ui.DrawText("Space = Jump state");
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided in runstate");

        if( collision.tag == "enemy")
        {
            collision.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        }
    }
    public override void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit collision in runstate");

        if (collision.tag == "enemy")
        {
            collision.GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.1f, 0.1f);
        }
    }



    public override void FixedUpdate()
    {
    }
}
