using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScript : MonoBehaviour
{
	public float speed;
	private GameObject player;
	public bool changeColor = true;
	public Color mycol;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        this.gameObject.GetComponent<ParticleSystem>().startColor = new Color(mycol.r + 50/255f, mycol.g + 50/255f, mycol.b + 50/255f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (player.GetComponent<PlayerScript>().dead) {
        	speed = 0.2f;
        	//this.material.color = 0xffffff;
        }
        if (transform.position.z <= -5) {
        	Destroy(this.gameObject);
        }
        if (transform.position.z <= 0 && changeColor) {
        	changeColor = false;
        	GameObject.FindGameObjectWithTag("spawner").GetComponent<WallSpawnScript>().mycol = mycol;
        	Camera.main.backgroundColor = new Color(mycol.r - 150/255f, mycol.g - 150/255f, mycol.b - 150/255f, 1);
        }
    }
}
