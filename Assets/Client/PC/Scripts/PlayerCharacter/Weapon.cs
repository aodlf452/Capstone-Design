using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;
using Photon.Pun;

public class Weapon : MonoBehaviourPun
{

    public enum WeaponType { Melee, Range };
    public WeaponType type;
    public int weapon_damage; //���⺰ ���ݷ�
    public float weapon_rate; // ���⺰ ���� �ӵ�
    public BoxCollider meleeArea;   //������ ���� ���� ����
    //public TrailRenderer trailEffect; //���ݽ� ���� ����Ʈ
    private HashSet<GameObject> hitEnemies = new HashSet<GameObject>();
    public bool isHeavyAttack = false;

    public Player player;
    public PlayerStatus status;
    public CameraShake cameraShaking;
    public AttackController attackController;

    public GameObject effectPrefab;//���� ����Ʈ ������
    public GameObject shieldEffectPrefab;//��� ����Ʈ ������
    public GameObject strongEffectPrefab;//�ʻ�� ����Ʈ ������
    public GameObject hitEffectPrefab; //Ÿ�ݽ� ����Ʈ ������
    GameObject nEffectPrefab;
    public Transform HandEffect;       //�ڽ� ������Ʈ�� �ڵ� ����Ʈ ������ �Ҵ��ؾ� ��
    public bool isShield=false;



    //�и�
    public GameObject parryingParticle;     //�и� ��ƼŬ ������
    float parryingCooldown = 3.0f;          //�и� ��Ÿ��
    bool canPrrying = true;
    public Vector3 parryingPos;

    private void Awake()
    {

        player = GetComponentInParent<Player>();
        status = GetComponentInParent<PlayerStatus>();
        attackController = GetComponentInParent<AttackController>();
        cameraShaking = Camera.main.GetComponent<CameraShake>();
        if(HandEffect!=null)    HandEffect.gameObject.SetActive(false);
    }

    private void Start()
    {

    }
    public void Use()
    {
        if (type == WeaponType.Melee)
        {
            StopCoroutine(Weapon_Activation());
            hitEnemies.Clear();                         //HashSet �ʱ�ȭ, ������ ���Ӱ� ���۵� �� ���� �ʱ�ȭ.
            //Debug.Log("HashSet Ŭ����");
            StartCoroutine(Weapon_Activation());
        }

        if (type == WeaponType.Range)
        {
            StopCoroutine(Weapon_Activation());
            hitEnemies.Clear();                         //HashSet �ʱ�ȭ, ������ ���Ӱ� ���۵� �� ���� �ʱ�ȭ.
            Debug.Log("HashSet Ŭ����");
            StartCoroutine(Weapon_Activation());
        }
    }
    public void AttackOut()
    {
        meleeArea.enabled = false; 
        isHeavyAttack = false;                          //������ Out
    }


    IEnumerator Weapon_Activation()
    {
        meleeArea.enabled = true;
        Debug.Log("����");  
        yield return null;
    }


    public void EffectInstance(bool reverse)                
    {
        
        if (effectPrefab == null)
        {
            return;
        }

        if (reverse)
        {
            GameObject effectInstance = Instantiate(effectPrefab, transform.position, transform.rotation);
            PrefabCreator info = effectInstance.AddComponent<PrefabCreator>();//������ �����Ǹ� ������ ������Ʈ(�÷��̾� ĳ����)�� transform �޾ƿ���
            info.isStrong = attackController.stepupBuffer;                                 //��ؽ� ����. ��ؽ� ��ȭ �� ���� üũ
            info.attackNum = CurAttackKey();
            info.weapon = this;
            Destroy(effectInstance, 1.0f);
        }
        if (!reverse)
            {
            Quaternion reverseRoation = Quaternion.Euler(0, 0, 180);
            GameObject effectInstance = Instantiate(effectPrefab, transform.position, transform.rotation* reverseRoation);
            PrefabCreator info = effectInstance.AddComponent<PrefabCreator>();//������ �����Ǹ� ������ ������Ʈ(�÷��̾� ĳ����)�� transform �޾ƿ���
            info.isStrong = attackController.stepupBuffer;                                //��ؽ� ����. ��ؽ� ��ȭ �� ���� üũ
            info.attackNum = CurAttackKey();
            info.weapon = this;
            Destroy(effectInstance, 1.0f);
        }
    }

    public void StrongEffectInstance()
    {
        GameObject effectInstance = Instantiate(strongEffectPrefab, (transform.position), transform.rotation);
        Destroy(effectInstance, 3.0f);
    }

