using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonScript : MonoBehaviour
{
	public int price;
	int coins;
	bool purchased;

    // Start is called before the first frame update
    void Start()
    {
        purchased = false;
    }

    // Update is called once per frame
    void Update()
    {
        coins = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerScript>().coins;
        if (!purchased) {
	        if (coins >= price) {
	        	GetComponent<Image>().color = new Color(255f/255, 100f/255, 100f/255);
	        } else {
	        	GetComponent<Image>().color = new Color(169f/255, 169f/255, 169f/255);
	        }
	    } else {
	    	GetComponent<Image>().color = new Color(100f/255, 255f/255, 100f/255);
	    	GetComponentInChildren<Text>().text = "Equip";
	    }
    }

    public void purchaseRed() {
    	if (coins >= 1) {
    		GameObject.FindGameObjectWithTag("player").GetComponent<PlayerScript>().coins -= 50;
    	}
    }
}
