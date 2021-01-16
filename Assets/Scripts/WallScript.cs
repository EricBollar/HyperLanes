using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
	public float speed;
	private GameObject player;
	private bool scoreable = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.color = GameObject.FindGameObjectWithTag("spawner").GetComponent<WallSpawnScript>().mycol;
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (player.GetComponent<PlayerScript>().dead) {
        	speed = 0.2f;
        	//this.material.color = 0xffffff;
        }
        if (transform.position.z < 0 && scoreable) {
        	player.GetComponent<PlayerScript>().score++;
        	scoreable = false;
        }
        if (transform.position.z <= -5) {
        	Destroy(this.gameObject);
        }
    }
}
