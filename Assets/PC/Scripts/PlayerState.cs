using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public int hp = 100;
    public int def = 10;
    public int atk = 30;

    Rigidbody rigid;
    Player player;

    private void Awake()
    {
        player = GetComponent<Player>();  
        rigid = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Knockback") // ����
        {
            //hp--=Enemy.~~ ��� ������ ���ݷ±������� hp����
            Vector3 attackEmemyVec = other.transform.position;
            player.Knockback(attackEmemyVec);

        }
    }
  
}
