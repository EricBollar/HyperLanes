using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGrow : MonoBehaviour
{

    public float offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = new Vector3(this.transform.localScale.x + 0.005f*Mathf.Sin(4*Time.time + offset), this.transform.localScale.y + 0.005f*Mathf.Sin(4*Time.time + offset),
        											this.transform.localScale.z + 0.005f*Mathf.Sin(4*Time.time + offset));
    }
}
