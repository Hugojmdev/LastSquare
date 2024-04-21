using UnityEngine;

public class PlayerHealthCtrl : MonoBehaviour {

    private PlayerDataMgr playerDataMgr;

    private void Start() {
        playerDataMgr = PlayerDataMgr.GetInstance();
    }

    // Update is called once per frame
    void Update() {
        //Will check if the player is alive, if not it will trigger die actions
        if(!playerDataMgr.IsAlive()) Die();
    }

    private void OnCollisionEnter2D(Collision2D other) {

        //Verify if player is still alive
        if(playerDataMgr.IsAlive()){
            //Will update player health when hits spikes
            if(other.gameObject.CompareTag(Tag.SPIKE)) playerDataMgr.UpdateHealth(-1);
            //Will update player health when hits enemy
            if(other.gameObject.CompareTag(Tag.ENEMY)) playerDataMgr.UpdateHealth(-1);

        }
    }

    //Triggers die actions.
    private void Die() {
        Debug.Log("Died!!!");
    }

    //Triggers restart actions.
    public void Restart() {
        Debug.Log("Restart!!!");
    }
}
