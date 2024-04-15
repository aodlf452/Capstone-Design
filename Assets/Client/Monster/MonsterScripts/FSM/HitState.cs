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
        //�ǰ� �ִϸ��̼� ���� - ���� �ִϸ��̼� ����
        monster.Anim.SetTrigger("getHit");
    }
    
    //2. �ݺ� ����
    public void ExecuteState()
    {
        //�������
    }

    public void ExitState()
    {
        //�������
    }
}
