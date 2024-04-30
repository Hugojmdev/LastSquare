using UnityEngine;

public class AbilityCtrl : MonoBehaviour {

    [SerializeField]
    private GameObject groundSquare;

    [SerializeField]
    private GameObject damageSquare;

    [SerializeField]
    private GameObject explosiveSquare;
    
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
        //Spawns 'groundSquare' if press 'J' and there are squares available, cost=1 square
        if (Input.GetKeyDown(KeyCode.J) && squareAmount >= 1) SpawnSquare(groundSquare);
        //Spawns 'explosiveSquare' if press 'L' and there are squares available, cost=1 squares
        if (Input.GetKeyDown(KeyCode.L) && squareAmount >= 1) SpawnSquare(explosiveSquare);
        //Launch the 'shield' ability if press 'K' and there are squares available, cost=2 square
        if (Input.GetKeyDown(KeyCode.K) && squareAmount >= 2) shieldCtrl.Launch();
    }

    //Spawns a square depending on what key was pressed down and square cost
     private void SpawnSquare(GameObject squarePrefab){
        if(IsTargetPositionAvailable() && Tag.SQUARE_TAGS.Contains(squarePrefab.tag)) {
            //Get spawn position.
            Vector3 playerPosition = transform.position;
            Vector3 spawnPosition = new(playerPosition.x, playerPosition.y -0.5f, playerPosition.z); 
            //Instantiates a new consumable object to be spawned with the required components        
            GameObject square = Instantiate(squarePrefab, spawnPosition, Quaternion.identity);
            square.name = squarePrefab.name;
            square.transform.SetParent(GameObject.Find(LevelObjects.SQUARES).transform);
            //Update squares amount depending on square type spawned
            if (squarePrefab.CompareTag(Tag.GROUND_SQUARE)) playerDataMgr.UpdateSquares(-1);
            if (squarePrefab.CompareTag(Tag.EXPLOSIVE_SQUARE)) playerDataMgr.UpdateSquares(-2);
        }
    }

    //This will verify if target position is available to spawn.
    private bool IsTargetPositionAvailable(){
        return !CastHelper.IsWithin2DBox(new(transform.position.x, transform.position.y - colliderOffset), spawnRange, 
        Vector3.down, Layers.GROUNDED_LAYERS);
    }

    private void OnDrawGizmos() {
        Vector2 position = new(transform.position.x, transform.position.y - colliderOffset);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(position, spawnRange);
    }
}
