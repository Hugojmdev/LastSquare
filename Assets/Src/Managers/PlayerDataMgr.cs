using UnityEngine;

public class PlayerDataMgr : DataMgr {
    
    public Player GetPlayer(){
        return GetData().player;
    }

    //Will update health amount based on the given +/- value
    public void UpdateHealth(int value) {
        Player player = GetPlayer();
        player.health += value;
        Save(new Data(player));
    }

    //Will update squares amount based on the given value(acceptable values= 0.5, 1)
    public void UpdateSquares(float value){
        Player player = GetPlayer();
        player.squares += value;
        Save(new Data(player));
    }

    //Verifies if the player is alive.
    public bool IsAlive() {
        int min = 0;
        int max = 6;
        int health = GetPlayer().health;
        return health > min && health <= max;
    }

}