using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoScript : MonoBehaviour {

    public GameObject HPbar;

    info TowerInfo_Sc;

    public Text TowerName;
    public Text TowerAttactDamage;
    public Text TowerArmor;

    float maxHP;
    float nowHP;

    float HPbarRatio;

    // Use this for initialization
    void Start () {
        nowHP = maxHP = 100;
        TowerInfo_Sc = null;
    }

    // Update is called once per frame
    void Update() {
        //hp 최대 0 최소 -330
        if (TowerInfo_Sc != null)
        {
            if(!gameObject.activeSelf)
                gameObject.SetActive(true);
            maxHP = TowerInfo_Sc.GetMaxHp();
            nowHP = TowerInfo_Sc.GetHp();
            if (maxHP != 0)
            {
                HPbarRatio = -330 + (330 * (nowHP / maxHP));
            }
            Vector3 HP = HPbar.transform.localPosition;
            HP.x = HPbarRatio;
            HPbar.transform.localPosition = HP;
        }
        else
            gameObject.SetActive(false);
        
	}


    public void TarGetInfoInitailize(info _pInfo)
    {
        TowerInfo_Sc = _pInfo;
        if (TowerInfo_Sc != null)
        {
            TowerName.text = "Name : " + _pInfo.tname;
            TowerAttactDamage.text = "Atk : " + _pInfo.damage;
            TowerArmor.text = "Def : " + _pInfo.def;
        }
    }

}
