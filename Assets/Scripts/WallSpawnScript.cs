using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawnScript : MonoBehaviour
{
	private float timeLeft;
	public GameObject wall;
    private int r;
    public float speed = 30f;
    private float timeAmount;
    public GameObject player;
    public GameObject particles;
    public GameObject floor;
    public GameObject coin;
    public Color mycol;
    public bool newFloor;
    int count = 0;
    Color[] cols = {Color.red, Color.green, Color.blue, Color.cyan, Color.magenta, Color.yellow};

    // Start is called before the first frame update
    void Start()
    {
        newFloor = true;
        mycol = Color.red;
        timeAmount = 1f;
        timeLeft = 1f;
        r = 1;
        player = GameObject.FindGameObjectWithTag("player");


        GameObject m1 = Instantiate(particles, new Vector3(-2.5f, 0, -4), Quaternion.identity);
        m1.GetComponent<RoadScript>().speed = speed;
        m1.GetComponent<RoadScript>().mycol = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerScript>().playing) {
            if (speed < 100f) {
                speed += 0.4f*Time.deltaTime;
            }
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0) {
                if (timeAmount > 0.2f) {
                    timeAmount -= 0.4f*Time.deltaTime;
                }
            	timeLeft = timeAmount;
                int temp = Random.Range(-1, 2);
                if (temp == r) {
                    temp = Random.Range(-1, 2);
                }
                if (temp == 2) {
                    temp = 1;
                }
            	GameObject w = Instantiate(wall, new Vector3(temp, 0, 201), Quaternion.identity);
                w.GetComponent<WallScript>().speed = speed;
                count++;

                if (count % 10 == 0) {
                    int t = Random.Range(-1, 2);
                    while (t == temp) {
                        t = Random.Range(-1, 2);
                    }
                    GameObject l = Instantiate(coin, new Vector3(t, 0, 201), Quaternion.identity);
                    l.GetComponent<CoinScript>().speed = speed;
                }

                if (count % 50 == 0) {
                    Color ranCol = cols[Random.Range(0, 6)];
                    while (ranCol == mycol) {
                        ranCol = cols[Random.Range(0, 6)];
                    }
	            	GameObject m1 = Instantiate(particles, new Vector3(-2.5f, -2, 201), Quaternion.identity);
	                m1.GetComponent<RoadScript>().speed = speed;
                    m1.GetComponent<RoadScript>().mycol = ranCol;
	            	GameObject m2 = Instantiate(particles, new Vector3(2.5f, -2, 201), Quaternion.identity);
	                m2.GetComponent<RoadScript>().speed = speed;
                    m2.GetComponent<RoadScript>().mycol = m1.GetComponent<RoadScript>().mycol;
                }


                r = temp;
            }
        }
    }
}
