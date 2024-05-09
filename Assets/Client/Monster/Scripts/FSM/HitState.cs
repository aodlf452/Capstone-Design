using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : MonoBehaviour, IMonsterState
{
    private Monster monster;
    public HitState(Monster monster)
    {
        this.monster = monster;
    }

    // 1. ���� ���� �� 1ȸ ����
    public void EnterState()
    {
        //�ǰ� �ִϸ��̼� ����
        monster.Anim.SetTrigger("getHit");
        switch (monster.Type)
        {
            case Monster.MonsterType.Cyclops:
                monster.Anim.SetInteger("randomValue", Random.Range(0, 2));
                break;
            case Monster.MonsterType.Hobgoblin:
                monster.Anim.SetInteger("randomValue", Random.Range(0, 2));
                break;
            case Monster.MonsterType.Troll:
                monster.Anim.SetInteger("randomValue", Random.Range(0, 2));
                break;
        }
        Debug.Log("�ǰ� ����");

    }
    
    //2. �ݺ� ����
    public void ExecuteState()
    {
        //�������
    }

    public void ExitState()
    {
        //�������
        Debug.Log("�ǰ� Ż��");
    }
}
