using TMPro;
using UnityEngine;

public class UICtrl : MonoBehaviour {

    [SerializeField]
    private Canvas canvas;

    private TextMeshProUGUI health;
    private TextMeshProUGUI squares;

    private PlayerDataMgr playerDataMgr;

    private void Start() {
        playerDataMgr = PlayerDataMgr.GetInstance();
        health = canvas.transform.Find(LevelObjects.UI_HEALTH).GetComponent<TextMeshProUGUI>();
        squares = canvas.transform.Find(LevelObjects.UI_SQUARES).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {
        health.text = "Health: " + playerDataMgr.GetPlayer().health;
        squares.text = "Squares: " + playerDataMgr.GetPlayer().squares;
    }

}
