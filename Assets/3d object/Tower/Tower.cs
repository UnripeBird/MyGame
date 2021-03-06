﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    Transform ClosestEnemy;
    Transform Target;
    public Transform Weapon;
    public Transform Head;
    Vector3 Pos;
    Vector3 TargetDir;
    string Closesttag = "Enemy";
    float Distance;
    float SkillTime;
    float StartTime = 0.5f;

    public AudioClip []ac;

    [Range(1, 3)] // 1: 공격형 2: 방어형 3: 지원형
    public int TowerType;    
    [Range(1, 10)] // Type 당 최대 5 종류
    public int TowerDetailType;

    public float TowerRange, SkillTimeSec, BulletSpeed;
    public GameObject BulletPrefab;

    void Start()
    {
        if (TowerType == 1 || TowerType == 3)            // 공격형 or 지원형 타워일 경우 진입
            InvokeRepeating("GetClosestEnemy", 0, SkillTimeSec); // 0초부터 GetClosestEnemy 함수 호출 후 스킬 대기시간마다 반복 호출
    }

    void Update()
    {
        if(GetComponent<info>().isDie())
        {
            GetComponent<info>().DestroyObj();
        }
        else if (Target != null) // 타워 인식 범위에 타겟이 있을 경우 진입
        {
            if (Target.GetComponent<info>().isDie())
            {
                Target = null;
                return;
            }
            if (Head != null)
            {
                Head.LookAt(Target);                           // 타워가 타겟을 쳐다봄
            }
            TargetDir = Target.position - Weapon.position;   // 타워로부터 해당 타겟 방향 정보 저장 
            if(StartTime==0)
                StartTime = Time.time;

            SkillTime = Time.time - StartTime;                  // SkillTime - 스킬 사용 후 흐른 시간
            if (SkillTime >= SkillTimeSec)                      // SkillTime이 SkillTimeSec(스킬 대기 시간) 이상일 때 진입
            {
                Skill();                 // Target에 스킬 사용
                StartTime = Time.time;   // 다음 스킬 사용까지의 대기 시작 시간을 현재 스킬 사용 직후 시간으로 변경
                SkillTime = 0;           // 스킬 사용 후 흐른 시간 0초로 변경
            }
            
        }
        else
        {
            StartTime = 0;
            SkillTime = 0;
            if(TowerDetailType==10)
            {
                if (GetComponent<AudioSource>().loop)
                {
                    GetComponent<AudioSource>().clip = ac[1];
                    GetComponent<AudioSource>().loop = false;
                    GetComponent<AudioSource>().Play();
                    Weapon.GetComponent<EmissionModuleEffect>().stop();
                }
            }

            if(TowerDetailType==9)
            {
                Weapon.GetComponent<EmissionModuleEffect>().stop();
            }
        }
        


    }

    // 타워 스킬 함수
    void Skill()
    {
        // 발사체 생성 - transform.position(위치)에서 transform.rotation(형태)하게 날라갈 BulletFrefab(발사체)
        GameObject Projectile =null;
        if (TowerDetailType<=4)
            Projectile = Instantiate(BulletPrefab, Weapon.position, Weapon.rotation) as GameObject;

		float x = Random.Range(TargetDir.x - 50, TargetDir.x + 50);   // 램덤 x 좌표 설정 
        float z = Random.Range(TargetDir.z - 50, TargetDir.z + 50);   // 랜덤 z 좌표 설정
        if (TowerType == 1) // 공격형 타워일 때 진입
            switch (TowerDetailType)
            {
                case 1: // 중거리 단발 공격 포탑
                case 2: // 원거리 단발 공격 포탑
                case 3: // 중원거리 단발 범위 공격 포탑
                case 4: // 단거리 연사 공격 포탑
                    // 월드 좌표 기준 TargetDir 방향으로 BulletSpeed 속력으로 날아감
                    Projectile.GetComponent<Rigidbody>().velocity = transform.TransformDirection(TargetDir * BulletSpeed);
                    GetComponent<AudioSource>().Play();
                    //Debug.Log(TargetDir);
                    break;
                case 5: // 중단거리 다방향 공격 포탑
                    Projectile.GetComponent<Rigidbody>().velocity = 
                        transform.TransformDirection(new Vector3(x, TargetDir.y, z) * BulletSpeed);
                    Projectile.GetComponent<Rigidbody>().velocity =
                        transform.TransformDirection(new Vector3(x, TargetDir.y, z) * BulletSpeed);
                    Projectile.GetComponent<Rigidbody>().velocity =
                        transform.TransformDirection(new Vector3(x, TargetDir.y, z) * BulletSpeed);
                    break;
                case 9: //전기

                    Weapon.GetComponent<EmissionModuleEffect>().start();
                    Projectile = Instantiate(Weapon.gameObject, Target.position, Weapon.rotation) as GameObject;
                    Destroy(Projectile, 2f);
                    break;

                case 10: //화염
                    
                    GetComponent<AudioSource>().clip = ac[0];
                    GetComponent<AudioSource>().loop = true;
                    GetComponent<AudioSource>().Play();
                    Weapon.GetComponent<EmissionModuleEffect>().start();
                    break;
            }
        else                // 지원형 타워일 때 진입
            switch (TowerDetailType)
            {
                case 1: // 범위 적 이동속도 감소 디버프(원거리)
                case 2: // 단일 적 일정시간 경직 디버프(원거리)
                    Projectile.GetComponent<Rigidbody>().velocity = transform.TransformDirection(TargetDir * BulletSpeed);
                    break;
            }
    }

    // 가장 가까운 적 판별하는 함수
    void GetClosestEnemy()
    {
        GameObject[] Enemys = GameObject.FindGameObjectsWithTag(Closesttag); // Closesttag 태그를 가진 객체를 모두 찾아서 저장
        float ClosestDistValue = Mathf.Infinity; // 가장 가까운 적 거리 값에 양의 무한 값 초기화(제일 먼 객체의 거리가 얼마인지 모르기 때문에)
        ClosestEnemy = null;                     // 가장 가까운 적 정보 값에 null 저장(인식 범위 안에 객체가 없는 상황도 있기 때문에)
        foreach (GameObject Enemy in Enemys)
        {
            /*  1) 해당 적 위치 값 - Pos
                2) 타워부터 해당 적까지 거리 값 - Distance
                3) Distance 값이 TowerRange(타워 인식 범위) 미만일 경우, 
                   ClosestDistValue(가장 가까운 적의 거리 값) 보다 작은지 비교하여
                   작다면 ClosetDistValue에 Distance 값 저장 and 
                   ClosestEnemy(가장 가까운 적 정보)에 해당 적 정보 저장
                4) 적 수만큼 반복하여 현재 타워로부터 가장 가까운 적 정보를 가지는 ClosestEnemy 값 구함  */
            if (!Enemy.GetComponent<info>().isDie())
            {
                Pos = Enemy.transform.position;
                Distance = (Pos - transform.position).sqrMagnitude;
                if (Distance < TowerRange)
                {
                    if (Distance < ClosestDistValue)
                    {
                        ClosestDistValue = Distance;
                        ClosestEnemy = Enemy.transform;
                    }
                }
            }
        }
        Target = ClosestEnemy; // Target(타워가 최종적으로 인식할 적 정보)에 ClosestEnemy(가장 가까운 적 정보) 값 저장
    }
}
