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

    //�и�
    public GameObject parryingParticle;     //�и� ��ƼŬ ������
    float parryingCooldown = 3.0f;          //�и� ��Ÿ��
    bool canPrrying = true;
    ParryingEffect parryingEffect = null;

    private void Awake()
    {
        parryingEffect = GetComponent<ParryingEffect>();
        player = GetComponentInParent<Player>();
        status = GetComponentInParent<PlayerStatus>();
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
            StartCoroutine(Parrying());
        }

        if (other.tag == "MonsterEnemy")
        {
            GameObject enemy = other.gameObject;
            Monster enemyDamage = enemy.GetComponent<Monster>();
            if (!hitEnemies.Contains(enemy)) // �̹� ������ ���� �ƴ϶��
            {
                hitEnemies.Add(enemy); // �� ���� ������ �� ��Ͽ� �߰� //enemyDamage.curHP -= damage;//++ ���⿡ enemy���� ������ �����ϴ� ���� �߰� //if (hitEnemies.Contains(enemy))    {Debug.Log("�߰���");  }
                enemyDamage.TakeDamage((status.basicStats.atk + weapon_damage), transform.position);
            }
        }
    }

    IEnumerator Parrying()
    {
        canPrrying = false;
        yield return new WaitForSeconds(0.2f);              //�ֵη�� ����� ������ ���� �� �ֵ��� ������
        player.Parrying();
        if (parryingEffect != null)
        {
            StartCoroutine(parryingEffect.ShakeCamera());
        }
        GameObject effectInastantiate = Instantiate(parryingParticle, transform.position, Quaternion.identity);
        Destroy(effectInastantiate, 1.0f);                  //1�� �̻� x
        yield return new WaitForSeconds(parryingCooldown);
        canPrrying = true;
    }

}
