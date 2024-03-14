using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float hAxis;
    float vAxis;
    public float speed = 2.0f;
    public float jumpPower = 4.0f;

    Vector3 moveVec;
    Vector3 jumpVec;

    bool rDown;
    bool jDown;
    bool isDeath;
    public bool isJump;

    //����
    float attackDelay = 0.0f;
    bool isAttackReady;
    bool left_attack;                       //��Ŭ�� ����
    bool right_attack;                      //��Ŭ�� ����
    bool strong_attack;                     //��+��Ŭ�� ���� ��ģ��
    bool isAttack;                          //���� ��?
    public Weapon weapon_left;
    public Weapon weapon_right;

    //�и�
    bool isParrying;

    //�˹�
    public float knockbackForce = 5.0f;
    public float knockbackTime = 0.5f;
    bool isKnockback;

    Animator anim;
    Rigidbody rigid;
    PlayerState state;

    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        state = GetComponent<PlayerState>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        if (state.hp <= 0) { Death(); }
        attack1();
        attack2();
        strongAttack();
        hit();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        rDown = Input.GetKey(KeyCode.LeftShift);//leftshift _ ���߿� ������ �ٲ�� �Ԥ���
        jDown = Input.GetKeyDown(KeyCode.Space);//spacebar
        left_attack = Input.GetMouseButtonDown(0);
        right_attack = Input.GetMouseButtonDown(1);
        strong_attack  = Input.GetMouseButtonDown(2);
        //if(left_attack && right_attack) {strong_attack = true;}       //�̷��ϱ� ��� ���� �ٺ��ȴ�. �ٽ� ���� ��.
    }

    void Move()
    {

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        if(isJump){
             moveVec = jumpVec;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("death3") || isAttack || anim.GetCurrentAnimatorStateInfo(0).IsName("dodge")|| isKnockback)
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
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }

    void attack1()          //���� �׳� �ڷ�ƾ���� �ٲܱ�? ��
    {

        attackDelay += Time.deltaTime;
        isAttackReady = weapon_left.rate < attackDelay;

        if (left_attack && isAttackReady && !isJump && !isDeath && !isAttack)
        {
            StopCoroutine(coAttack1());
            StartCoroutine(coAttack1());
        }
    }

    IEnumerator coAttack1()         //��Ŭ�� ���� /�� �ڷ�ƾ ������ ���� �ð� �����ϰ� Use�� �ش� ������ ���� Ʈ���� ���� �ð��� �ѱ���.
    {
        isAttack = true;
        anim.SetTrigger("doLattack");
        weapon_left.Use(1.05f);         //�� ���� ���ÿ� ���� ������
        weapon_right.Use(1.05f);
        attackDelay = 0;                //attack�����̰� ���� �������ڸ��� 0�Ǵ°� �´°�???
        yield return new WaitForSeconds(2.0f);
        attackOut();
    }


    void attack2()
    {
        if (right_attack && isAttackReady && !isJump && !isDeath && !isAttack)
        {
            StopCoroutine(coAttack2());
            StartCoroutine(coAttack2());
        }
    }

    IEnumerator coAttack2()         //��Ŭ�� ���� /�� �ڷ�ƾ ������ ���� �ð� �����ϰ� Use�� �ش� ������ ���� Ʈ���� ���� �ð��� �ѱ���.
    {
        isAttack = true;
        anim.SetTrigger("doRattack");
        weapon_right.Use(0.15f);
        attackDelay = 0;
        yield return new WaitForSeconds(2.0f);
        attackOut();
    }


    void strongAttack()
    {
        if (strong_attack && isAttackReady && !isJump && !isDeath && !isAttack)
        {
            StopCoroutine(coStrongAttack());
            StartCoroutine(coStrongAttack());
        }
    }

    IEnumerator coStrongAttack()
    {
        isAttack = true;
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
        attackDelay = 0;
        yield return new WaitForSeconds(0.6f);
        attackOut();
    }

        void attackOut()
    {
        isAttack = false;
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            anim.SetBool("isJump", false);
            StopCoroutine(IsJumpFalse());
            StartCoroutine(IsJumpFalse());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
    public void Parrying()
    {
        anim.SetTrigger("doDodge");
    }


    public void Knockback(Vector3 enemyVec)
    {
        isKnockback = true;
        StartCoroutine(OnKnockback(enemyVec));
    }

    IEnumerator IsJumpFalse()
    {
        yield return new WaitForSeconds(0.7f);
        isJump = false;
    }

    IEnumerator OnKnockback(Vector3 enemyVec)
    {
        float startTime = Time.time;
        Vector3 reactVec = (transform.position - enemyVec).normalized;
        Debug.Log(reactVec);
        rigid.AddForce(reactVec * knockbackForce, ForceMode.Impulse);
        while (Time.time < startTime + knockbackTime)
        {
            yield return null;
        }
        isKnockback = false;
    }




}
