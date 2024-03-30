using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    //�޺� ����
    int comboStep = 1;
    float comboTimer = 0.0f;
    private bool isAttackLock;//2���� ���� �Է� ������ �÷��� �߰�
    bool ya=false;

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
        state = GetComponent<PlayerStatus>();
        player_controller = GetComponent<Player>();
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
        if(player_controller.isAttack) { return; }
        StartCoroutine(coStrongAttack());
    }

    IEnumerator coStrongAttack()
    {
        anim.SetTrigger("doStrongAttack");
        yield return null;
    }
}
