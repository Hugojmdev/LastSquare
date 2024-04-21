using UnityEngine;

public class HalfSquareCtrl : MonoBehaviour {

    [SerializeField]
    private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start() {
        // Get the SpriteRenderer component attached to the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
        //Will set sprite randomly
        SetSprite();
        
    }
    
    //Will set the sprite randomly based on a list of sprties
    private void SetSprite(){
        // Select a random sprite from the array and assign it to the SpriteRenderer
        if (sprites != null && sprites.Length > 0) {
            int randomIndex = Random.Range(0, sprites.Length);
            spriteRenderer.sprite = sprites[randomIndex];
        }
        else {
            Debug.LogError("Sprite array is null or empty!");
        }
    }
}
