using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �ش� ������Ʈ�� �����̻� ������Ʈ
 * �̸� ������Ʈ�� �Ҵ��صδ� ���̾ƴ� �������� �Ҵ��� �������� ����
 */
public abstract class StatusEffect : MonoBehaviour
{

    public float Duration
    {
        get { return duration; }
        set { duration = value; }
    }

    protected float duration; // ���� �ð�
    private float currentTime = 0f; // ���� �ð�

    // ó�� �����̻� ������ �� ȣ��
    public virtual void OnStart()
    {

    }
    // �����̻� ���� �ð� ���� ȣ��
    public virtual void OnUpdate()
    {

    }
    // �����̻� ���� �ð� ���� �� ȣ��
    public virtual void OnExit()
    {

    }
    // ���� �̻� ���� �� ȣ��Ǵ� �Լ�
    private void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime < duration) // ���ӽð� ��
        {
            OnUpdate();
        }
        else // ���ӽð� ��
        {
            OnExit();
            Destroy(this); // StatusEffect ������Ʈ ����
        }
    }
}
