using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public enum WeaponType { Melee, Range };
    public WeaponType type;
    public int weapon_damage; //���⺰ ���ݷ�
    public float weapon_rate; // ���⺰ ���� �ӵ�
    public BoxCollider meleeArea;   //������ ���� ���� ����
    public TrailRenderer trailEffect; //���ݽ� ���� ����Ʈ
    private HashSet<GameObject> hitEnemies = new HashSet<GameObject>();

    public Player player;
    public PlayerStatus status;
    public CameraShake cameraShaking;
    public AttackController attackController;


    //�и�
    public GameObject parryingParticle;     //�и� ��ƼŬ ������
    float parryingCooldown = 3.0f;          //�и� ��Ÿ��
    bool canPrrying = true;
    public Transform parryingPos;

    private void Awake()
    {
        
        player = GetComponentInParent<Player>();
        status = GetComponentInParent<PlayerStatus>();
        attackController = GetComponentInParent<AttackController>();
        cameraShaking =Camera.main.GetComponent<CameraShake>();
        
    }
    public void Use()
    {
        if (type == WeaponType.Melee)
        {
            StopCoroutine(Weapon_Activation());
            hitEnemies.Clear();                         //HashSet �ʱ�ȭ, ������ ���Ӱ� ���۵� �� ���� �ʱ�ȭ.
            Debug.Log("HashSet Ŭ����");
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
        trailEffect.enabled = false;
    }

    IEnumerator Weapon_Activation()
    {
        meleeArea.enabled = true;
        trailEffect.enabled = true;
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ParryingBox" && canPrrying)
        {
            attackController.Parrying();
            StartCoroutine(Parrying());
        }

        if (other.tag == "MonsterEnemy"|| other.tag == "Enemy")
        {
            GameObject enemy = other.gameObject;
            Monster enemyDamage = enemy.GetComponent<Monster>();
            if (!hitEnemies.Contains(enemy)) // �̹� ������ ���� �ƴ϶��
            {
                hitEnemies.Add(enemy); // �� ���� ������ �� ��Ͽ� �߰� //enemyDamage.curHP -= damage;//++ ���⿡ enemy���� ������ �����ϴ� ���� �߰� //if (hitEnemies.Contains(enemy))    {Debug.Log("�߰���");  }
                enemyDamage.TakeDamage((status.basicStats.atk + weapon_damage), transform.position);
                cameraShaking.Shaking();
            }
        }
    }

    IEnumerator Parrying()
    {
        canPrrying = false;
        yield return new WaitForSeconds(0.2f);              //�ֵη�� ����� ������ ���� �� �ֵ��� ������
        //attackController.Parrying();                      //������ �ʾ ��
        cameraShaking.Shaking();

        GameObject effectInastantiate = Instantiate(parryingParticle,parryingPos.position, Quaternion.identity);
        Destroy(effectInastantiate, 1.0f);                  //1�� �̻� x
        yield return new WaitForSeconds(parryingCooldown);
        canPrrying = true;

    }

}
