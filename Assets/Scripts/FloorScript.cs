using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
	public float speed;
	private GameObject player;
	public Color mycol;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
    	if (player.GetComponent<PlayerScript>().playing) {
    		speed = GameObject.FindGameObjectWithTag("spawner").GetComponent<WallSpawnScript>().speed;
    		float move = -speed * Time.time / 250f;
    		while (move <= -0.5f) {
    			move += 0.5f; 
    		}
    		Color cs = GameObject.FindGameObjectWithTag("spawner").GetComponent<WallSpawnScript>().mycol;
	    	GetComponent<Renderer>().material.color = new Color(cs.r - 150f/255, cs.g - 150f/255, cs.b - 150f/255);
	    }
	    
    		Color c = GameObject.FindGameObjectWithTag("spawner").GetComponent<WallSpawnScript>().mycol;
	    	GetComponent<Renderer>().material.color = new Color(c.r - 150f/255, c.g - 150f/255, c.b - 150f/255);
    }
}
