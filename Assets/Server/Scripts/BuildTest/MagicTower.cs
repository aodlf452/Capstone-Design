using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTower : MonoBehaviour
{
    [SerializeField]
    private float detectionRadius = 5f; // ���� ���� ����
    private Vector3 lastMonsterPosition; // ���������� �߰ߵ� ������ ��ġ
    private bool hasMonsterPosition = false; // ������ ��ġ�� ������ �ִ��� ����
    [SerializeField]
    private float cooldown = 3f; // ���� �߻� ��Ÿ��
    private float nextFireTime = 0f; // ���� ���� �߻� �ð�
    private bool isCoolingDown = false; // ��Ÿ�� ������ ����

    void Start()
    {

    }



    void Update()
    {
        if (!isCoolingDown && !hasMonsterPosition)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position - new Vector3(0, 4, 0), detectionRadius);

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {


                    lastMonsterPosition = collider.transform.position + new Vector3(0, 1, 0);
                    transform.LookAt(lastMonsterPosition);
                    hasMonsterPosition = true;
                    StartCooldown(); //��Ÿ��
                    break;
                }
            }
        }

        if (Time.time >= nextFireTime && hasMonsterPosition)
        {
            ShootMagic();
            hasMonsterPosition = false; //��ġ �ʱ�ȭ
        }
    }

    void ShootMagic()
    {



        var bullet = ObjectPool.GetObject(transform);



        nextFireTime = Time.time + cooldown;
        return;


    }

    void StartCooldown()
    {
        isCoolingDown = true;
        Invoke("EndCooldown", cooldown);
    }

    void EndCooldown()
    {

        isCoolingDown = false;
    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, 4, 0), detectionRadius);
    }
}