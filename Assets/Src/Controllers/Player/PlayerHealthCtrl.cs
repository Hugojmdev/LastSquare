using UnityEngine;

public class PlayerHealthCtrl : MonoBehaviour {

    private PlayerDataMgr playerDataMgr;

    private void Start() {
        playerDataMgr = new PlayerDataMgr();
    }

    // Update is called once per frame
    void Update() {
        //Will check if the player is alive, if not it will trigger die actions
        if(!playerDataMgr.IsAlive()) Die();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        //Will update player health when hit spikes and it's still alive
        if(other.gameObject.CompareTag(Tag.SPIKE) && playerDataMgr.IsAlive()) playerDataMgr.UpdateHealth(-1);
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
