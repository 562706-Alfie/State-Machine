
//player.cs is the Monobehaviour and owns the Unity components
//It passes control to the statemachine

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    StateMachine sm;
    public bool isGrounded;
    public Animator anim;

    public int jumpCount;
    public Vector2 horizontalSpeed;
    public SpriteRenderer spriteRenderer;


    //define the actions
    public InputAction moveAction;
    public InputAction crouchAction;
    public InputAction jumpAction;
    public InputAction interactAction;
    public InputAction doubleJumpAction;
    
    private void Start()
    {
        sm = new StateMachine(this); //"this" means - pass a reference of this script (player script) to the statemachine
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        sm.Init(sm.idleState); //this will be the first state to run 

        horizontalSpeed.x = 3f;

        //initialise the actions. Found in edit, project settings, input systemn package. The inputs there are automatically assigned.
        moveAction = InputSystem.actions.FindAction("Move");
        crouchAction = InputSystem.actions.FindAction("Crouch");
        interactAction = InputSystem.actions.FindAction("Interact");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    {
        //do not put any of your own methods here - they go in the individual state files
        sm.Update();

        UIscript.ui.DrawText("Current state= " + sm.currentState + "  Last state= " + sm.lastState);

    }

    private void FixedUpdate()
    {
        //do not put any of your own methods here - they go in the state files
        sm.FixedUpdate();
    }

    //add your additional collision handling here
    void OnCollisionEnter2D(Collision2D collision)
    {
        sm.currentState.OnCollisionEnter2D(collision);

        if( collision.gameObject.tag == "Ground")
        {
            print("grounded");
            isGrounded = true;
        }

    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        sm.currentState.OnTriggerEnter2D(collision);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        sm.currentState.OnTriggerExit2D(collision);
    }



}
