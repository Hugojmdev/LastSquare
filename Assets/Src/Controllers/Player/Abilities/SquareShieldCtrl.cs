using UnityEngine;

public class SquareShieldCtrl : MonoBehaviour {

    [SerializeField]
    private Sprite squareShieldSprite;
    [SerializeField]
    private Sprite brokenSquareShieldSprite;
    [SerializeField]
    private PhysicsMaterial2D material2D;
    private bool isBroken = false;

    private void OnEnable() {
        UpdateSprite(squareShieldSprite);
        isBroken = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.CompareTag(Tag.ENEMY)) BreakShield();
    }

    private void BreakShield(){
        if(!isBroken) { 
            UpdateSprite(brokenSquareShieldSprite);
            isBroken = true;
            return;
        }
        if(isBroken) {
            UpdateSprite(squareShieldSprite);
            gameObject.SetActive(false);
            isBroken = false;
        } 
    }

    private void UpdateSprite(Sprite sprite){
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //Changes the sprite for a new one
        spriteRenderer.sprite = sprite;
    }
}
