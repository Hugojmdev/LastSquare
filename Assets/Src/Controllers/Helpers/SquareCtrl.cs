using UnityEngine;

public class SquareCtrl : MonoBehaviour {

    [SerializeField]
    private GameObject groundSquare;

    [SerializeField]
    private GameObject damageSquare;

    [SerializeField]
    private GameObject explosiveSquare;
    
    [SerializeField] 
    private Vector2 spawnRange = new Vector2(0.8f,0.2f);

    [SerializeField]
    private float colliderOffset = 0.0f;

    private PlayerDataMgr playerDataMgr;

    // Start is called before the first frame update
    void Start() {
        playerDataMgr = new PlayerDataMgr();
    }

    // Update is called once per frame
    void Update() {
        //Will spawn a 'square' if it's available to do so
        Spawn();
    }

    //Spawns a square depending on what key was pressed down and square cost
     private void Spawn(){
        //Gets available amount of squares
        int squares = (int)playerDataMgr.GetPlayer().squares;
        //Initialize square prefab.
        GameObject squarePrefab = new("NoSquare");
        //Spawns 'groundSquare' if press 'J' and there are squares available, cost=1 square
        if(Input.GetKeyDown(KeyCode.J) && squares >= 1) squarePrefab = groundSquare;
        //Spawns 'damageSquare' if press 'K' and there are squares available, cost=1 square 
        if(Input.GetKeyDown(KeyCode.K) && squares >= 1) squarePrefab = damageSquare;
        //Spawns 'explosiveSquare' if press 'L' and there are squares available, cost=2 squares
        if(Input.GetKeyDown(KeyCode.L) && squares >= 2) squarePrefab = explosiveSquare;
        //Verify if the give target position is available to spawn and square prefab is an actual square.
        if(IsTargetPositionAvailable() && Tag.SQUARE_TAGS.Contains(squarePrefab.tag)) {
            //Get spawn position.
            Vector3 playerPosition = transform.position;
            Vector3 spawnPosition = new Vector3(playerPosition.x, playerPosition.y -0.5f, playerPosition.z); 
            //Instantiates a new consumable object to be spawned.        
            GameObject square = Instantiate(squarePrefab, spawnPosition, Quaternion.identity);
            square.name = squarePrefab.name;
            square.transform.SetParent(GameObject.Find(LevelObjects.SQUARES).transform);
            playerDataMgr.UpdateSquares(-1);
        }
    }

    //This will verify if target position is available to spawn.
    private bool IsTargetPositionAvailable(){
        Vector2 position = new Vector2(transform.position.x, transform.position.y - colliderOffset);
        RaycastHit2D hit = Physics2D.BoxCast(position, spawnRange, 0.0f, Vector2.down,  0.0f);
        if (hit.collider == null) {
            return true;
        }
        return false;
    }
    
    private void OnDrawGizmos() {
        Vector2 position = new Vector2(transform.position.x, transform.position.y - colliderOffset);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(position, spawnRange);
    }
}
