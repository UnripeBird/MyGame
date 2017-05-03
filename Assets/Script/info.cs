using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objinfo
{
    public string name;
    public int index;
    public int maxhp;
    public int hp;
    public int power;
    public int def;
    public int speed;

    public void setInfo(string _name, int _index, int _maxhp, int _power, int _def, int _speed)
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

    /*
    float maxhp = 3f;
    float hp;
    float damage = 3f;
    float def = 2f;
    */
    public string oname;
    public int index;
    public int maxhp;
    public int power;
    public int def;
    public int speed;

    float hp;
    /*
    public string tname;
    public float maxhp;
    
    public float damage;
    public float def;
    public int index;
    */
    public objinfo oinfo;
    // Use this for initialization
    void Start()
    {

        hp = maxhp;
        InfoSet();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void InfoSet()
    {
        oinfo = new objinfo();
        oinfo.setInfo(oname, index, maxhp, power, def, speed);
    }

    // Use this for initialization

    // Update is called once per frame


    // 공격 받았을 때 상태 변화 함수
    public void Attack(int dmg)
    {
        oinfo.hp -= (dmg - oinfo.def);
        if (oinfo.hp <= 0)
            Destroy(gameObject);
    }

    // 디버프 받았을 때 상태 변화 함수
    public void Debuff(int val)
    {
        int tspeed = oinfo.speed;
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
        if (hp > 0)
        {
            hp--;
            //resize();
        }

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void resize()
    {
        Debug.Log((Vector3.one * (hp / maxhp)));
        //if ((maxhp - hp)!=0)            
        gameObject.transform.localScale = (Vector3.one * (hp / maxhp));
    }
    public float GetMaxHp() { return maxhp; }
    public float GetHp() { return hp; }


}
