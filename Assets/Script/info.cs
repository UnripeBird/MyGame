using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct objinfo
{
    public string name;
    public float maxhp;
    public float hp;
    public float damage;
    public float def;
    public int index;

    public void setinfo(string _name, float _maxhp, float _damage, float _def, int _index)
    {
        name = _name;
        hp = maxhp = _maxhp;
        damage = _damage;
        def = _def;
        index = _index;
    }

}



public class info : MonoBehaviour {

    /*
    float maxhp = 3f;
    float hp;
    float damage = 3f;
    float def = 2f;
    */
    public string tname;
    public float maxhp;
    float hp;
    public float damage;
    public float def;
    public int index;

    public objinfo oinfo;
    // Use this for initialization
    void Start() {
        hp = maxhp;
    }

    // Update is called once per frame
    void Update() {

    }    
    

    public void InfoSet()
    {
        oinfo = new objinfo();
        oinfo.setinfo(tname, maxhp, damage, def, index);
    }
    
    // Use this for initialization
   
    // Update is called once per frame
    
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
