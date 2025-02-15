﻿using System;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : MonoBehaviour, IMonsterState
{
    Monster monster;
    private float timer;
    private float wanderTimer = 5f;
    public IdleState(Monster monster)
    {
        this.monster = monster;
        timer = 0f;
    }

    public void EnterState()
    {
        Debug.Log("Idle: Enter");
        
        if(monster.TargetPlayer != null)
        {
            Debug.Log($"{monster.TargetPlayer.name}");
        }
        monster.TargetPlayer = null; // 버전 9 추가: Idle 진입 시에 플레이어 타겟 초기화
        monster.Agent.isStopped = false; // Agent 활성화

        // Chase -> Idle 전환 시 타게팅 하고있던 플레이어를 잠시동안 따라올 수 있으므로
        // 뭐가 더 자연스러운지 판단 필요
        Vector3 newPos = SetRandomPosInSpawnPointRange(monster.SpawnPoint, monster.WanderRadius, -1);
        monster.Agent.SetDestination(newPos);
    }

    public void ExitState()
    {
        Debug.Log("Idle: Exit");
        monster.Agent.isStopped = true; // Agent 비활성화
        monster.Anim.SetBool("Walk", false);
    }

    public void ExecuteState()
    {
        monster.Anim.SetBool("Walk", (monster.Agent.velocity.magnitude > 0.05f) ? true : false);
        timer += Time.deltaTime;
        if (timer >= wanderTimer)
        {
            Debug.Log("Idle: Execute - 위치 전환");
            Vector3 newPos = SetRandomPosInSpawnPointRange(monster.SpawnPoint, monster.WanderRadius, -1);
            monster.Agent.SetDestination(newPos);
            timer = 0f;
        }
    }

    private Vector3 SetRandomPosInSpawnPointRange(Vector3 spawnPoint, float range, int layermask)
    {
        // 1. 범위내 랜덤 구 위치 벡터 
        Vector3 randDir = UnityEngine.Random.insideUnitSphere * range;
        randDir += spawnPoint; // 스폰 원점 벡터 더하기 

        NavMeshHit navHit;
        // randDir 주변 네비게이션 메쉬 위 가장 가까운 점 찾아 navHit에 저장
        NavMesh.SamplePosition(randDir, out navHit, range, layermask); // layermask가 1이면 모든 레이어 대상

        return navHit.position;
    }
 
}