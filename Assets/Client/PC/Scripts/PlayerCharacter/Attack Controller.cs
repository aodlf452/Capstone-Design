using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackController : MonoBehaviour
{
    // 전사는 weapon_right만 사용 ******** 어쌔신은 둘 다 사용
    [Header("수동 설정")]
    public Weapon weapon_left;
    public Weapon weapon_right;

    [Header("자동 설정")]
    public CameraShake cameraEffect;
    Animator anim;
    Player player_controller;
    [HideInInspector]
    public bool stepupBuffer= false;        //어쌔신 캐릭터 E키 강화 여부
    [HideInInspector]
    public int curAttack;                   //현재 공격 값

    public bool strongCoolTime { get; private set; }

    private void Awake()
    {
       anim = GetComponent<Animator>();
       player_controller = GetComponent<Player>();
       //cameraEffect = GetComponentInChildren<CameraShake>();
        strongCoolTime = false;
       cameraEffect = GameManager.Instance.SetEffect();

    }

    private void Start()
    {

    }
    public void attack1()
    {
        StartCoroutine(coAttack1()); 
    }
    
    IEnumerator coAttack1()
    {
        anim.SetTrigger("doRattack");
        anim.SetBool("isRattack", true);
        yield return null;
    }


    public void attack2()
    {
        StartCoroutine(coAttack2());
    }

    IEnumerator coAttack2()       
    {
        anim.SetTrigger("doLattack");
        anim.SetBool("isLattack", true);
        yield return null;
    }

    public void strongAttack()
    {
        strongCoolTime = true;
        if(player_controller.isAttack) { return; }
        StartCoroutine(coStrongAttack());
    }
    IEnumerator coStrongAttack()
    {
        Debug.Log("쿨타임 입니다.");
        anim.SetTrigger("doStrongAttack");
        /*
        yield return new WaitForSeconds(0.58f);

        cameraShaking.Zoom(0.55f,0.638f,0.0f,10.0f);

        yield return new WaitForSeconds(1.18f);
        cameraShaking.Shaking(0.5f,7.5f);
        */
        yield return new WaitForSeconds(10.0f);
        Debug.Log("쿨타임 끝났 습니다.");
        strongCoolTime = false;
    }
    public void parryingAttack()
    {
        if(player_controller.isAttack) { return; }
        StartCoroutine(coParryingAttack());
    }

    IEnumerator coParryingAttack()
    {
        anim.SetTrigger("doParrying");
        weapon_right.parryingAttack = true;
        if (weapon_left != null) { weapon_left.parryingAttack = true; }
        yield return null;
    }


    public void Parrying() // 즉시 패링 애니메이션
    {
        anim.SetTrigger("Parried");
    }





    //공격 카메라 효과
    public void ShakeCamera()
    {
        cameraEffect.Shaking(0.5f, 2.0f);
    }
    public void ZoomCamera()
    {
        cameraEffect.Zoom(2.0f, 2.0f, 0.5f, 40.0f);
    }


    //공격&방어 이펙트
    public void SwordEffect(int value = 0)
    {
        if (value != 0)
        {
            bool reverse = false;
            weapon_right.EffectInstance(reverse);
        }
        else
        {
            bool forward = true;
            weapon_right.EffectInstance(forward);
        }
    }

    public void SwordEffectLeft(int value = 0)
    {
        if (value != 0)
        {
            bool reverse = false;
            weapon_left.EffectInstance(reverse);
        }
        else
        {
            bool forward = true;
            weapon_left.EffectInstance(forward);
        }
    }

    public void StrongEffect()
    {
        weapon_right.StrongEffectInstance();
        if (weapon_left != null) weapon_left.StrongEffectInstance();
    }

    public void IsHeavyAttack()
    {
        weapon_right.isHeavyAttack = true;
        if (weapon_left != null) { weapon_left.isHeavyAttack = true; }
    }

    //어쌔신 E키(강화) 스킬
    public void AssassinStepUp()
    {
        stepupBuffer = true;
        Invoke("AssassinStepDown", 15.0f);
        weapon_right.HandEffect.gameObject.SetActive(true);
        weapon_left.HandEffect.gameObject.SetActive(true);
    }

    public void AssassinStepDown()
    {
        stepupBuffer = false;
        weapon_right.HandEffect.gameObject.SetActive(false);
        weapon_left.HandEffect.gameObject.SetActive(false);
    }

}
