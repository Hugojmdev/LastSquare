using UnityEngine;

public class PlayerHealthCtrl : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        Debug.Log("Health = " + DataManager.Load().player.health);

        //Will check if the player is alive, if not it will trigger die actions
        if(!IsAlive()) Die();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        //Will update player health when hit spikes and it's still alive
        if(other.gameObject.CompareTag(Tag.SPIKE) && IsAlive()) UpdateHealth(-1);
    }

    //Will update health with the given +/- value
    public void UpdateHealth(int value) {
        Player player = DataManager.Load().player;
        player.health += value;
        DataManager.Save(new Data(player));
    }

    //Verifies if the player is alive.
    public bool IsAlive() {
        int min = 0;
        int max = 6;
        int health = DataManager.Load().player.health;
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
