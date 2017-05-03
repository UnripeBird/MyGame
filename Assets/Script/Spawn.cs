using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public GameObject obj;
    public int cnt = 10;
    public float delay = 2f;
    float t=0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (t == 0)
        {            
            t = Time.time + delay;
        }
        else if (Time.time >= t && cnt > 0)
        {
            GameObject g = Instantiate(obj);
            g.transform.parent = gameObject.transform;
            g.transform.localPosition = gameObject.transform.position;
            t = 0;
            cnt--;
        }
    }
}
