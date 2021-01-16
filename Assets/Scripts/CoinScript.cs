using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
	public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	if (GameObject.FindGameObjectWithTag("player").GetComponent<PlayerScript>().dead) {
        	speed = 0.2f;
        }
        if (transform.position.z > -5) {
        	transform.Translate(Vector3.back * speed * Time.deltaTime);
        } else {
        	Destroy(this.gameObject);
        }
    }
}
