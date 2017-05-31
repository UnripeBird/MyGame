using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionModuleEffect : MonoBehaviour {
    
    public GameObject []g;
    int n;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void start()
    {
        for(n = 0;n<g.Length;n++)
        {
            ParticleSystem.EmissionModule pe = g[n].GetComponent<ParticleSystem>().emission;
            pe.enabled = true;
        }
    }

    public void stop()
    {
        for (n = 0; n < g.Length; n++)
        {
            ParticleSystem.EmissionModule pe = g[n].GetComponent<ParticleSystem>().emission;
            pe.enabled = false;
        }
    }

}
