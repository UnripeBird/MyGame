using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
            //Debug.Log(transform.position+ ""+ transform.localPosition );
            GameObject g = Instantiate(obj);            
            g.transform.parent = gameObject.transform;
            //g.transform.localPosition = gameObject.transform.localPosition;
            g.transform.localPosition = Vector3.zero;
            g.GetComponent<NavMeshAgent>().enabled = true;
            g.GetComponent<Monster>().enabled =  true;
            
            
            t = 0;
            cnt--;
        }
    }
}
