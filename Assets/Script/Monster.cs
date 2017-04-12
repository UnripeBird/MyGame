using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour {

    public GameObject target;
    public GameObject monster;
    NavMeshAgent agent;
    GameObject Atarget = null;

    ArrayList ar;

    bool bAttack = false;
    bool bAgent = true;
    float t=0;
    float delay = 1f;
    enum state {idel =0,work,attack };
    bool arrived = false;

    float tdistance = 0;
    //float area;
    // Use this for initialization
    void Start () {
        target = Gm.getgm().target;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 1f;
    }
	
	// Update is called once per frame
	void Update () {

        if (!bAttack)
        {
            if (!agent.isActiveAndEnabled && !arrived)
                agent.enabled=true;
            else if(arrived)
                agent.enabled = false;
            //Vector3 direction = (target.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (tdistance != distance && agent.isActiveAndEnabled)
            {
                tdistance = distance;

                agent.SetDestination(target.transform.position);                
                arrived = false;                
            }

            

            if (distance < agent.stoppingDistance)
            {
                arrived = true;
                Debug.Log("a");
            }
            else
                arrived = false;

            if (arrived)
                monster.GetComponent<Animator>().SetInteger("State", (int)state.idel);
            else
                monster.GetComponent<Animator>().SetInteger("State", (int)state.work);

            

            // 현재 속도를 보관한다.

            //　목적지에 가까이 왔으면 도착.
            
        }
        else
        {
            if(agent.isActiveAndEnabled)
            {
                Debug.Log(agent.isActiveAndEnabled);
                agent.enabled=false;
                //agent.SetDestination(transform.position);
            }

            if (Atarget != null)
            {
                
                //agent.SetDestination(Atarget.transform.position);
                Look(Atarget.transform.position);

                //agent.Stop();
                if (t == 0)
                {
                    monster.GetComponent<Animator>().SetInteger("State", (int)state.attack);
                    t = Time.time + delay;
                }
                else if (Time.time >= t)
                {
                    t = 0;
                    Atarget.GetComponent<info>().Attack();

                }

            }
            else
            {
                bAttack = false;
            }


        }
        
	}

    void OnTriggerEnter(Collider other)
    {
        if (!bAttack)
        {
            Atarget = other.gameObject;
            bAttack = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (Atarget!=null && Atarget == other.gameObject)
        {
            Atarget = null;
            bAttack = false;
        }
    }

    void Look(Vector3 v)
    {
        Vector3 dir = v - transform.position;
        dir.Normalize();
        Quaternion q = Quaternion.identity;
        q.SetLookRotation(dir, Vector3.up);
        transform.rotation = q;
    }

}
