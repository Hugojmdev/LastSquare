using NUnit.Framework;
using UnityEngine;

public class SquareCtrl : MonoBehaviour {

    [SerializeField]
    private GameObject squarePrefab;
    
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
        //Debug.Log("Available to spawn: " + IsAvailableToSpawn());
        if(Input.GetKeyDown(KeyCode.J) && IsAvailableToSpawn()) Spawn();
    }

     private void Spawn(){
        int squares = (int)playerDataMgr.GetPlayer().squares;
        Debug.Log("Squares= " + squares);

        //Verify if there are squares available to be used.
        if(squares >= 1){
            //Get spawn position.
            Vector3 playerPosition = GetComponent<Transform>().position;
            Vector3 spawnPosition = new Vector3(playerPosition.x, playerPosition.y -0.5f, playerPosition.z); 
            //Instantiates a new consumable object to be spawned.        
            GameObject square = Instantiate(squarePrefab, spawnPosition, Quaternion.identity);
            square.name = squarePrefab.name;
            square.transform.SetParent(GameObject.Find(LevelObjects.SQUARES).transform);
            playerDataMgr.UpdateSquares(-1);
        }
    }


    //This will verify if it's available to spawn.
    private bool IsAvailableToSpawn(){
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
