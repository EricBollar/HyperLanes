using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {
    public bool redPurchased;
    public bool greenPurchased;
    public bool purplePurchased;
    public bool crownPurchased;
    public bool cowboyPurchased;
    public bool vikingPurchased;
    public bool tophatPurchased;
    public int coins;
    public int highScore;
    public int equipped;
    public int hatEquipped;

    public GameData() {
    	PlayerScript p = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerScript>();
    	redPurchased = p.redPurchased;
    	coins = p.coins;
    	highScore = p.highScore;
    	equipped = p.equipped;
    	greenPurchased = p.greenPurchased;
    	purplePurchased = p.purplePurchased;
        crownPurchased = p.crownPurchased;
        hatEquipped = p.hatEquipped;
        cowboyPurchased = p.cowboyPurchased;
        vikingPurchased = p.vikingPurchased;
        tophatPurchased = p.tophatPurchased;
    }
}
