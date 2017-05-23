using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIgm : MonoBehaviour {

    List<GameObject> Error_List;
    AsyncOperation async=null;
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
    public GameObject ResourcesChangePanel;
    public GameObject R_EChangePanel;
    public GameObject E_RChangePanel;
    public GameObject E_RResourceSelect;

    public Transform TowerContent;
    public Transform UnitContent;

    public Text ResourcesText;
    public Text ResourcesText2;
    public Text E_REnergyText;
    public Text[] TextResources;
    public Text[] R_EResourcesText;
    public Text[] E_RResourcesText;
    

    Gm Gm_sc;

    bool TowerUnitMenubool;
    bool TowerUnitMenuSwitch;       //true : Tower , false : Unit

    private float timeScale;
    private float ErrorTime;
    
    public int[] Resources;
    int[] NowResoures;
    int[] R_EResource;
    int ResourcesInt;
    int NowResouresint;
    int E_RResourceSelectint;
    int E_REnergy;
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
        _info.oinfo.index = 2000;
        //_info.InfoSet();

        AddTowerMenu(Gm_sc.list.tower[0].GetComponent<info>());
        AddTowerMenu(Gm_sc.list.tower[1].GetComponent<info>());
        AddTowerMenu(_info);

        TowerUnitMenubool = false;
        TowerUnitMenuSwitch = true;
        UnitMenuList.SetActive(false);

        NowResouresint =  ResourcesInt = 0;

        ErrorCount = 0;

        ErrorTime = 0;
        
        //Resources = new int[5];
        NowResoures = new int[5];
        R_EResource = new int[5];

        E_RResourceSelectint = 0;

        for (int i = 0; i < 5; i++) 
        {
            //Resources[i] = 10;
            NowResoures[i] = 0;
            R_EResource[i] = 0;
        }
        Resources = Gm_sc.energy;
    }
	
	// Update is called once per frame
	void Update () { /////////////////////////UPDATE
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
        for (int i = 0; i < 5; i++)
        {
            if(NowResoures[i] != Resources[i])
            {
                int Val;
                Val = Gm_sc.boo(Resources[i] - NowResoures[i]);

                NowResoures[i] += Val * 7;

                if (Val == 1 && NowResoures[i] > Resources[i])
                {
                    NowResoures[i] = Resources[i];
                }
                else if (Val == -1 && NowResoures[i] < Resources[i])
                {
                    NowResoures[i] = Resources[i];
                }

                E_RResourcesText[i].text = TextResources[i].text = ":" + NowResoures[i];
            }
        }

        if(NowResouresint != ResourcesInt)
        {
            int Val;
            Val = Gm_sc.boo(ResourcesInt - NowResouresint);

            NowResouresint += Val * 7;

            if (Val == 1 && NowResouresint > ResourcesInt)
            {
                NowResouresint = ResourcesInt;
            }
            else if (Val == -1 && NowResouresint < ResourcesInt)
            {
                NowResouresint = ResourcesInt;
            }

            ResourcesText.text = ":" + NowResouresint;
            ResourcesText2.text = ":" + NowResouresint;
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

        if ((_pTinfo.oinfo.index) / 1000 == 1) //(_pTinfo.index) / 1000 == 1
            ParentObj = TowerContent;
        else if ((_pTinfo.oinfo.index) / 1000 == 2)
            ParentObj = UnitContent;

        TestSC.TowerInitailize(_pTinfo.oinfo.index);
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
    public void SetResources(int _pNum, int _pValue) // 자원 추가하는 함수 SetResources(자원번호,자원 증감량);
    {
        Resources[_pNum] += _pValue;
    }

    public void gamesave()
    {
    }

    public void MainMenuButtonClick()
    {
        StartCoroutine(GameSceneChange());
    }

    IEnumerator GameSceneChange()
    {
        if (async == null)
            async = SceneManager.LoadSceneAsync(0);

        yield return null;
    }

    public void ButtonClickEvent(int _pValue)
    {
        switch (_pValue)
        {
            case 0: //ResourcesChangeButton
                ResourcesChangePanel.SetActive(true);
                break;
            case 1: //ResourcesChangeExit
                ResourcesChangePanel.SetActive(false);
                E_REnergy = 0;
                E_REnergyText.text = ":0";
                for (int i = 0; i < 5; i++)
                {
                    R_EResource[i] = 0;
                    R_EResourcesText[i].text = ":0";
                }
                break;
            case 2: //R_EButton
                R_EChangePanel.SetActive(true);
                E_RChangePanel.SetActive(false);
                break;
            case 3: //E_RButton
                E_RChangePanel.SetActive(true);
                R_EChangePanel.SetActive(false);
                break;
        }
    }

    public void ChangePanelButton(int _pValue)
    {
        switch (_pValue)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
                if(_pValue % 2 == 0)        //+
                {
                    if (NowResoures[_pValue / 2] >= R_EResource[_pValue / 2] + 2) 
                    {
                        R_EResource[_pValue / 2] += 2;
                    }
                }
                else if(_pValue % 2 == 1)   //-
                {
                    if (R_EResource[_pValue / 2] > 0)
                    {
                        R_EResource[_pValue / 2] -= 2;
                    }
                }
                R_EResourcesText[_pValue / 2].text = ":" + R_EResource[_pValue / 2];
                break;
            case 10:
                int SumNum = 0;
                for (int i = 0; i < 5; i++)
                {
                    SumNum += R_EResource[i];
                    Resources[i] -= R_EResource[i];
                    R_EResource[i] = 0;
                    R_EResourcesText[i].text = ":0";
                }
                
                ResourcesInt += SumNum / 2;
                break;
            case 11: //Energy +
                if(NowResouresint >= E_REnergy + 1)
                {
                    E_REnergy += 1;
                    E_REnergyText.text = ":" + E_REnergy;
                }
                break;
            case 12: //Energy -
                if (E_REnergy > 0)
                {
                    E_REnergy -= 1;
                    E_REnergyText.text = ":" + E_REnergy;
                }
                break;
            case 13:
            case 14:
            case 15:
            case 16:
            case 17:
                Vector3 vec3;
                vec3 = E_RResourceSelect.GetComponent<RectTransform>().localPosition;
                vec3.y = 216.0f - ((_pValue - 13.0f) * 105.0f);
                E_RResourceSelect.GetComponent<RectTransform>().localPosition = vec3;
                E_RResourceSelectint = _pValue - 13;
                break;
            case 18:
                SetResources(E_RResourceSelectint, 2 * E_REnergy);
                ResourcesInt -= E_REnergy;
                E_REnergy = 0;
                E_REnergyText.text = ":0";
                break;
        }
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
