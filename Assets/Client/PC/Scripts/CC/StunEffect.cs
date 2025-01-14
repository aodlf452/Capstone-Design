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
        monster = gameObject.GetComponent<Monster>(); // 몬스터 컴포넌트
        if(monster != null)
        {
            monster.Agent.isStopped = true; // NavMeshAgent 이동 정지
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
            monster.Agent.isStopped = false; // NavMeshAgent 이동 활성화
        }

    }
}
