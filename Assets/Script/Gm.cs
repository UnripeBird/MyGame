using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
[Serializable]
public struct objlist
{
    public GameObject[] tower;
    public GameObject[] unit;
}
public struct selectinfo
{
    public GameObject g;
    public Vector2 p;
}

public class Gm : MonoBehaviour {

    public GameObject testgm;
    public GameObject cm;
    public GameObject target;
    public GameObject Td;
    public Animator a;
    public Canvas canvas;
    //bool bTd = false;
    public objlist list;
    float x, z;
    float check_x, check_z;
    private Touch tempTouchs;
    private Vector2 touchedPos;

    private bool touchOn;
    private bool bMove=false;
    private bool bMapMove = false;

    private float initTouchDist;

    //GameObject g;

    public int money;
    AnimatorControllerParameter p;
        
    
    selectinfo sinfo;

    static Gm gm;
    // Use this for initialization
    void Awake()
    {
        foreach(GameObject Tower in list.tower)
        {
            if(Tower != null)
                Tower.GetComponent<info>().InfoSet();
        }
        foreach (GameObject Tower in list.unit)
        {
            if (Tower != null)
                Tower.GetComponent<info>().InfoSet();
        }
        gm = this;
    }

    void Start () {
        //BitmapFontImporter.GenerateFont();
        
        TdState(false);
        //p= inanimator.par
    }

    
	
	// Update is called once per frame
	void Update () {
        move();
	}
    void move()
    {

        //Camera.main.ScreenPointToRay (Input.GetTouch(0).position);
        if (Application.platform == RuntimePlatform.Android)
        {
            if ((IsPointerOverUIObject() == false))
            {
                if (Input.touchCount == 3)//test target
                {
                    sinfo = select(Input.GetTouch(2).position, "");
                    if (sinfo.g != null)
                    {
                        switch (sinfo.g.tag)
                        {
                            case "Plane":
                                move(target,x,z);
                                //target.transform.position = new Vector3(x, target.transform.position.y, z);
                                break;


                            default:
                                TdState(false);
                                break;
                        }
                        check_x = x;
                        check_z = z;
                    }
                }

                else if (Input.touchCount > 1) // 확대
                {
                    if (Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position) < initTouchDist && cm.GetComponent<Camera>().orthographicSize <= 16f)
                    {
                        cm.GetComponent<Camera>().orthographicSize += 0.2f;
                    }
                    else if (Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position) > initTouchDist && cm.GetComponent<Camera>().orthographicSize >= 3f)
                    {
                        cm.GetComponent<Camera>().orthographicSize -= 0.2f;
                    }

                    initTouchDist = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);

                }

                else if (Input.touchCount == 1) //터치
                {

                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        touchedPos = Input.GetTouch(0).position;
                        sinfo = select(touchedPos, "");


                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        if (Td.activeSelf)
                        {
                            if (sinfo.g != null)
                            {
                                x = sinfo.p.x;
                                z = sinfo.p.y;

                                if (check_x == x && check_z == z)
                                    bMove = true;
                                else
                                    bMapMove = true;
                            }
                        }

                        if (bMapMove || !Td.activeSelf)
                        {
                            if (!bMapMove)
                                bMapMove = true;
                            Vector2 v = touchedPos - Input.GetTouch(0).position;

                            cm.GetComponent<Camera>().transform.position += new Vector3((v.x / 102.4f) - (v.y / 60f), 0, (v.x / 102.4f) + (v.y / 60f));
                            touchedPos = Input.GetTouch(0).position;

                        }
                        else if (bMove)
                        {
                            touchedPos = Input.GetTouch(0).position;
                            sinfo = select(touchedPos, "");
                            if (sinfo.g != null)
                            {
                                x = sinfo.p.x;
                                z = sinfo.p.y;
                                move(Td, x, z);
                                //Td.transform.position = new Vector3(x, Td.transform.position.y, z);
                                check_x = x;
                                check_z = z;
                                bMove = true;
                            }
                        }

                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Stationary)
                    {
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        if (bMapMove || bMove)
                        {
                            if (bMapMove)
                            {
                                bMapMove = false;
                            }
                            if (bMove)
                            {
                                bMove = false;

                            }
                            return;
                        }

                        if (sinfo.g != null)
                        {
                            switch (sinfo.g.tag)
                            {
                                case "Plane":

                                    //Debug.Log("a");
                                    if (Td.activeSelf)
                                    {
                                        if (check_x == x && check_z == z)
                                        {
                                            Create(1000);
                                            TdState(false);
                                        }
                                        else
                                            TdState(false);
                                    }
                                    else
                                    {
                                        x = sinfo.p.x;
                                        z = sinfo.p.y;
                                        move(Td, x, z);
                                        //Td.transform.position = new Vector3(x, Td.transform.position.y, z);
                                        TdState(true);
                                        check_x = x;
                                        check_z = z;
                                    }
                                    break;



                                default:
                                    gameObject.GetComponent<UIgm>().TowerInfoViewTrue(sinfo.g.GetComponent<info>());
                                    TdState(false);
                                    break;
                            }

                        }

                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && (EventSystem.current.IsPointerOverGameObject() == false))

