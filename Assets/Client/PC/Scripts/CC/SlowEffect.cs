using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : StatusEffect
{
    private float speedRate = 0.5f;
    private float originSpeed;

    public override void OnStart()
    {
        base.OnStart();
        // ���ο� ����Ʈ �ο�

        // ����� �̵����� ������Ʈ�� ������ ����

        // originSpeed ����

        // ����� �̵��ӵ� *= speedRate


    }
    public override void OnExit()
    {
        base.OnExit();
        // ���ο� ����Ʈ ����

        // ����� �̵��ӵ� = originSpeed
    }
}
