using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButtonScript : MonoBehaviour {

    int m_TowerIndex;
    public Button TowerButton;
    public Image TowerImage;
    public Image TowerInfo;
    public Image Towerprice;

    UIgm UIgm_cs;

    // Use this for initialization
    void Start () {
        UIgm_cs = GameObject.Find("Gm").GetComponent<UIgm>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TowerInitailize(int Index)
    {
        m_TowerIndex = Index;
        TowerButton.onClick = new Button.ButtonClickedEvent();
        TowerButton.onClick.AddListener(delegate { UIgm_cs.TowerListButtonClick(m_TowerIndex); });
    }
}
