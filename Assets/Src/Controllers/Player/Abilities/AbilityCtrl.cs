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
    private DamageSquareCtrl damageSquareCtrl;

    #region singleton and initialization
    public static AbilityCtrl Instance;
    void Awake() {
        playerDataMgr = PlayerDataMgr.GetInstance();
        damageSquareCtrl = DamageSquareCtrl.Instance;

        //singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

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
        //Spawns 'damageSquare' if press 'K' and there are squares available, cost=1 square
        if (Input.GetKeyDown(KeyCode.K) && squareAmount >= 1) damageSquareCtrl.Enable();
        //Spawns 'explosiveSquare' if press 'L' and there are squares available, cost=2 squares
        if (Input.GetKeyDown(KeyCode.L) && squareAmount >= 2) SpawnSquare(explosiveSquare);
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
        Vector2 position = new(transform.position.x, transform.position.y - colliderOffset);
        RaycastHit2D hit = Physics2D.BoxCast(position, spawnRange, 0.0f, Vector2.down, 0.0f);
        if (hit.collider == null) {
            return true;
        }
        return false;
    }
    
    private void OnDrawGizmos() {
        Vector2 position = new(transform.position.x, transform.position.y - colliderOffset);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(position, spawnRange);
    }
}
