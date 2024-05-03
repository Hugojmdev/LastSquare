using UnityEngine;

public class AbilityCtrl : MonoBehaviour {

    [Header("Square Ground")]
    [SerializeField]
    private GameObject squareGround;
    [SerializeField]
    private Transform squareGroundParent;
    [Header("Square Explosive")]
    [SerializeField]
    private GameObject squareExplosive;
    [SerializeField]
    private Transform squareExplosiveParent;
    
    [Header("Spawner")]
    [SerializeField] 
    private Vector2 spawnRange = new(0.97f,0.8f);

    [SerializeField]
    private float colliderOffset = 0.41f;

    //Class references
    private PlayerDataMgr playerDataMgr;
    private ShieldCtrl shieldCtrl;

    #region singleton
    public static AbilityCtrl Instance;
    void Awake() {
        //singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    void Start() {
        playerDataMgr = PlayerDataMgr.GetInstance();
        shieldCtrl = ShieldCtrl.Instance;
    }

    // Update is called once per frame
    void Update() {
        //Will spawn squares
        TriggerAbility();
    }

    //Will spawn a square depending on which button is pressed and square cost.
    private void TriggerAbility(){
        //Gets amount of squares from player data.
        int squareAmount = (int)playerDataMgr.GetPlayer().squares;
        //Spawns 'squareGround' if press 'J' and there are squares available, cost=1 square
        if (Input.GetKeyDown(KeyCode.J) && squareAmount >= 1) SpawnAbility(squareGround);
        //Spawns 'explosiveSquare' if press 'L' and there are squares available, cost=1 squares
        if (Input.GetKeyDown(KeyCode.L) && squareAmount >= 1) SpawnAbility(squareExplosive);
        //Launch the 'shield' ability if press 'K' and there are squares available, cost=2 square
        if (Input.GetKeyDown(KeyCode.K) && squareAmount >= 2) shieldCtrl.Launch();
    }

    //Spawns a square depending on what key was pressed down and square cost
     private void SpawnSquareAbility(GameObject squarePrefab){
        if(IsTargetPositionAvailable(squarePrefab.tag) && Tag.SQUARE_TAGS.Contains(squarePrefab.tag)) {
            //Get spawn position.
            Vector3 playerPosition = transform.position;
            Vector3 spawnPosition = new(playerPosition.x, playerPosition.y -0.5f, playerPosition.z); 
            //Instantiates a new consumable object to be spawned with the required components        
            GameObject square = Instantiate(squarePrefab, spawnPosition, Quaternion.identity);
            square.name = squarePrefab.name;
            //Set parent to the spawned square
            if (squarePrefab.CompareTag(Tag.SQUARE_GROUND)) square.transform.SetParent(squareGroundParent);
            if (squarePrefab.CompareTag(Tag.SQUARE_EXPLOSIVE)) square.transform.SetParent(squareExplosiveParent);
            playerDataMgr.UpdateSquares(-1);
        }
    }

    private void SpawnAbility(GameObject squarePrefab){
        //Get player position to define spawn position 
        Vector3 playerPosition = transform.position;
        //set default spawn position
        Vector3 spawnPosition = new(playerPosition.x, playerPosition.y -0.5f, playerPosition.z); 
        Vector3 direction = Vector3.down;
        Transform parent = null;
        if(squarePrefab.CompareTag(Tag.SQUARE_GROUND)){
            parent = squareGroundParent;
        }
        if(squarePrefab.CompareTag(Tag.SQUARE_EXPLOSIVE)){
            parent = squareExplosiveParent;
            spawnPosition = new(playerPosition.x, playerPosition.y + 1.4f, playerPosition.z);
            direction = Vector3.up;
        }
        if(IsSpawnPositionAvailable(direction)){
            //Instantiates a new consumable object to be spawned with the required components        
            GameObject square = Instantiate(squarePrefab, spawnPosition, Quaternion.identity);
            if(parent != null) square.transform.SetParent(parent);
            playerDataMgr.UpdateSquares(-1);
        }
    }

    //This will verify if target position is available to spawn.
    private bool IsTargetPositionAvailable(string ability){
        return !CastHelper.IsWithin2DBox(new(transform.position.x, transform.position.y - colliderOffset), spawnRange, 
        Vector3.down, Layers.GROUNDED_LAYERS);
    }
    private bool IsSpawnPositionAvailable(Vector3 direction){
        return !CastHelper.IsWithin2DBox(new(transform.position.x, transform.position.y + (direction == Vector3.down ? -colliderOffset : colliderOffset + 1)), 
        spawnRange, direction, Layers.ABILITY_SPAWNER_LAYERS);
    }

    private void OnDrawGizmos() {
        Vector2 downPos = new(transform.position.x, transform.position.y - colliderOffset);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(downPos, spawnRange);

        Vector2 upPos = new(transform.position.x, transform.position.y + colliderOffset + 1);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(upPos, spawnRange);
    }
}
