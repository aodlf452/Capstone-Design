using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Enemy : MonoBehaviourPun
{
    public int maxHP;
    public int curHP;
    public int atkPower; // 공격력

    Rigidbody rigid;
    BoxCollider boxCollider;
    Material mat;
    Color originalColor;
    Animator anim;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();

        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        originalColor = mat.color;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //0313_tk_

    public void OnDamage(int damage,Vector3 other)
    {
        curHP -= damage;
        Debug.Log("데미지 적용:" + damage);
        Vector3 reactVec = transform.position - other;
        Debug.Log("OreactVec: " + reactVec);
        StartCoroutine(OnDamage(reactVec));
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Melee") // 예시
        {
            // 1. 충돌한 other의 스크립트를 가져온다(ex. 무기)

            Weapon weapon = other.GetComponent<Weapon>();
            // 2. 해당 스크립트가 가진 공격력을 이용해서 체력 삭감
            //curHP -= weapon.damage;
            // 3, 현재 위치 - 피격 위치 = 반작용 벡터
            Vector3 reactVec = transform.position - other.transform.position;
            Debug.Log("TreactVec: " + reactVec);//지우자ㅏㅏㅏ
            // 4. 코루틴 시작
            StartCoroutine(OnDamage(reactVec));
            
        }

    }
    */
    IEnumerator OnDamage(Vector3 reactVec)
    {
        mat.color = Color.red; // 예시) 빨간색
        yield return new WaitForSeconds(0.1f); // 0.1s 대기

        if (curHP > 0)
        {
            // 원래 매터리얼 색상으로 돌리기
            mat.color = originalColor;
        }
        else // 몬스터 사망 시
        {
            mat.color = Color.gray;
            gameObject.layer = 9;

            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec*5, ForceMode.Impulse);
            Destroy(gameObject, 5);// 5초후 삭제
        }
    }
}
