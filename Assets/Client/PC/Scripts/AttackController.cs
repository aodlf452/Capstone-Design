using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    //��ؽ� ����
    public Weapon weapon_left;
    public Weapon weapon_right;

    Animator anim;
    Player player_controller;
    PlayerStatus state;
    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
        state= GetComponent<PlayerStatus>();
        player_controller = GetComponent<Player>();
    }


    public void attack1()
    {
        StartCoroutine(coAttack1());
    }

    IEnumerator coAttack1()
    {
        anim.SetTrigger("doLattack");
        weapon_left.Use(1.05f);         //�� ���� ���ÿ� ���� ������
        weapon_right.Use(1.05f);
        //attackDelay = 0;                ���⼭ �ʱ�ȭ �ϸ� ���� �ι� �Էµ�

        bool animationState = false;
        while (!animationState)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("atack11") && stateInfo.normalizedTime >= 0.8f)       //0.8f = �ִϸ��̼��� HasExitTime
            {
                animationState = true;
            }
            yield return null;
        }
        player_controller.attackOut();
    }


    public void attack2()
    {
        StartCoroutine(coAttack2());
    }

    IEnumerator coAttack2()         //��Ŭ�� ���� /�� �ڷ�ƾ ������ ���� �ð� �����ϰ� Use�� �ش� ������ ���� Ʈ���� ���� �ð��� �ѱ���.
    {
        //isAttack = true;
        anim.SetTrigger("doRattack");
        weapon_right.Use(0.15f);
        bool animationState = false;
        while (!animationState)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("atack16") && stateInfo.normalizedTime >= 0.95f)
            {
                animationState = true;
            }
            yield return null;
        }
        player_controller.attackOut();
    }

    public void strongAttack()
    {
        StartCoroutine(coStrongAttack());
    }

    IEnumerator coStrongAttack()
    {
        //isAttack = true;
        anim.SetTrigger("doStrongAttack");
        yield return new WaitForSeconds(0.6f);          //������ �� ���� 0.01�� ���̷� ������ �����ų� �ʰ� ����
        weapon_left.Use(0.3f);
        yield return new WaitForSeconds(0.26f);         //0.14s late
        weapon_right.Use(0.6f);
        yield return new WaitForSeconds(0.85f);         //0.008s late
        weapon_right.Use(0.31f);
        weapon_left.Use(0.31f);
        yield return new WaitForSeconds(0.63f);         //0.001s fast
        weapon_left.Use(0.35f);
        //attackDelay = 0;
        bool animationState = false;
        while (!animationState)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("atack8") && stateInfo.normalizedTime >= 0.8f)
            {
                animationState = true;
            }
            yield return null;
        }
        player_controller.attackOut();
    }
}
