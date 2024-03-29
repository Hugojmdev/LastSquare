using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCtrl : MonoBehaviour {

    #region Walk variables

    [SerializeField]
    float moveSpeed = 5f; // Speed at which the object will move

    #endregion

    #region Jump variables

    [SerializeField]
    private float jumpForce = 5f; // Force applied when jumping

    [SerializeField] 
    private Vector2 jumpRange = new Vector2(0.8f,0.2f);

    [SerializeField]
    private float jumpColliderOffset; 

    #endregion 

    #region components
    private Rigidbody2D rigidBody;
    #endregion

    void Start() {
        rigidBody =  GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        Walk();
        
        // Check for player input to trigger jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()){
            Jump();
        } 
    }

    private void Walk(){
        // Get the horizontal input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        // Calculate the movement amount based on input and speed
        float movement = horizontalInput * moveSpeed * Time.deltaTime;
        // Calculate the new position
        Vector3 newPosition = transform.position + new Vector3(movement, 0f, 0f);
        // Apply the new position
        transform.position = newPosition;
        //Flip player depending on the horizontal input
        if (horizontalInput !=0) transform.localScale = new Vector2(horizontalInput, transform.localScale.y);
    }

    #region Jump
    
    private void Jump(){
        // Apply vertical force to make the player jump
        rigidBody.velocity = Vector2.up * jumpForce;
    }

    //This will verify if player is grounded.
    private bool IsGrounded(){
        Vector2 position = new Vector2(transform.position.x, transform.position.y - jumpColliderOffset);
        RaycastHit2D hit = Physics2D.BoxCast(position, jumpRange, 0.0f, Vector2.down,  0.0f);
        if (hit.collider != null && Tag.JUMP_TAGS.Contains(hit.collider.tag)) {
            return true;
        }
        return false;
    }
    
    private void OnDrawGizmos() {
        Vector2 position = new Vector2(transform.position.x, transform.position.y - jumpColliderOffset);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(position, jumpRange);
    }
    #endregion
}
