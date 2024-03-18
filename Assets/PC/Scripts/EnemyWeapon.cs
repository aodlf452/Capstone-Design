using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public enum WeaponType { Melee, Range };
    public WeaponType type;
    public int weapon_damage=30; //���⺰ ���ݷ�
    public float weapon_rate; // ���⺰ ���� �ӵ�
    public BoxCollider meleeArea;   //������ ���� ���� ����
    //public TrailRenderer trailEffect; //���ݽ� ���� ����Ʈ
    private HashSet<GameObject> hitEnemies = new HashSet<GameObject>();


    private void Awake()
    {
        
    }
    public void Use(float attackEndTime)
    {
        if (type == WeaponType.Melee)
        {
            StopCoroutine(Weapon_Activation(attackEndTime));
            hitEnemies.Clear();                         //HashSet �ʱ�ȭ, ������ ���Ӱ� ���۵� �� ���� �ʱ�ȭ.
            Debug.Log("HashSet Ŭ����");
            StartCoroutine(Weapon_Activation(attackEndTime));
        }

        if (type == WeaponType.Range)
        {
            StopCoroutine(Weapon_Activation(attackEndTime));
            hitEnemies.Clear();                         //HashSet �ʱ�ȭ, ������ ���Ӱ� ���۵� �� ���� �ʱ�ȭ.
            Debug.Log("HashSet Ŭ����");
            StartCoroutine(Weapon_Activation(attackEndTime));
        }
    }

    IEnumerator Weapon_Activation(float attackEndTime)
    {
        meleeArea.enabled = true;
        //trailEffect.enabled = true;
        yield return new WaitForSeconds(attackEndTime);
        meleeArea.enabled = false;
        yield return new WaitForSeconds(0.2f);
        //trailEffect.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject enemy = other.gameObject;
            Player enemyDamage = enemy.GetComponent<Player>();
            if (!hitEnemies.Contains(enemy)) // �̹� ������ ���� �ƴ϶��
            {
                hitEnemies.Add(enemy); 
                enemyDamage.TakeDamage((20 + weapon_damage));
            }
        }
    }


}
