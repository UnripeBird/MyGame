using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int Type, DetailType;
    public float Power, expRange;

	void Start ()
    {
        // "Tower" layer와 "Bullet" layer 의 충돌을 무시
        //Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Tower"), LayerMask.NameToLayer("Bullet"), true);
    }
	
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (Type == 1 && other.transform.tag == "Enemy")
        {
            /* 공격형 타워가 날린 발사체에 충돌된 객체의 태그가 Enemy일 경우 진입
             세부 타입에 따라 발사체 충돌 시 충돌 적 상태 변화(+ 범위 적용), 이펙트 적용 */
            if (DetailType == 3)
            {
                Collider[] colls = Physics.OverlapSphere(transform.position, expRange); // 원점을 중심으로 일정 범위 안에 충돌 객체 찾음
                foreach (Collider coll in colls)
                {
                    // 일정 범위 안의 충돌 객체들에게 폭발 데미지, 폭발력을 줌 
                    coll.GetComponent<info>().Attack(Power);
                    if(coll.GetComponent<Rigidbody>() != null)
                    {
                        coll.GetComponent<Rigidbody>().mass = 1.0f;
                        coll.GetComponent<Rigidbody>().AddExplosionForce(400.0f, transform.position, expRange, 300.0f);
                    }
                }
            }
            else
                other.GetComponent<info>().Attack(Power);   // 충돌한 적 HP 감소           
            Destroy(gameObject);   // 충돌 후 발사체 파괴
        }
        else if (Type == 3 && other.transform.tag == "Enemy")
        {
            // 지원형 타워가 날린 발사체에 충돌된 객체의 태그가 Enemy일 경우 진입
            if (DetailType == 1)
            {
                Collider[] colls = Physics.OverlapSphere(transform.position, expRange); 
                foreach (Collider coll in colls)
                    coll.GetComponent<info>().Debuff(Power, 2.0f); // 일정 범위 안의 충돌 객체들 2초간 Speed 감소
            }
            else
                other.GetComponent<info>().Debuff(Power, 3.0f);    // 충돌한 적 일시적 3초간 경직
            Destroy(gameObject);
        }
    }
}
