using UnityEngine;

public class TriangleAHealthCtrl : MonoBehaviour {
    private int health = 3;

    // Update is called once per frame
    void Update() {
        //Verifies if it's still alive
        if(!IsAlive()) Die();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        //Verifies if it's hitting squares
        if(other.collider.CompareTag(Tag.DAMAGE_SQUARE) && IsAlive()) UpdateHealth(-1);
    }

    //Updates the triangleA health based on a given +/- value.
    private void UpdateHealth(int value){
        health += value;
    }

    //Verifies if triangle is alive
    private bool IsAlive() => health > 0;

    //Triggers die actions
    public void Die(){
        transform.gameObject.SetActive(false);
    }

}
