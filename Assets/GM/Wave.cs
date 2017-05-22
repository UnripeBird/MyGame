using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
[Serializable]
public struct wave_object
{    
    public GameObject g;
    public int cnt;
}

[Serializable]
public struct wave_struct
{
    public wave_object wo;
    public int cnt;
}


public class Wave : MonoBehaviour {

    public wave_struct[] ws;
    //public wave_object[] wo;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
