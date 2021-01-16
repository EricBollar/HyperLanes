using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
	public bool dead;
	public bool playing;
	public int score = 0;
	public Text scoreText;
	public Button playButton;
	public float deadTime = 2f;
    public float buttonRDeadTime = 0.1f;
    public float buttonLDeadTime = 0.1f;
    public GameObject bloodParticles;
    public GameObject teleportParticles;
    public Material greenCol;
    public GameObject jetPackL;
    public GameObject jetPackR;
    public Text bestText;
    public int coins;
    public Text coinText;
    public GameObject shop;
    public bool redPurchased;
    public bool greenPurchased;
    public bool purplePurchased;
    public bool crownPurchased;
    public bool cowboyPurchased;
    public bool vikingPurchased;
    public bool tophatPurchased;
    public int equipped;
    public int hatEquipped;
    public int highScore;
    int prev;

    // Start is called before the first frame update
    void Start()
    {
        GameData data = SaveSystem.Load();
        if (data == null) {
            redPurchased = false;
            greenPurchased = false;
            purplePurchased = false;
            crownPurchased = false;
            cowboyPurchased = false;
            vikingPurchased = false;
            tophatPurchased = false;
            coins = 0;
            highScore = 0;
            equipped = 0;
            hatEquipped = 0;
            SaveSystem.Save();
        } else {
            redPurchased = data.redPurchased;
            greenPurchased = data.greenPurchased;
            purplePurchased = data.purplePurchased;
            coins = data.coins;
            highScore = data.highScore;
            equipped = data.equipped;
            crownPurchased = data.crownPurchased;
            hatEquipped = data.hatEquipped;
            cowboyPurchased = data.cowboyPurchased;
            vikingPurchased = data.vikingPurchased;
            tophatPurchased = data.tophatPurchased;
        }
        dead = false;
        playing = false;
    }

    // Update is called once per frame
    void Update()
    {
    	GetComponent<AudioSource>().pitch = 1 + 0.015f * (GameObject.FindGameObjectWithTag("spawner").GetComponent<WallSpawnScript>().speed - 30);
    	if (playing) {
	        handleMovement();
            scoreText.text = score.ToString();
            if (score > highScore) {
                highScore = score;
            }
		}
		if (dead) {
            score--;
            SaveSystem.Save();
		}
        bestText.text = "Best: " + highScore.ToString();
        coinText.text = coins.ToString();
        switch (equipped) {
            case 0: GetComponent<Renderer>().material.color = Color.blue; break;
            case 1: GetComponent<Renderer>().material.color = Color.red; break;
            case 2: GetComponent<Renderer>().material.color = Color.green; break;
            case 3: GetComponent<Renderer>().material.color = Color.magenta; break;
            default: GetComponent<Renderer>().material.color = Color.blue; break;
        }
        handleHats();
        handleShop();
    }

    public void handleHats() {
        GameObject crown = GameObject.FindGameObjectWithTag("crown");
        crown.GetComponent<Renderer>().enabled = false;
        GameObject cowboy = GameObject.FindGameObjectWithTag("cowboy");
        cowboy.GetComponent<Renderer>().enabled = false;
        GameObject viking = GameObject.FindGameObjectWithTag("viking");
        viking.GetComponent<Renderer>().enabled = false;
        GameObject tophat = GameObject.FindGameObjectWithTag("tophat");
        tophat.GetComponent<Renderer>().enabled = false;
        switch (hatEquipped) {
            case 0: break;
            case 1: crown.GetComponent<Renderer>().enabled = true; break;
            case 2: cowboy.GetComponent<Renderer>().enabled = true; break;
            case 3: viking.GetComponent<Renderer>().enabled = true; break;
            case 4: tophat.GetComponent<Renderer>().enabled = true; break;
        }
    }

    public void restart() {
        SceneManager.LoadScene("SampleScene");
    }

    void handleMovement() {
    	if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            left();
    	} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            right();
    	}
    	if (Input.touchCount > 0) {
    		Touch t = Input.GetTouch(Input.touchCount-1);
    		if (t.phase == TouchPhase.Began) {
	    		if (t.position.x > Screen.width/2) {
	    			right();
	    		} else {
	    			left();
	    		}
	    	}
	    }
    }

    private void OnTriggerEnter(Collider other) {
    	if (other.gameObject.tag == "bad") {
    		dead = true;
    		playing = false;
            Instantiate(bloodParticles, this.transform.position, Quaternion.identity);
    		this.GetComponent<Renderer>().material = greenCol;
    	} else if (other.gameObject.tag == "coin" && !dead) {
            coins++;
            Destroy(other.gameObject);
        }
    }

    public void playGame() {
    	playing = true;
    	playButton.GetComponent<Image>().enabled = false;
        jetPackL.transform.position = new Vector3(this.transform.position.x - 0.35f, 0, 0);
        jetPackR.transform.position = new Vector3(this.transform.position.x + 0.35f, 0, 0);
    }

    public void left() {
        if (transform.position.x != -1 && !dead) {
            Instantiate(teleportParticles, transform.position, Quaternion.identity);
            transform.Translate(Vector3.left);
	        jetPackL.transform.position = new Vector3(this.transform.position.x - 0.35f, 0, 0);
	        jetPackR.transform.position = new Vector3(this.transform.position.x + 0.35f, 0, 0);
        }
    }

    public void right() {
    	if (transform.position.x != 1 && !dead) {
            Instantiate(teleportParticles, transform.position, Quaternion.identity);
            transform.Translate(Vector3.right);
	        jetPackL.transform.position = new Vector3(this.transform.position.x - 0.35f, 0, 0);
	        jetPackR.transform.position = new Vector3(this.transform.position.x + 0.35f, 0, 0);
        }
    }

    public void openShop() {
    	if (shop.transform.localPosition.x > 0) {
        	shop.transform.localPosition = new Vector3(0, 0, 0);
    	} else {
    		closeShop();
    	}
    }

    public void closeShop() {
        shop.transform.localPosition = new Vector3(1000, 0, 0);
    }

    public void website() {
        Application.OpenURL("https://www.ericbollar.com");
    }

    public void handleShopItem(string s, bool isPurchased, int equipValue, int price) {
        GameObject b = GameObject.FindGameObjectWithTag(s);
        if (!isPurchased) {
            b.GetComponent<Image>().color = Color.red;
            Image[] imgs = b.GetComponentsInChildren<Image>();
            for (int i = 1; i < imgs.Length; i++) {
                imgs[i].enabled = true;
            }
            b.GetComponentInChildren<Text>().text = price.ToString() + " ";
        } else if (equipped == equipValue) {
            b.GetComponent<Image>().color = Color.gray;
            Image[] imgs = b.GetComponentsInChildren<Image>();
            for (int i = 1; i < imgs.Length; i++) {
                imgs[i].enabled = false;
            }
            b.GetComponentInChildren<Text>().text = "Equipped";

        } else {
            b.GetComponent<Image>().color = Color.blue;
            b.GetComponentInChildren<Text>().text = "Equip";
            Image[] imgs = b.GetComponentsInChildren<Image>();
            for (int i = 1; i < imgs.Length; i++) {
                imgs[i].enabled = false;
            }
        }
    }

    public void handleHatItem(string s, bool isPurchased, int equipValue, int price) {
        GameObject b = GameObject.FindGameObjectWithTag(s);
        if (!isPurchased) {
            b.GetComponent<Image>().color = Color.red;
            Image[] imgs = b.GetComponentsInChildren<Image>();
            for (int i = 1; i < imgs.Length; i++) {
                imgs[i].enabled = true;
            }
            b.GetComponentInChildren<Text>().text = price.ToString() + "  ";
        } else if (hatEquipped == equipValue) {
            b.GetComponent<Image>().color = Color.gray;
            Image[] imgs = b.GetComponentsInChildren<Image>();
            for (int i = 1; i < imgs.Length; i++) {
                imgs[i].enabled = false;
            }
            b.GetComponentInChildren<Text>().text = "Equipped";

        } else {
            b.GetComponent<Image>().color = Color.blue;
            b.GetComponentInChildren<Text>().text = "Equip";
            Image[] imgs = b.GetComponentsInChildren<Image>();
            for (int i = 1; i < imgs.Length; i++) {
                imgs[i].enabled = false;
            }
        }
    }

    public void handleShop() {
        handleShopItem("bluePurchaseButton", true, 0, 0);
        handleShopItem("redPurchaseButton", redPurchased, 1, 25);
        handleShopItem("greenPurchaseButton", greenPurchased, 2, 25);
        handleShopItem("purplePurchaseButton", purplePurchased, 3, 25);
        handleHatItem("crownPurchaseButton", crownPurchased, 1, 999);
        handleHatItem("cowboyPurchaseButton", cowboyPurchased, 2, 200);
        handleHatItem("nohatPurchaseButton", true, 0, 2);
        handleHatItem("vikingPurchaseButton", vikingPurchased, 3, 200);
        handleHatItem("tophatPurchaseButton", tophatPurchased, 4, 200);
    }

    public void clickRed() {
        if (!redPurchased) {
            if (coins >= 25) {
                coins -= 25;
                redPurchased = true;
                equipped = 1;
            }
        } else {
            equipped = 1;
        }
        SaveSystem.Save();
    }

    public void clickGreen() {
        if (!greenPurchased) {
            if (coins >= 25) {
                coins -= 25;
                greenPurchased = true;
                equipped = 2;
            }
        } else {
            equipped = 2;
        }
        SaveSystem.Save();
    }

    public void clickPurple() {
        if (!purplePurchased) {
            if (coins >= 25) {
                coins -= 25;
                purplePurchased = true;
                equipped = 3;
            }
        } else {
            equipped = 3;
        }
        SaveSystem.Save();
    }

    public void clickBlue() {
        equipped = 0;
        SaveSystem.Save();
    }

    public void clickReset() {
        redPurchased = false;
        greenPurchased = false;
        purplePurchased = false;
        crownPurchased = false;
        cowboyPurchased = false;
        vikingPurchased = false;
        tophatPurchased = false;
        equipped = 0;
        hatEquipped = 0;
        SaveSystem.Save();
    }

    public void clickCrown() {
        if (!crownPurchased) {
            if (coins >= 999) {
                coins -= 999;
                crownPurchased = true;
                hatEquipped = 1;
            }
        } else {
            hatEquipped = 1;
        }
        SaveSystem.Save();
    }

    public void clickCowboy() {
        if (!cowboyPurchased) {
            if (coins >= 200) {
                coins -= 200;
                cowboyPurchased = true;
                hatEquipped = 2;
            }
        } else {
            hatEquipped = 2;
        }
        SaveSystem.Save();
    }

    public void clickViking() {
        if (!vikingPurchased) {
            if (coins >= 200) {
                coins -= 200;
                vikingPurchased = true;
                hatEquipped = 3;
            }
        } else {
            hatEquipped = 3;
        }
        SaveSystem.Save();
    }

    public void clickTopHat() {
        if (!tophatPurchased) {
            if (coins >= 200) {
                coins -= 200;
                tophatPurchased = true;
                hatEquipped = 4;
            }
        } else {
            hatEquipped = 4;
        }
        SaveSystem.Save();
    }

    public void clickNoHat() {
        hatEquipped = 0;
        SaveSystem.Save();
    }
}
