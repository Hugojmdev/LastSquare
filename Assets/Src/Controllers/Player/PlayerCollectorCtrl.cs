using UnityEngine;

public class PlayerCollectorCtrl : MonoBehaviour {

    private PlayerDataMgr playerDataMgr;

    private void Start() {
        playerDataMgr = PlayerDataMgr.GetInstance();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Executes square behavior
        if(other.gameObject.CompareTag(Tag.HALF_SQUARE)) {
            //Will deactivate half-square.
            other.gameObject.SetActive(false);
            playerDataMgr.UpdateSquares(0.5f);
        }
    }
}
