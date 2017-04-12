using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIgm : MonoBehaviour {

    List<GameObject> Error_List;

    Color[] ErrorCol;

    public GameObject TowerButton;
    public GameObject TowerUnitMenu;
    public GameObject TowerInfo;
    public GameObject TowerMenuList;
    public GameObject UnitMenuList;
    public GameObject TowerMenuButton;
    public GameObject UnitMenuButton;
    public GameObject PlayMenu;
    public GameObject ErrorParant;
    public GameObject ErrorText;

    public Transform TowerContent;
    public Transform UnitContent;

    public Text ResourcesText;

    Gm Gm_sc;

    bool TowerUnitMenubool;
    bool TowerUnitMenuSwitch;       //true : Tower , false : Unit

    private float timeScale;
    private float ErrorTime;

    int ResourcesInt;
    int NowResoures;
    int ErrorCount;

    // Use this for initialization
    void Start () {
        Gm_sc = Gm.getgm();

        Error_List = new List<GameObject>();

        ErrorCol = new Color[3];
        ErrorCol[0] = Color.red;
        ErrorCol[1] = Color.yellow;
        ErrorCol[2] = Color.green;

        info _info = new info();
        _info.index = 2000;
        _info.InfoSet();

        AddTowerMenu(Gm_sc.list.tower[0].GetComponent<info>());
        AddTowerMenu(Gm_sc.list.tower[1].GetComponent<info>());
        AddTowerMenu(_info);

        TowerUnitMenubool = false;
        TowerUnitMenuSwitch = true;
        UnitMenuList.SetActive(false);

        NowResoures =  ResourcesInt = 0;

        ErrorCount = 0;

        ErrorTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(TowerUnitMenu.GetComponent<RectTransform>().localPosition);
        Vector3 UIpo = TowerUnitMenu.GetComponent<RectTransform>().localPosition;

        if (TowerUnitMenubool == true && UIpo.y < -300.0f)  //비활성 -555  활성 -300
        {
            UIpo.y += 600.0f * Time.deltaTime;

            TowerUnitMenu.GetComponent<RectTransform>().localPosition = UIpo;
        }
        else if(TowerUnitMenubool == false && UIpo.y > -555.0f)
        {
            UIpo.y -= 600.0f * Time.deltaTime;

            TowerUnitMenu.GetComponent<RectTransform>().localPosition = UIpo;
            TowerMenuButton.GetComponent<Image>().color = new Color(255, 255, 255);
            UnitMenuButton.GetComponent<Image>().color = new Color(255, 255, 255);
        }
        else if(UIpo.y > -300.0f)
        {
            UIpo.y = -300.0f;
            TowerUnitMenu.GetComponent<RectTransform>().localPosition = UIpo;
        }
        else if (UIpo.y < -555.0f)
        {
            UIpo.y = -555.0f;
            TowerUnitMenu.GetComponent<RectTransform>().localPosition = UIpo;
        }

        if(NowResoures != ResourcesInt)
        {
            int Val;
            Val = Gm_sc.boo(ResourcesInt - NowResoures);

            NowResoures += Val * 7;

            if (Val == 1 && NowResoures > ResourcesInt)
            {
                NowResoures = ResourcesInt;
            }
            else if (Val == -1 && NowResoures < ResourcesInt)
            {
                NowResoures = ResourcesInt;
            }

            ResourcesText.text = "Resources : " + NowResoures;
        }

        if(Time.time - ErrorTime > 4)
        {
            ErrorParant.SetActive(false);
        }
    }

    public void TowerListButtonClick(int index)
    {
        if (Gm_sc.Td.activeSelf)
        {
            Gm_sc.Create(index);
            Gm_sc.Td.SetActive(false);
            TowerUnitMenubool = false;
        }
        else
        {
            ErrorTextView("Please select a location");
        }
    }

    public void TowerListViewEnergize(int btnValue)
    {
        switch (btnValue)
        {
            case 0:
                if (TowerUnitMenuSwitch == true || TowerUnitMenubool == false)
                {
                    TowerMenuList.SetActive(true);
                    UnitMenuList.SetActive(false);
                    TowerUnitMenuSwitch = true;
                    TowerUnitMenubool = !TowerUnitMenubool;
                }
                else if (TowerUnitMenuSwitch == false && TowerUnitMenubool == true)
                {
                    TowerUnitMenuSwitch = true;
                    TowerMenuList.SetActive(true);
                    UnitMenuList.SetActive(false);
                }
                TowerMenuButton.GetComponent<Image>().color = new Color(0, 255, 0);
                UnitMenuButton.GetComponent<Image>().color = new Color(255, 255, 255);
                break;
            case 1:
                if (TowerUnitMenuSwitch == false || TowerUnitMenubool == false)
                {
                    TowerMenuList.SetActive(false);
                    UnitMenuList.SetActive(true);
                    TowerUnitMenuSwitch = false;
                    TowerUnitMenubool = !TowerUnitMenubool;
                }
                else if (TowerUnitMenuSwitch == true && TowerUnitMenubool == true)
                {
                    TowerUnitMenuSwitch = false;
                    TowerMenuList.SetActive(false);
                    UnitMenuList.SetActive(true);
                }
                TowerMenuButton.GetComponent<Image>().color = new Color(255, 255, 255);
                UnitMenuButton.GetComponent<Image>().color = new Color(0, 255, 0);
                break;
            default:
                break;
        }
    }

    public void TowerInfoViewTrue(info _pTInfo)
    {
        if (_pTInfo != null)
        {
            TowerInfo.SetActive(true);
            TowerInfo.GetComponent<TowerInfoScript>().TarGetInfoInitailize(_pTInfo);
        }
    }

    public void TowerInfoViewFalse()
    {
        TowerInfo.SetActive(false);
    }

    public void DoubleSpeedButtonClick()
    {
        Image DoubleBtnImg = GameObject.Find("DoubleSpeedButton").GetComponent<Image>();
        if (Time.timeScale == 1.0f)
        {
            DoubleBtnImg.color = new Color(0, 255, 0);
            Time.timeScale = 2.0f;
        }
        else if(Time.timeScale == 2.0f)
        {
            DoubleBtnImg.color = new Color(255, 255, 255);
            Time.timeScale = 1.0f;
        }
    }

    public void PlayMenuButtonClick()
    {
        timeScale = Time.timeScale;
        Time.timeScale = 0.0f;
        PlayMenu.SetActive(true);
    }

    public void GoGameButtonClick()
    {
        Time.timeScale = timeScale;
        PlayMenu.SetActive(false);
    }

    public void AddTowerMenu(info _pTinfo)
    {
        GameObject TestObject = Instantiate(this.TowerButton) as GameObject;

        TowerButtonScript TestSC = TestObject.GetComponent<TowerButtonScript>();

        Transform ParentObj = null;

        if ((_pTinfo.index) / 1000 == 1) //(_pTinfo.index) / 1000 == 1
            ParentObj = TowerContent;
        else if ((_pTinfo.index) / 1000 == 2)
            ParentObj = UnitContent;

        TestSC.TowerInitailize(_pTinfo.index);
        if(ParentObj != null)
        TestObject.transform.SetParent(ParentObj);

        TestObject.transform.localScale = Vector3.one;
    }

    public void ErrorTextView(string _Erstr)
    {
        GameObject TestObject = Instantiate(this.ErrorText) as GameObject;

        Text Errtxt = TestObject.GetComponent<Text>();

        if (Error_List.Count == 3)
        {
            Destroy(Error_List.ToArray()[0]);
            Error_List.RemoveAt(0);
        }

        Error_List.Add(TestObject);

        if (ErrorCount == ErrorCol.Length - 1)
            ErrorCount = 0;
        else
            ErrorCount++;

        ErrorTime = Time.time;
        Errtxt.text = _Erstr;
        Errtxt.color = ErrorCol[ErrorCount];
        ErrorParant.SetActive(true);
        TestObject.transform.SetParent(ErrorParant.transform);

        TestObject.transform.localScale = Vector3.one;
    }

    public void SetResourInt(int _pValue)
    {
        ResourcesInt = _pValue;
    }

    /*public void testButton(int i)
    {
        if (i == 1) 
        {
            SetResourInt(1000);
        }
        else
        {
            SetResourInt(2000);
        }
    }*/
}
