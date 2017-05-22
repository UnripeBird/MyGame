using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {

    Text txt;
    bool Errorbool;
    float Timer;

	// Use this for initialization
	void Start () {
        txt = this.GetComponent<Text>();
        Errorbool = true;
        Timer = Time.time;

    }
	
	// Update is called once per frame
	void Update () {

        Color col = txt.color;
        if (Errorbool == true && col.a < 1.0f)
        {
            col.a += 1.0f * Time.deltaTime;
        }
        else if (Errorbool == false && col.a > 0.0f && Time.time - Timer > 3.0f)
        {
            col.a -= 1.0f * Time.deltaTime;
        }
        else
        {
            if (col.a < 0)
            {
                this.gameObject.SetActive(false);
            }
            Errorbool = false;

        }
        txt.color = col;
    }
}
