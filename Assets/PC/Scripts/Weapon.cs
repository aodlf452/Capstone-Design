using System.Collections;
using System.Collections.Generic;
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
    public void Use()
    {
        if(type == WeaponType.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
            
        }
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f);          //�ٸ� ��� �ʿ��� ����
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