    public void ShieldEffectInstance()
    {
        if (shieldEffectPrefab == null)
        {
            return;                                   
        }

        if (!isShield)
        {
            Quaternion reverseRoation = Quaternion.Euler(0, 1, 0);
            nEffectPrefab = Instantiate(shieldEffectPrefab, transform.position + new Vector3(0.0f,0.0f,-1.0f),transform.rotation* reverseRoation);
            PrefabCreator info = nEffectPrefab.AddComponent<PrefabCreator>();//������ �����Ǹ� ������ ������Ʈ(�÷��̾� ĳ����)�� transform �޾ƿ�
            info.creatorParentTransform = player.transform;                  //���� �÷��̾� �������� ȸ��
            isShield = true;
        }           
    }
    public void ShieldEffectOut()
    {
        if(nEffectPrefab != null)
        {
            Destroy(nEffectPrefab);
            nEffectPrefab = null;
            isShield = false;
        }
    }

    //���� ���� Ʈ����
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyWeapon" && canPrrying)
        {
            parryingPos = other.ClosestPointOnBounds(transform.position);
            attackController.Parrying();
            StartCoroutine(Parrying());
        }

        if (other.tag == "MonsterEnemy" || other.tag == "Enemy" || other.tag == "Player")
        {
            GameObject enemy = other.gameObject;

            if (other.tag == "MonsterEnemy")
            {
                Monster enemyDamage = enemy.GetComponent<Monster>();
                if (!hitEnemies.Contains(enemy)) // �̹� ������ ���� �ƴ϶��
                {
                    hitEnemies.Add(enemy); // �� ���� ������ �� ��Ͽ� �߰� //enemyDamage.curHP -= damage;//++ ���⿡ enemy���� ������ �����ϴ� ���� �߰� //if (hitEnemies.Contains(enemy))    {Debug.Log("�߰���");  }
                    enemyDamage.TakeDamage((status.basicStats.atk + weapon_damage), transform.position);
                    GameObject hiteffectInstance = Instantiate(hitEffectPrefab, other.ClosestPointOnBounds(transform.position), Quaternion.identity);
                    Destroy(hiteffectInstance, 0.5f);
          
                    if (isHeavyAttack) //�������� ��� �ǰ� ���� �ִϸ��̼� ó��
                    {
                        enemyDamage.HitResponse();
                    }
                }
            }
            else if (other.tag == "Player"&& !(other.gameObject.GetComponent<PhotonView>().IsMine))
            {
                CombatStatusManager enemyDamage = enemy.GetComponent<CombatStatusManager>();
                if (!hitEnemies.Contains(enemy)) // �̹� ������ ���� �ƴ϶��
                {
                    hitEnemies.Add(enemy); // �� ���� ������ �� ��Ͽ� �߰� //enemyDamage.curHP -= damage;//++ ���⿡ enemy���� ������ �����ϴ� ���� �߰� //if (hitEnemies.Contains(enemy))    {Debug.Log("�߰���");  }
                    enemyDamage.TakeDamage((status.basicStats.atk + weapon_damage));
                    GameObject hiteffectInstance = Instantiate(hitEffectPrefab, other.ClosestPointOnBounds(transform.position), Quaternion.identity);
                    Destroy(hiteffectInstance, 0.5f);

                    if (isHeavyAttack) //���⼭ HeavyAttack false�� �ϸ�, ���� �ǰ� ������ ���� �ȵ�
                    {
                        enemyDamage.HitResponse();
                    }
                }
            }
            else return;
        }

        else return;
    }

    IEnumerator Parrying()
    {
        canPrrying = false;
        yield return new WaitForSeconds(0.2f);              //�ֵη�� ����� ������ ���� �� �ֵ��� ������
        //attackController.Parrying();                      //������ �ʾ ��
        //cameraShaking.Shaking();

        GameObject effectInastantiate = Instantiate(parryingParticle,parryingPos, Quaternion.identity);
        Destroy(effectInastantiate, 1.0f);                  //1�� �̻� x
        yield return new WaitForSeconds(parryingCooldown);
        canPrrying = true;

    }


    private int CurAttackKey()
    {
        Animator anim = player.anim;
        int curAttack;

        if (anim != null && anim.GetCurrentAnimatorStateInfo(0).IsName("Combi1")) {return curAttack = 1; }
        else if (anim != null && anim.GetCurrentAnimatorStateInfo(0).IsName("Combi2")) { return curAttack = 2; }
        else if (anim != null && anim.GetCurrentAnimatorStateInfo(0).IsName("Combi3")) { return curAttack = 3; }
        else if (anim != null && anim.GetCurrentAnimatorStateInfo(0).IsName("SingleRightAttack")) { return curAttack = 4; }
        else if (anim != null && anim.GetCurrentAnimatorStateInfo(0).IsName("JumpAttack")) { return curAttack = 5; }
        else { return curAttack = 0; }
    }

    public HashSet<GameObject> GethitEnemeies()
    {
        return hitEnemies;
    }

    public void AddToHitEnemeies(GameObject enemy)
    {
        hitEnemies.Add(enemy);
    }

}
