using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    public enum WeaponType {Melee,Range};
    public WeaponType type;
    public int damage; //atk
    public float rate;   //���ݼӵ�
    public BoxCollider meleeArea;   //���ݹ���
    public TrailRenderer trailEffect; //��������Ʈ


    //private HashSet<GameObject> alreadyHitObjects;

    private HashSet<GameObject> hitEnemies = new HashSet<GameObject>();

    //�и�
    public GameObject parryingParticle;     //�и� ��ƼŬ ������
    bool canPrrying = true;                 
    float parryingCooldown = 3.0f;          //�и� ��Ÿ��
    ParryingEffect parryingEffect = null;

    private void Awake()
    {
        parryingEffect = GetComponent<ParryingEffect>();
        //alreadyHitObjects=new HashSet<GameObject>();
    }
    public void Use(float attackEndTime)
    {
        if(type == WeaponType.Melee)
        {
            StopCoroutine(Swing(attackEndTime));
            hitEnemies.Clear();                         //HashSet �ʱ�ȭ, ������ ���Ӱ� ���۵� �� ���� �ʱ�ȭ.
            Debug.Log("HashSet Ŭ����");
            StartCoroutine(Swing(attackEndTime));
            
        }
    }

    IEnumerator Swing(float attackEndTime)
    {
        meleeArea.enabled = true;
        trailEffect.enabled = true;
        //Debug.Log("Swinging"+ System.DateTime.Now.ToString("HH:mm:ss.fff"));
        yield return new WaitForSeconds(attackEndTime);          //�޾ƿ� ���� �ð����� >> ���� �ִϸ��̼� ���� �����ؾ� �� ��
        meleeArea.enabled = false;
        yield return new WaitForSeconds(0.2f);
        trailEffect.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ParryingBox"&&canPrrying)
        {
            StartCoroutine(Parrying());
        }

        if (other.tag == "Enemy")
        {
            GameObject enemy = other.gameObject;
            Enemy enemyDamage = enemy.GetComponent<Enemy>();
            if (!hitEnemies.Contains(enemy)) // �̹� ������ ���� �ƴ϶��
            {
                hitEnemies.Add(enemy); // �� ���� ������ �� ��Ͽ� �߰� //enemyDamage.curHP -= damage;//++ ���⿡ enemy���� ������ �����ϴ� ���� �߰� //if (hitEnemies.Contains(enemy))    {Debug.Log("�߰���");  }
                enemyDamage.OnDamage(damage, transform.position);
            }
        }
    }

    IEnumerator Parrying()
    {
        canPrrying = false;

        yield return new WaitForSeconds(0.3f);              //�ֵη�� ����� ������ ���� �� �ֵ��� ������
        Player player = GetComponentInParent<Player>();
        player.Parrying();
        if (parryingEffect != null)
        {
            StartCoroutine(parryingEffect.ShakeCamera());
        }

        GameObject effectInastantiate = Instantiate(parryingParticle,transform.position, Quaternion.identity);
        Destroy(effectInastantiate, 1.0f);                  //1�� �̻� x

        yield return new WaitForSeconds(parryingCooldown);
        canPrrying=true;
    }

}
