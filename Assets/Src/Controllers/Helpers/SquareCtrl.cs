using UnityEngine;

public class SquareCtrl : MonoBehaviour {

    [SerializeField]
    private GameObject squarePrefab;

    private PlayerDataMgr playerDataMgr;

    // Start is called before the first frame update
    void Start() {
        playerDataMgr = new PlayerDataMgr();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.J)) Spawn();
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

}