            {
                
                sinfo = select(Input.mousePosition, "");

                if (sinfo.g != null)
                {
                    switch (sinfo.g.tag)
                    {
                        case "Plane":
                            gameObject.GetComponent<UIgm>().TowerInfoViewFalse();
                            x = sinfo.p.x;
                            z = sinfo.p.y;
                            Td.transform.position = new Vector3(x, Td.transform.position.y, z);
                            //Debug.Log("a");
                            if (Td.activeSelf)
                            {

                                if (check_x == x && check_z == z)
                                {
                                    TdState(false);
                                }
                            }
                            else
                                TdState(true);



                            break;



                        default:
                            //Debug.Log(sinfo.g.tag);
                            gameObject.GetComponent<UIgm>().TowerInfoViewTrue(sinfo.g.GetComponent<info>());
                            TdState(false);
                            break;
                    }
                    check_x = x;
                    check_z = z;
                }
            }


            if (Input.GetMouseButtonDown(1))
            {
                
                sinfo = select(Input.mousePosition, "");
                if (sinfo.g != null)
                {
                    switch (sinfo.g.tag)
                    {
                        case "Plane":
                            target.transform.position = new Vector3(x, target.transform.position.y, z);
                            break;



                        default:
                            TdState(false);
                            break;
                    }
                    check_x = x;
                    check_z = z;
                }
            }


            if (Input.GetAxis("Mouse ScrollWheel") < 0 && cm.GetComponent<Camera>().orthographicSize <= 10.6f)
            {
                // Zoom In
                cm.GetComponent<Camera>().orthographicSize += 0.4f;
                Debug.Log(cm.GetComponent<Camera>().orthographicSize);
                //cm.transform.position -= (Vector3.one * 0.1f);

            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0 && cm.GetComponent<Camera>().orthographicSize >= 3f)
            {
                //cm.transform.position += (Vector3.one * 0.1f);
                cm.GetComponent<Camera>().orthographicSize -= 0.4f;
                Debug.Log(cm.GetComponent<Camera>().orthographicSize);
                // Zoom Out
            }
        }
    }

    public int boo(float f)
    {
        int b;
        if (f >= 0)
            b = 1;
        else
            b = -1;
        return b;
    }
    void TdState(bool b)
    {
        Td.SetActive(b);
    }
    public void Create(int n)
    {
        GameObject[] glist = null;
        switch (n/1000)
        {
            case 1:
                glist = list.tower;
                break;
            case 2:
                glist = list.unit;
                break;
        }

        if (glist != null)
        {
            GameObject g;
            g = Instantiate<GameObject>(glist[n % 1000]);
            g.transform.position = new Vector3(x, 0, z);
        }

        //g.GetComponent<MeshRenderer>().
    }


    selectinfo select(Vector3 position,string tag) 
    {
        RaycastHit[] raycastall;
        Ray ray = Camera.main.ScreenPointToRay(position);

        raycastall = Physics.RaycastAll(ray, 30).OrderBy(h => h.distance).ToArray();
        selectinfo s = new selectinfo();
        for (int i = 0; i < raycastall.Count(); i++)
        {
            Debug.Log(raycastall[i].transform.gameObject.tag);
            if (raycastall[i].transform.gameObject.tag == tag || tag == "")
            {
                x = ((int)(raycastall[i].point.x + boo(raycastall[i].point.x)) / 2) * 2;
                z = ((int)(raycastall[i].point.z + boo(raycastall[i].point.z)) / 2) * 2;
                s.g =  raycastall[i].transform.gameObject;
                s.p = new Vector2(x, z);
                return s;
            }
        }
        return s;
    }
    public static Gm getgm()
    {
        return gm;
    }

    private bool IsPointerOverUIObject()

    {

        // Referencing this code for GraphicRaycaster https://gist.github.com/stramit/ead7ca1f432f3c0f181f

        // the ray cast appears to require only eventData.position.

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);

        eventDataCurrentPosition.position = Input.GetTouch(0).position;



        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        int cnt = results.Count;

        for (int i=0; i< results.Count;i++)
        {
            if (results[i].gameObject.GetComponent<Text>() != null)
            {
                Debug.Log("aa");
                cnt--;
            }
        }
        return cnt > 0;

    }

    bool move(GameObject g,float x,float z)
    {
        if(g == Td)
        g.transform.position = new Vector3(x, g.transform.position.y, z);

        return true;
    }
}

 