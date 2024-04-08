using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : StatusEffect
{
    private float damagePerSecond = 5f; // �ʴ� ������
    private float damageInterval = 1f; // ������ ����(1��)
    private float lastDamageTime = 0f; // ������ ������ �� �ð�


    public override void OnStart()
    {
        base.OnStart();
        lastDamageTime = Time.time; // ���� �ð� ��� �� ����
        // �� ����Ʈ �ο�

        
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if(Time.time  - lastDamageTime > damageInterval) // ����ð� - ������ ������ �ð� > ������ ���� �ð�
        {
            // �������� �ο�
            ApplyPoison();
            lastDamageTime = Time.time;
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        // �� ����Ʈ ����
    }
    private void ApplyPoison()
    {
        // status ������Ʈ�� ü���� �����ͼ� ���
        //gameObject.GetComponent
    }
}
