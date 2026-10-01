
//This is the base class 
// It defines the common methods and fields that all other states inherit
// You can include methods that you want to allow other states to use here

using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class State
{
    protected PlayerScript player;
    protected StateMachine sm;

    public float verticalInput;
    public float horizontalInput;
    public bool enterFallingState;
    protected Vector2 verticalSpeed;
    protected bool hasJumped;
    public bool hasDoubleJumped;

    // base constructor
    public State(PlayerScript player, StateMachine sm)
    {
        this.player = player;
        this.sm = sm;
    }

    //methods that can be overriden by each state
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void Exit() { }
    public virtual void OnCollisionEnter2D(Collision2D collision) { }
    public virtual void OnTriggerEnter2D(Collider2D collision) { }
    public virtual void OnTriggerExit2D(Collider2D collision) { }

    //Common Shared Methods
    //Put methods that you wish to share between other states here
    //Set them to be public
    public void TestMethod(string text)
    {
        //Debug.Log(text);
    }
    
    public void ReadInput()
    {
    }



}
