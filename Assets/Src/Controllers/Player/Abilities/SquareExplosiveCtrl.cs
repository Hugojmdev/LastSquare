using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SquareExplosiveCtrl : MonoBehaviour {

    [SerializeField]
    private Vector3 range = new(0.0f, 1.4f);
    [SerializeField]
    private Vector3 downOffset = new(0.0f, 1.4f);
    [SerializeField]
    private float throwForce = 2f;
    [SerializeField]
    private float explosionRadius = 3f;
    [SerializeField]
    private float explosionForce = 100f;
    [SerializeField]
    private float explosionDelay = 3f;
 
    #region Components
    private Rigidbody2D rigidbody2d;
    private new ParticleSystem particleSystem;
    private Animator animator;
    private PlayerDataMgr playerDataMgr;
    private GameObject player;
    private Rigidbody2D playerRigidBody;
    private GameObject destructibleTilemap;
    #endregion

    private bool isHolding = false;


    // Start is called before the first frame update
    void Start() {
        playerDataMgr = PlayerDataMgr.GetInstance();
        rigidbody2d = GetComponent<Rigidbody2D>();
        particleSystem = GetComponent<ParticleSystem>();
        animator = GetComponent<Animator>();
        player = GameObject.Find(LevelObjects.PLAYER);
        destructibleTilemap = GameObject.Find(LevelObjects.DESTRUCTIBLE_TILE_MAP);
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
        
        //throws the explosive when L key is not pressed    
        if(Input.GetKeyUp(KeyCode.L)){
            if(playerRigidBody.velocity.x != 0) Throw();
            StartCoroutine(Explode());
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
        rigidbody2d?.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
    }

    private IEnumerator Explode(){
        yield return new WaitForSeconds(explosionDelay);
        particleSystem.Play();
        animator.SetTrigger("Explode");
        ExplodeEnemiesOrPlayer();
        ExplodeWallOrGround();

        yield return new WaitForSeconds(explosionDelay/4);
        // Destroy the explosive object
        Destroy(gameObject);
    }

    private void ExplodeEnemiesOrPlayer(){
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.down);
        // Process each hit
        foreach (RaycastHit2D hit in hits){
            Rigidbody2D rb = hit.collider.GetComponent<Rigidbody2D>();
            float force = explosionForce;
            if(hit.collider.CompareTag(Tag.PLAYER)) {
                force = explosionForce * 20;
                if(playerDataMgr.IsAlive()) playerDataMgr.UpdateHealth(-2);
            } 
            if (rb != null){
                rb.AddForce((rb.transform.position - transform.position).normalized * force, ForceMode2D.Impulse);
            }
        }
    }

    private void ExplodeWallOrGround(){
        Tilemap tilemap = destructibleTilemap.GetComponent<Tilemap>();
        List<Vector3Int> tilesPositions = GetTilesPositions();
        tilesPositions.ForEach(tilePosition => {
            if(Vector3.Distance(tilePosition, transform.position) <= explosionRadius 
                && Vector3.Distance(tilePosition, transform.position) >= -explosionRadius){
                tilemap.SetTile(tilePosition, null);
            }
        });
    }

     //Will retrieves the position of all Wall tile set.
    private List<Vector3Int> GetTilesPositions(){
        Tilemap tilemap = destructibleTilemap.GetComponent<Tilemap>();
        List<Vector3Int> positions = new();
        foreach (var position in tilemap.cellBounds.allPositionsWithin) {
            if (tilemap.HasTile(position)) {
                positions.Add(position);
            }
        }
        return positions;
    }

    private bool IsAbovePlayer(){
        return CastHelper.IsWithin2DBox(transform.position + downOffset, 
        range, Vector3.down, Layers.PLAYER_LAYER);
    }

    private void OnDrawGizmos() {
        Vector2 downPos = transform.position + downOffset;
        Gizmos.color = Color.gray;
        Gizmos.DrawWireCube(downPos, range);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
