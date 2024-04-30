using System;
using UnityEngine;

public class PlayerMovementCtrl : MonoBehaviour {

    #region Walk variables

    [SerializeField]
    float moveSpeed = 5f; // Speed at which the object will move
    [SerializeField]
    private float fallingSpeed = -12;

    #endregion

    #region Jump variables

    [SerializeField]
    private float jumpForce = 5f; // Force applied when jumping

    [SerializeField] 
    private Vector2 groundRange = new(0.8f,0.2f);

    [SerializeField]
    private float groundCastOffset; 

    #endregion 

    #region components
    private Rigidbody2D rigidBody;
    #endregion

    private Vector3 initialPosition;
    private SpriteRenderer sprite;

    void Start() {
        rigidBody =  GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update() {
        PreventHighSpeedWhenFalling();
        Walk();
        
        // Check for player input to trigger jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()){
            Jump();
        } 

        Isfalling();
    }

    private void Walk(){
        // Get the horizontal input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        //Calculate the movement amount based on input and speed
        rigidBody.velocity = new Vector2(horizontalInput * moveSpeed, rigidBody.velocity.y);
        //Flip player depending on the horizontal input
        if (horizontalInput !=0) sprite.flipX = horizontalInput < 0;//transform.localScale = new Vector2(horizontalInput, transform.localScale.y);
    }

    #region Jump
    private void Jump(){
        // Apply vertical force to make the player jump
        rigidBody.velocity = Vector2.up * jumpForce;
    }

    //This will verify if player is grounded.
    public bool IsGrounded(){
        Vector2 position = new(transform.position.x, transform.position.y - groundCastOffset);
        return CastHelper.IsWithin2DBox(position, groundRange, Vector2.down, Tag.GROUNDED_TAGS);
    }

    private void OnDrawGizmos() {
        Vector2 position = new(transform.position.x, transform.position.y - groundCastOffset);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(position, groundRange);
    }
    #endregion

    private void Isfalling(){
        int fallDistance = Math.Abs((int)transform.position.y - (int)initialPosition.y);
        bool isFalling = transform.position.y < initialPosition.y;

        if(fallDistance >= 10 && isFalling) Restart();
    }

    //Prevents to increase speed when falling
    private void PreventHighSpeedWhenFalling(){
           if(rigidBody.velocity.y <= fallingSpeed){
                rigidBody.velocity = new Vector2(0.0f, fallingSpeed);
            }
        }

    public void Restart(){
        transform.position = initialPosition;
    }
}
