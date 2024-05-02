
using System.Collections;
using UnityEngine;

public class SquareGroundCtrl : MonoBehaviour {

    #region sprites
    [SerializeField]
    private Sprite initialSprite;
    [SerializeField]
    private Sprite steppedSprite;
    #endregion

    #region Components
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    #endregion

    private PlayerDataMgr playerDataMgr;

    private int steps = 0;

    // Start is called before the first frame update
    void Start() {
        playerDataMgr = PlayerDataMgr.GetInstance();
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        UpdateColliderType(IsPlayerBelow());
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag(Tag.PLAYER)) {
            StepOn();
            if(steps>=3) animator.SetTrigger("Shake");
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag(Tag.PLAYER) && steps==3) StartCoroutine(Dissapear());
    }

    private void UpdateColliderType(bool isTrigger){
        boxCollider2D.isTrigger = isTrigger;
    }

    //Verifies if player is below or above the square ground
    private bool IsPlayerBelow(){
        Vector3 playerPosition = playerDataMgr.GetPlayer().position.ToVector3();
        return playerPosition.y < transform.position.y + 0.2f;
    }

    //Executes behavior depending on the steps count
    private void StepOn(){
        if(steps == 0){
            steps++;
            return;
        }

        if(steps == 1){
            steps++;
            return;
        }

        if(steps == 2){
            steps++;
            spriteRenderer.sprite = steppedSprite;
            return;
        }
    }

    private IEnumerator Dissapear(){
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
