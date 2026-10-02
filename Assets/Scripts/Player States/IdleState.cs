
//This is a derived class of State
//This means it inherits fields and methods from State.cs

using UnityEngine;
using System.Collections;

public class IdleState : State
{
    // constructor
    
    public IdleState( PlayerScript player, StateMachine sm) : base(player, sm)
    {
    }

    public override void Enter()
    {
        // this method is called when the state begins
        hasJumped = false;
        hasDoubleJumped = false;
        Debug.Log("entering idle state");
        player.jumpCount = 0;
        Debug.Log("reset jump count");
        player.anim.Play("Idle");
    }

    public override void Exit()
    {
        // this method is called when the state has finished
        Debug.Log("exiting idle state");

        //you should disable any running coroutines here
        player.StopAllCoroutines();
    }


    public override void Update()
    {
        if (player.moveAction.ReadValue<Vector2>().magnitude > 0.1f )
        {
            sm.ChangeState(sm.runState);
        }

        if (player.jumpAction.IsPressed() && player.GroundCheck() && player.jumpCount < 2)
        {
            player.jumpCount++;
            sm.ChangeState(sm.jumpState);
        }

        UIscript.ui.DrawText("*** This is the idle state ***\n");
        UIscript.ui.DrawText("Space = Jump State");
        UIscript.ui.DrawText("Left/Right arrows = Move State");
        UIscript.ui.DrawText("C = Start the coroutine");
    }

    public override void FixedUpdate()
    {
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided");
    }


    public IEnumerator IdleCo()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(2);
            Debug.Log("Coroutine step 1");

            yield return new WaitForSeconds(2);
            Debug.Log("Coroutine step 2");

            Debug.Log("Coroutine repeat " + (i+1));

        }
        yield break;
    }




}
