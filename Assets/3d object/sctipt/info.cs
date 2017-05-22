using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objinfo
{
    public string name;
    public int index;
    public float maxhp;
    public float hp;
    public float power;
    public float def;
    public float speed;

    public void setInfo(string _name, int _index, float _maxhp, float _power, float _def, float _speed)
    {
        name = _name;
        index = _index;
        hp = maxhp = _maxhp;
        power = _power;
        def = _def;
        speed = _speed;
    }
}





public class info : MonoBehaviour
{
    public GameObject hpbar;
    public bool die = false;
    public bool bHpbar = true;   
    public string oname;
    public int index;
    public float maxhp;
    public int power;
    public int def;
    public int speed;

    bool on = false;
    float tspeed, duration, startTime;

    float hp;
    public objinfo oinfo;
    // Use this for initialization
    void Start()
    {
        if (hpbar != null)
        {
            // 캔버스에
            hpbar = Instantiate(hpbar);
            hpbar.transform.parent = Gm.getgm().canvas.transform;
            hpbar.transform.localScale = Vector3.one;
            
            //hpbar = Instantiate(hpbar);
            //hpbar.transform.parent = transform.parent;
            //hpbar.transform.localScale = Vector3.one;

        }
        InfoSet();
    }

    // Update is called once per frame
    void Update()
    {
        if (oinfo != null)
        {
            if (oinfo.hp <= 0)
                die = true;
            if (bHpbar)
                Hpbar();
        }
        else
            InfoSet();
    }


    public void InfoSet()
    {
        oinfo = new objinfo();
        oinfo.setInfo(oname, index, maxhp, power, def, speed);
    }

    // Use this for initialization

    // Update is called once per frame


    // 공격 받았을 때 상태 변화 함수
    public void Attack(float dmg)
    {
        oinfo.hp -= (dmg - oinfo.def);
        //if (oinfo.hp <= 0)
        //    Destroy(gameObject);
    }

    // 디버프 받았을 때 상태 변화 함수
    public void Debuff(int val)
    {
        float tspeed = oinfo.speed;
        if (val == 0)
        {
            oinfo.speed = val;
        }
        else
        {
            oinfo.speed -= val;
        }
        oinfo.speed = tspeed;
    }

    public void Attack()
    {
        if (oinfo.hp > 0)
        {
            oinfo.hp--;
            //resize();
        }

        if (oinfo.hp <= 0)
        {
            //Destroy(gameObject);
        }
    }
    public void resize()
    {
        Debug.Log((Vector3.one * (oinfo.hp / oinfo.maxhp)));
        //if ((maxhp - hp)!=0)            
        gameObject.transform.localScale = (Vector3.one * (oinfo.hp / oinfo.maxhp));
    }
    public float GetMaxHp() { return oinfo.maxhp; }
    public float GetHp() { return oinfo.hp; }

 // 디버프 받았을 때 상태 변화 함수
    public void Debuff(float dmg, float drt)
    {
        if (on == false)
        {
            tspeed = oinfo.speed;
            duration = drt;
            startTime = Time.time;
            if (dmg == 0)
                oinfo.speed = dmg;
            else
            {
                if (dmg < oinfo.speed)
                    oinfo.speed -= dmg;
                else
                    oinfo.speed = 1;
            }
            on = true;
        }
    }


    public bool isDie()
    {
        return die;
    }
    // 캔버스 쓴거
    void Hpbar()
    {
        if (hpbar != null)
        {
            if(Gm.getgm().cm.GetComponent<Camera>().orthographicSize <= 3.8f)
            {
                hpbar.transform.localScale = Vector3.one;
            }
            else
            {
                float f = 1f - (0.64f*( Gm.getgm().cm.GetComponent<Camera>().orthographicSize - 3.8f)/7.2f);
                hpbar.transform.localScale = Vector3.one * f;
            }
            hpbar.transform.position = Gm.getgm().cm.GetComponent<Camera>().WorldToScreenPoint(transform.position + new Vector3(1f, 0,-1f));


            if (oinfo.hp <= 0)
            {
                Destroy(hpbar);
            }
            else
            {
                float HPbarRatio=0 ;
                if (oinfo.maxhp != 0)
                {
                    HPbarRatio = (oinfo.hp / oinfo.maxhp);
                    //Vector3 HP = hpbar.transform.localPosition;
                    //HP.x += 67 * HPbarRatio;
                    //hpbar.transform.localPosition = HP;
                    hpbar.transform.FindChild("hp").GetComponent<Image>().fillAmount = HPbarRatio;
                        //new Vector3(HPbarRatio, hpbar.transform.localScale.y, hpbar.transform.localScale.z);

                }
            }
        }
    }
    
    /*
    void Hpbar()
    {
        if (hpbar != null)
        {
            hpbar.transform.position = transform.localPosition; //+ new Vector3(0.5f, 0, -1.5f);


            if (oinfo.hp <= 0)
            {
                Destroy(hpbar);
            }
            else
            {
                float HPbarRatio = 0;
                if (oinfo.maxhp != 0)
                {
                    HPbarRatio = (oinfo.hp / oinfo.maxhp);
                    Vector3 HP = hpbar.transform.position;
                    HP.x += 67 * HPbarRatio;
                    hpbar.transform.position = HP;
                    hpbar.GetComponentInChildren<Transform>().localScale = new Vector3(HPbarRatio, hpbar.transform.localScale.y, hpbar.transform.localScale.z);
                    hpbar.GetComponentInChildren<SpriteRenderer>().fi

                }
            }
        }
    }
    */

    public void DestroyObj(float f)
    {
        Invoke("DestroyObj", f);
    }

    public void DestroyObj()
    {
        Destroy(gameObject);
    }
}
