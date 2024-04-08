using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEffect : StatusEffect
{
    Monster monster;
    Player player;
    public override void OnStart()
    {
        base.OnStart();
        monster = gameObject.GetComponent<Monster>(); // ���� ������Ʈ
        if(monster != null)
        {
            monster.Agent.isStopped = true; // NavMeshAgent �̵� ����
        }

        player = gameObject.GetComponent<Player>();
        if(player != null )
        {
        }
    }
    public override void OnExit()
    {
        base.OnExit();

        if(monster != null)
        {
            monster.Agent.isStopped = false; // NavMeshAgent �̵� Ȱ��ȭ
        }

    }
}
