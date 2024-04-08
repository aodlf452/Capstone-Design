using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
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
    public CameraShake cameraShaking;
    Transform effectPoint;
    public GameObject effectPrefab;

    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
        state = GetComponent<PlayerStatus>();
        player_controller = GetComponent<Player>();
        cameraShaking = Camera.main.GetComponent<CameraShake>();
        effectPoint = GameObject.Find("SwordEffect").transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SwordEffect();
        }
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
        yield return new WaitForSeconds(0.58f);

        cameraShaking.Zoom(0.55f,0.638f,0.0f,15.0f);

        yield return new WaitForSeconds(1.18f);
        cameraShaking.Shaking(1.0f,7.5f);
    }

    public void Parrying() // ��� �и� �ִϸ��̼�
    {
        anim.SetTrigger("doParrying");
    }





    //���� ī�޶� ȿ��
    public void ShakeCamera()
    {
        cameraShaking.Shaking(0.5f, 2.0f);
    }
    public void ZoomCamera()
    {
        cameraShaking.Zoom(0.36f,0.2f,0.0f,5.0f);
    }

    public void SwordEffect()
    {
        if( effectPoint == null || effectPrefab == null)
        {
            return;
        }
            GameObject effectInstance = Instantiate(effectPrefab, effectPoint.position, effectPoint.rotation);
            Destroy(effectInstance, 1.0f);
    }
}
