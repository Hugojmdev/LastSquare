using UnityEngine;

public class SquareMovementCtrl : MonoBehaviour {

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

    void Start() {
        //GetComponent<Rigidbody2D>().inertia = 0;
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
        //Flip square depending on which direction is walking
        Flip(horizontalInput);
    }

    private void Flip(float horizontalInput){
        if(horizontalInput > 0.0) { //facing right
            transform.localScale = new Vector2(1, transform.localScale.y);
        } else if(horizontalInput < 0.0) {//facing left
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
    }

    private void Jump(){
        // Apply vertical force to make the player jump
        //GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
    }

    //This will verify if player is grounded.
    private bool IsGrounded(){
        Vector2 position = new Vector2(transform.position.x, transform.position.y - jumpColliderOffset);
        RaycastHit2D hit = Physics2D.BoxCast(position, jumpRange, 0.0f, Vector2.down,  0.0f);
        if (hit.collider != null && hit.collider.tag == TAG.GROUND) {
            return true;
        }
        return false;
    }
    private void OnDrawGizmos() {
        Vector2 position = new Vector2(transform.position.x, transform.position.y - jumpColliderOffset);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(position, jumpRange);
    }
}
