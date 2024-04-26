using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : MonoBehaviour, IMonsterState
{

    private Monster monster;
    private int randomValue;
    public HitState(Monster monster)
    {
        this.monster = monster;
    }


    // 1. ���� ���� �� 1ȸ ����
    public void EnterState()
    {
        //�ǰ� �ִϸ��̼� ���� - ���� �ִϸ��̼� ����
        randomValue = Random.Range(0, 1);
        monster.Anim.SetTrigger("getHit");
        monster.Anim.SetInteger("randomValue", randomValue);
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
