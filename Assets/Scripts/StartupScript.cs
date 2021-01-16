using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartupScript : MonoBehaviour
{
	public GameObject scoreText;
	public GameObject playButton;
	public GameObject left;
	// public GameObject middle;
	public GameObject right;
    public GameObject restart;
    public GameObject title;
    public GameObject by;
    public GameObject howto;
    public GameObject shopButton;
	float slide = 2;
    bool start = true;
    float howtotime = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        shopButton.transform.localPosition = new Vector3(0, -550, 0);
        Image[] imgs = howto.GetComponentsInChildren<Image>();
        for (int i = 0; i < imgs.Length; i++) {
        	imgs[i].color = new Color(imgs[i].color.r, imgs[i].color.g, imgs[i].color.b, 0);
        }
	    Text[] ts = howto.GetComponentsInChildren<Text>();
	    for (int i = 0; i < ts.Length; i++) {
	    	ts[i].color = new Color(ts[i].color.r, ts[i].color.g, ts[i].color.b, 0);
	    }
        title.transform.localPosition = new Vector3(0, 285, 0);
        by.transform.localPosition = new Vector3(0, 185, 0);
        restart.transform.localPosition = new Vector3(1000, 0, 0);
        Camera.main.transform.position = new Vector3(0, 4, -5);
        scoreText.transform.localPosition = new Vector3(0, 600, 0);
        left.transform.localPosition = new Vector3(-185, -850, 0);
        // middle.transform.localPosition = new Vector3(0, -800, 0);
        right.transform.localPosition = new Vector3(185, -850, 0);
        // playButton.transform.position = new Vector2(0, 75);
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.transform.position.y > 2.55) {
        	Camera.main.transform.Translate(Vector3.down * slide * Time.deltaTime);
        } else {
        	// Vector3 temp = new Vector3(0, 3, -5);
        	// Camera.main.transform.position = temp;       
        }
        if (this.gameObject.GetComponent<PlayerScript>().playing) {
            shopButton.transform.localPosition = new Vector3(0, 2000, 0);
            if (howto.GetComponentInChildren<Image>().color.a < 150f/255 && howtotime > 0f) {
		        Image[] imgs = howto.GetComponentsInChildren<Image>();
		        for (int i = 0; i < imgs.Length; i++) {
		        	imgs[i].color = new Color(imgs[i].color.r, imgs[i].color.g, imgs[i].color.b, imgs[i].color.a+Time.deltaTime);
		        }
		        Text[] ts = howto.GetComponentsInChildren<Text>();
		        for (int i = 0; i < ts.Length; i++) {
		        	ts[i].color = new Color(ts[i].color.r, ts[i].color.g, ts[i].color.b, ts[i].color.a+Time.deltaTime);
		        }
            } else { 
                if (howtotime < 0f) {
			        Image[] imgs = howto.GetComponentsInChildren<Image>();
			        for (int i = 0; i < imgs.Length; i++) {
			        	imgs[i].color = new Color(imgs[i].color.r, imgs[i].color.g, imgs[i].color.b, imgs[i].color.a-Time.deltaTime);
			        }
			        Text[] ts = howto.GetComponentsInChildren<Text>();
			        for (int i = 0; i < ts.Length; i++) {
			        	ts[i].color = new Color(ts[i].color.r, ts[i].color.g, ts[i].color.b, ts[i].color.a-Time.deltaTime);
			        }
                } else {
                    howtotime -= Time.deltaTime;
                }
            }
        }
        if (start && this.gameObject.GetComponent<PlayerScript>().playing) {
            start = false;
            slide = 2f;
        }
        if (this.gameObject.GetComponent<PlayerScript>().playing && Camera.main.fieldOfView < 59) {
            Camera.main.GetComponent<Camera>().fieldOfView += 50*slide*Time.deltaTime;
        } else if (this.gameObject.GetComponent<PlayerScript>().playing)  {
            Camera.main.GetComponent<Camera>().fieldOfView = 60;
            Camera.main.transform.Translate(Vector3.up*Mathf.PerlinNoise(Time.time*5, Time.time*5) *Time.deltaTime);
        }
        if (scoreText.transform.localPosition.y > 550) {
        	scoreText.transform.localPosition = new Vector3(0, scoreText.transform.localPosition.y - 30*slide*Time.deltaTime, 0);
        }
        if (this.gameObject.GetComponent<PlayerScript>().playing && left.transform.localPosition.y < -576) {
        	left.transform.localPosition = new Vector3(-185, left.transform.localPosition.y + 500*Time.deltaTime, 0);
        } else if (this.gameObject.GetComponent<PlayerScript>().playing) {
        	left.transform.localPosition = new Vector3(-185, -575, 0);
        }
        if (this.gameObject.GetComponent<PlayerScript>().playing && title.transform.localPosition.x > -999) {
            title.transform.localPosition = new Vector3(title.transform.localPosition.x - 1000*Time.deltaTime, 285, 0);
        }
        if (this.gameObject.GetComponent<PlayerScript>().playing && by.transform.localPosition.x < 999) {
            by.transform.localPosition = new Vector3(by.transform.localPosition.x + 1000*Time.deltaTime, 185, 0);
        }
        // if (this.gameObject.GetComponent<PlayerScript>().playing && middle.transform.localPosition.y < -576) {
        // 	middle.transform.localPosition = new Vector3(0, middle.transform.localPosition.y + 500*Time.deltaTime, 0);
        // } else if (this.gameObject.GetComponent<PlayerScript>().playing) {
        // 	middle.transform.localPosition = new Vector3(0, -575, 0);
        // }
        if (this.gameObject.GetComponent<PlayerScript>().playing && right.transform.localPosition.y < -576) {
        	right.transform.localPosition = new Vector3(185, right.transform.localPosition.y + 500*Time.deltaTime, 0);
        } else if (this.gameObject.GetComponent<PlayerScript>().playing) {
        	right.transform.localPosition = new Vector3(185, -575, 0);
        }
        if (this.gameObject.GetComponent<PlayerScript>().dead && restart.transform.localPosition.x > 1) {
            restart.transform.localPosition = new Vector3(restart.transform.localPosition.x - 800*Time.deltaTime, 0, 0);
        } else if (this.gameObject.GetComponent<PlayerScript>().dead) {
            restart.transform.localPosition = new Vector3(0, 0, 0);
        }
        if (slide > 0.5f) {
        	slide -= Time.deltaTime;
        }
    }
}
