using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//struct objInfo
//{
//    public string name;
//    public uint index;
//    public int maxhp;
//    public int hp;
//    public int power;
//    public int def;
//    public int speed;

//    public void setInfo(string _name, uint _index, int _maxhp, int _power, int _def, int _speed){
//        name = _name;
//        index = _index;
//        hp = maxhp = _maxhp;
//        power = _power;
//        def = _def;
//        speed = _speed;
//    }
//}

//public class info : MonoBehaviour
//{
//    public string oname;
//    public uint index;
//    public int maxhp;
//    public int power;
//    public int def;
//    public int speed;

//    objinfo oinfo;

//    void start()
//    {
//        oinfo.setinfo(oname, index, maxhp, power, def, speed);
//    }

//    void update()
//    {

//    }

//    // 공격 받았을 때 상태 변화 함수
//    public void attack(int dmg)
//    {
//        oinfo.hp -= (dmg - oinfo.def);
//        if (oinfo.hp <= 0)
//            destroy(gameobject);
//    }

//    // 디버프 받았을 때 상태 변화 함수
//    public void debuff(int val)
//    {
//        int tspeed = oinfo.speed;
//        if (val == 0)
//        {
//            oinfo.speed = val;
//        }
//        else
//        {
//            oinfo.speed -= val;
//        }
//        oinfo.speed = tspeed;
//    }

//    // hp 바 조절 함수
//    public void resize()
//    {
//        debug.log(vector3.one * (oinfo.hp / oinfo.maxhp));
//        gameobject.transform.localscale = (vector3.one * (oinfo.hp / oinfo.maxhp));
//    }
//}
