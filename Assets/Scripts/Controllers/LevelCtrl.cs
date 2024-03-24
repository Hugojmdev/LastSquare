using UnityEngine;

public class LevelCtrl : MonoBehaviour {

    private LEVEL_STATE levelState;
    public LEVEL_STATE GetlevelState() => levelState;
    public void SetLevelState(LEVEL_STATE levelState){
        this.levelState = levelState;
    } 
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update(){
        EvaluateLevelState();
    }

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
