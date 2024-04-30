using UnityEngine;

public class LevelCtrl : MonoBehaviour {

    #region Managers
    private PlayerDataMgr playerDataMgr;
    #endregion

    private LEVEL_STATE levelState;
    public LEVEL_STATE GetlevelState() => levelState;
    public void SetLevelState(LEVEL_STATE levelState){
        this.levelState = levelState;
    } 
    
    // Start is called before the first frame update
    void Start() {
        playerDataMgr = PlayerDataMgr.GetInstance();
        //TODO temporary solution, needs to be updated because it's updating to default values everytime game starts.
        playerDataMgr.Save(new Data(new Player()));
    }

    // Update is called once per frame
    // void Update(){
    //     string playerData = JsonUtility.ToJson(playerDataMgr.GetPlayer());
    //     Debug.Log("Player Data = " + playerData);

    //     //EvaluateLevelState();
    // }

    public void EvaluateLevelState(){
        switch(levelState){
            case LEVEL_STATE.PLAYING:
            break;
            case LEVEL_STATE.GAME_OVER:
            break;
            case LEVEL_STATE.PAUSE:
            break;
            case LEVEL_STATE.MENU:
            break;

        }
    }
}
