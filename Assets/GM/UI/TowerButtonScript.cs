using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButtonScript : MonoBehaviour {

    int m_TowerIndex;
    public Button TowerButton;
    public Image TowerImage;
    public Text TowerAtk;
    public Text TowerDef;
    public Text TowerName;
    public Text TowerLife;
    public Text Towerprice;
    UIgm UIgm_cs;

    // Use this for initialization
    void Start () {
        UIgm_cs = GameObject.Find("Gm").GetComponent<UIgm>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TowerInitailize(info _tinfo)
    {
        m_TowerIndex = _tinfo.oinfo.index;
        TowerAtk.text = "Atk:" + _tinfo.oinfo.power;
        TowerDef.text = "Def:" + _tinfo.oinfo.def;
        TowerName.text = "Name :" + _tinfo.oinfo.name;
        TowerLife.text = "Life :" + _tinfo.oinfo.maxhp;
        Towerprice.text = "Price:";
        TowerButton.onClick = new Button.ButtonClickedEvent();
        TowerButton.onClick.AddListener(delegate { UIgm_cs.TowerListButtonClick(m_TowerIndex); });
    }
}
