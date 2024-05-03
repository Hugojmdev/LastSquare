using UnityEngine;

public class SquareExplosiveCtrl : MonoBehaviour {

    [SerializeField]
    private Vector3 range = new(0.0f, 1.4f);
    [SerializeField]
    private Vector3 downOffset = new(0.0f, 1.4f);
    [SerializeField]
    private float throwForce = 2f;
    private Rigidbody2D rigidbody2d;
    private GameObject player;
    private Rigidbody2D playerRigidBody;

    private bool isHolding = false;


    // Start is called before the first frame update
    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        player = GameObject.Find(LevelObjects.PLAYER);
        if(player != null){
            playerRigidBody = player.GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update() {
       if(Input.GetKey(KeyCode.L) && IsAbovePlayer()) {
            isHolding = true;
       }
       if(Input.GetKeyUp(KeyCode.L) || !IsAbovePlayer()) {
            isHolding = false;
       } 

       if(Input.GetKeyUp(KeyCode.L) && playerRigidBody.velocity.x != 0){
            Throw();
       }

       if(isHolding) HoldExplosive();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(!other.gameObject.CompareTag(Tag.PLAYER)) isHolding = false;
    }

    private void HoldExplosive(){
        rigidbody2d.velocity = playerRigidBody.velocity;
    }

    private void Throw(){
        Vector2 throwDirection = playerRigidBody.velocity.x > 0 ? Vector2.right : Vector2.left;
        if (rigidbody2d != null){
            rigidbody2d.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
        }
    }

    private bool IsAbovePlayer(){
        return CastHelper.IsWithin2DBox(transform.position + downOffset, 
        range, Vector3.down, Layers.PLAYER_LAYER);
    }

    private void OnDrawGizmos() {
        Vector2 downPos = transform.position + downOffset;
        Gizmos.color = Color.gray;
        Gizmos.DrawWireCube(downPos, range);
    }
}
