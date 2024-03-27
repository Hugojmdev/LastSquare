using UnityEngine;

public class SquareHealthCtrl : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        Debug.Log("Health = " + DataManager.Load().square.health);

        //Will check if the square is alive, if not it will trigger die actions
        if(!IsAlive()) Die();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        //Will update square health when hit spikes and it's still alive
        if(other.gameObject.CompareTag(Tag.SPIKE) && IsAlive()) UpdateHealth(-1);
    }

    //Will update health with the given +/- value
    public void UpdateHealth(int value) {
        Square square = DataManager.Load().square;
        square.health += value;
        DataManager.Save(new Data(square));
    }

    //Verifies if the square is alive.
    public bool IsAlive() {
        int min = 0;
        int max = 6;
        int health = DataManager.Load().square.health;
        return health > min && health <= max;
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
