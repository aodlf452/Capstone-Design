using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorStrongEffectTrasnform : MonoBehaviour
{

    new BoxCollider collider;
    float targetCenterZ = 14f;
    float duration = 1f;

    Vector3 initialCenter;
    float startTime;
    // Start is called before the first frame update

    private HashSet<GameObject> hitEnemies = new HashSet<GameObject>();
    private PrefabCreator prefabCreator;
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        prefabCreator = GetComponentInParent<PrefabCreator>();
        initialCenter = collider.center;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float timeElapsed = Time.time - startTime;
        float progress = timeElapsed / duration;

        if (progress < 1.0f)
        {
            Vector3 newCenter = collider.center;
            newCenter.z = Mathf.Lerp(initialCenter.z, targetCenterZ, progress);
            collider.center = newCenter;
        }
        else if (collider.center.z != targetCenterZ)
        {
            // ��ǥ ��ġ�� ��Ȯ�� ���߱�
            Vector3 finalCenter = collider.center;
            finalCenter.z = targetCenterZ;
            collider.center = finalCenter;
        }
    }


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
                    if(prefabCreator==null)Debug.Log("������ũ�������� null");
                    enemyDamage.TakeDamage((prefabCreator.result_damage));
                }
            }
            else return;
        }
    }

}
