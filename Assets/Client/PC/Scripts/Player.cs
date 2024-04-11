using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Player : MonoBehaviourPun
{
    float hAxis;
    float vAxis;
    private int speed;
    private float jumpPower;

    Vector3 moveVec;
    Vector3 jumpVec;

    bool rDown;
    bool jDown;
    bool isDeath;
    public bool dDown;
    public bool isJump;
    public bool isDefense;

    //����
    public float attackDelay = 1.0f;
    bool isAttackReady;
    bool left_attack;                       //��Ŭ�� ����
    bool right_attack;                      //��Ŭ�� ����
    bool strong_attack;                     //��+��Ŭ�� ���� ��ģ��
    public bool isAttack = false;                          //���� ��?
    bool canAttack;

    AttackController attack_controller;

    //�и�
    bool isParrying;

    //�˹�
    public float knockbackForce = 0.5f;
    public float knockbackTime = 0.3f;
    bool isKnockback;

    public Animator anim;
    Rigidbody rigid;
    PlayerStatus state;
    public CameraShake cameraShaking;

    Transform cameraPlayer; // cameraArm, player�� �θ�

    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        state = GetComponent<PlayerStatus>();
        attack_controller = GetComponent<AttackController>();
        cameraShaking = Camera.main.GetComponent<CameraShake>();
        cameraPlayer = transform.parent;
    }
    void Start()
    {
        speed = state.moveStats.speed;
        //jumpPower = state.moveStats.jumpPower;
    }

    // Update is called once per frame
    void Update()
    {
        
        /*if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }*/
        attackDelay += Time.deltaTime;
        isAttackReady = state.combatStats.attack_rate <= attackDelay;
        

        GetInput();
        Move();
        Turn();
        Jump();
        if (state.basicStats.hp <= 0) { Death(); }
        hit();
        //if (isAttackReady && !isJump && !isDeath && !isAttack) canAttack = true;
        if (isAttackReady && !isJump && !isDeath) canAttack = true;
        else canAttack = false;
        attack_controll();
        Defenssing(dDown);
        if(!dDown) isDefense = false;
    }
    void attack_controll()
    {
        if (canAttack)
        {
            if (left_attack)
            {
                attack2();
            }
            else if (right_attack)
            {
                attack1();
            }
            else if (strong_attack)
            {
                strongAttack();
            }
        }
    }
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal"); // x�� �̵�(-1/1)
        vAxis = Input.GetAxisRaw("Vertical"); // z�� �̵�(-1/1)
        rDown = Input.GetKey(KeyCode.LeftShift);//leftshift
        jDown = Input.GetKeyDown(KeyCode.Space);//spacebar
        left_attack = Input.GetMouseButtonDown(0);
        right_attack = Input.GetMouseButtonDown(1);
        strong_attack = Input.GetMouseButtonDown(2);
        dDown = Input.GetKey(KeyCode.E); //���潺
        
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        if (isJump)
        {
            moveVec = jumpVec;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death") || isAttack || anim.GetCurrentAnimatorStateInfo(0).IsName("Roll") || isKnockback || anim.GetCurrentAnimatorStateInfo(0).IsName("Defending"))
        {
            moveVec = Vector3.zero;
        }

        transform.position += moveVec * speed * (rDown ? 2.0f : 1.0f) * Time.deltaTime;

        anim.SetBool("isWalk", moveVec != Vector3.zero);
        anim.SetBool("isRun", rDown);
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        jumpVec = moveVec;
        if (jDown && !isJump && !isAttack)
        {
            isJump = true;
            anim.SetBool("isJump", true);
            /*if (anim.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
            {
                return;
            }*/

            anim.SetTrigger("doJump");

            Invoke("JumpOut", 1.16f);
        }
    }

    void JumpOut()
    {
        anim.SetBool("isJump", false);
        isJump = false;
    }


    void Defenssing(bool dDwon)
    {
        if(dDown && !isDefense)
        {
            anim.SetBool("Defense", dDown);
        }
        if(!dDown&&isDefense) {

            attack_controller.weapon_right.ShieldEffectOut();
            isDefense = false;
            anim.SetBool("Defense", dDown);
        }
    }

    public void DefensingHit()
    {
        anim.SetTrigger("getDefenseHIt");
    }
    void attack1()         
    {
        attack_controller.attack1();
    }

    void attack2()
    {
        attack_controller.attack2();
    }

    void strongAttack()
    {
        attack_controller.strongAttack();
    }

    public void attackOut()
    {
        attackDelay = 0;
        //isAttack = false;
    }

    public void isAttackAnimation()             //���� �ִϸ��̼� ���� �̺�Ʈ 1 (���� ����)
    {
        isAttack = true;
    }

    public void WeaponUse()                     //���� �ִϸ��̼� ���� �̺�Ʈ 2  (���� ��� �����ϴ� ����)
    {
        attack_controller.weapon_right.Use();
    }

    public void WeaponAttackOut()               //���� �ִϸ��̼� ���� �̺�Ʈ 3 (���� ��� ������ ����)
    {
        attack_controller.weapon_right.AttackOut();
    }
    public void isAttackAnimationEnd()          //���� �ִϸ��̼� ���� �̺�Ʈ 4 (���� ����)
    {
        isAttack = false;
    }
   

    bool CanAttack()
    {
        return isAttackReady && !isJump && !isDeath;
    }
     
    void hit()
    {

    }

    void Death()
    {
        if (!isDeath)
        {
            anim.SetTrigger("doDeath");
            isDeath = true;

            Invoke("DestroyPlayer", 5.0f);
        }
    }

    void DestroyPlayer()
    {
        Destroy(gameObject);
    }


    public void TakeDamage(int damage, Vector3 enemnyPosition)
    {
       state.TakeDamage(damage, enemnyPosition);
    }
    void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

    }
 


    public void Knockback(Vector3 enemyVec)
    {
        isKnockback = true;
        StartCoroutine(OnKnockback(enemyVec));
    }


    IEnumerator OnKnockback(Vector3 enemyVec)
    {
        Debug.Log("�˹�");
        float startTime = Time.time;
        
        Vector3 reactVec = (transform.position - enemyVec).normalized;
        reactVec.y += 1.0f;
        rigid.AddForce(reactVec * knockbackForce, ForceMode.Impulse);
        while (Time.time < startTime + knockbackTime)
        {
            yield return null;
        }
        isKnockback = false;
    }

}
