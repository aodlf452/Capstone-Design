using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinStrongEffectTransfrom : MonoBehaviour
{
    
    private HashSet<GameObject> hitEnemies = new HashSet<GameObject>();
    private PrefabCreator prefabCreator;
    private BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        prefabCreator = GetComponentInParent<PrefabCreator>();
        transform.rotation=prefabCreator.weapon.player.transform.rotation;
        boxCollider= GetComponent<BoxCollider>();
    }
    
    private void Update()
    {
        transform.position += transform.forward*0.3f;
        transform.position += transform.up * -0.1f;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MonsterEnemy" || other.tag == "Enemy" || other.tag == "Player")
        {
            GameObject enemy = other.gameObject;

            if (other.tag == "MonsterEnemy")
            {
                Monster enemyDamage = enemy.GetComponentInParent<Monster>();
                if (!hitEnemies.Contains(enemy)) // �̹� ������ ���� �ƴ϶��
                {
                    hitEnemies.Add(enemy); // �� ���� ������ �� ��Ͽ� �߰� //enemyDamage.curHP -= damage;//++ ���⿡ enemy���� ������ �����ϴ� ���� �߰� //if (hitEnemies.Contains(enemy))    {Debug.Log("�߰���");  }
                    enemyDamage.TakeDamage((prefabCreator.result_damage));
                    enemyDamage.HitResponse();
                }
            }
            else if (other.tag == "Player")
            {
                if (enemy == prefabCreator.weapon.player.gameObject) { return; }
                CombatStatusManager enemyDamage = enemy.GetComponent<CombatStatusManager>();
                if (!hitEnemies.Contains(enemy)) // �̹� ������ ���� �ƴ϶��
                {
                    hitEnemies.Add(enemy); // �� ���� ������ �� ��Ͽ� �߰� //enemyDamage.curHP -= damage;//++ ���⿡ enemy���� ������ �����ϴ� ���� �߰� //if (hitEnemies.Contains(enemy))    {Debug.Log("�߰���");  }
                    enemyDamage.TakeDamage((prefabCreator.result_damage));
                    enemyDamage.HitResponse();
                }
            }

            else if (other.tag == "Enemy")
            {

                Hit_Test_v2 enemyDamage = enemy.GetComponent<Hit_Test_v2>();
                if (!hitEnemies.Contains(enemy)) // �̹� ������ ���� �ƴ϶��
                {
                    hitEnemies.Add(enemy); // �� ���� ������ �� ��Ͽ� �߰� //enemyDamage.curHP -= damage;//++ ���⿡ enemy���� ������ �����ϴ� ���� �߰� //if (hitEnemies.Contains(enemy))    {Debug.Log("�߰���");  }
                    if (prefabCreator == null) Debug.Log("������ũ�������� null");
                    enemyDamage.TakeDamage((prefabCreator.result_damage));
                }
            }
            else return;
        }
    }
}
