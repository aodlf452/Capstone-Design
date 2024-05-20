using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrowthSystem : MonoBehaviour
{
    private int soulCount;

    private PlayerStatus status;
    private int hpLevel;
    private int defLevel;
    private int atkLevel;
    private int dexLevel;
    private int intLevel;
    private Dictionary<int, int> soulPerLevel;

    public delegate void SoulChangedHandler(string count);
    public static event SoulChangedHandler OnSoulChanged; // �������� event�� ���� �ʱ�ȭ �Ǿ��־�� ��, static�̶� ���� ����
    private void Awake()
    {
        hpLevel = 1;
        defLevel = 1;
        atkLevel = 1;
        dexLevel = 1;
        intLevel = 1;

        soulCount = 500;

        soulPerLevel = new Dictionary<int, int>()
        {
            {1,5}, {2,10}, {3,15}, {4,20}, {5,25},
            {6,30}, {7,35}, {8,40}, {9,45}, {10,50},
        };
    }
    private void Start()
    {
        status = GetComponent<PlayerStatus>();
        Debug.Log("GrowthSystem ID: " + GetInstanceID());
        OnSoulChanged(soulCount.ToString()); // �ʱ� �ҿ� ���� �̺�Ʈ �߻�
    }
    /// <summary>
    /// Soul ȹ��ó�� �� UI ǥ��
    /// </summary>
    public void GetSoul(int count)
    {
        soulCount += count;
        OnSoulChanged(soulCount.ToString()); // UI ������Ʈ �̺�Ʈ �߻�
    }
    public void OnPressEnhanceHealthBtn()
    {
        Debug.Log("����:"+ soulPerLevel.Count);
        if(hpLevel < 10 && soulCount >= soulPerLevel[hpLevel])
        {
            soulCount -= soulPerLevel[hpLevel];
            hpLevel++;
            status.IncreaseMaxHealth();
            OnSoulChanged(soulCount.ToString()); // UI ������Ʈ �̺�Ʈ �߻�
        }
    }
    public void OnPressEnhanceDefenseBtn()
    {
        if (defLevel < 10 && soulCount >= soulPerLevel[defLevel])
        {
            soulCount -= soulPerLevel[defLevel];
            defLevel++;
            status.IncreaseDefense();
            OnSoulChanged(soulCount.ToString()); // UI ������Ʈ �̺�Ʈ �߻�
        }
    }
    public void OnPressEnhanceAttackBtn()
    {
        if (atkLevel < 10 && soulCount >= soulPerLevel[atkLevel])
        {
            soulCount -= soulPerLevel[atkLevel];
            atkLevel++;
            status.IncreaseAttack();
            OnSoulChanged(soulCount.ToString()); // UI ������Ʈ �̺�Ʈ �߻�
        }
    }
    public void OnPressEnhanceDexBtn()
    {
        if (dexLevel < 10 && soulCount >= soulPerLevel[dexLevel])
        {
            soulCount -= soulPerLevel[dexLevel];
            dexLevel++;
            status.IncreaseDex();
            OnSoulChanged(soulCount.ToString()); // UI ������Ʈ �̺�Ʈ �߻�
        }
    }
    public void OnPressEnhanceIntBtn()
    {
        if (intLevel < 10 && soulCount >= soulPerLevel[intLevel])
        {
            soulCount -= soulPerLevel[intLevel];
            intLevel++;
            status.IncreaseInt();
            OnSoulChanged(soulCount.ToString()); // UI ������Ʈ �̺�Ʈ �߻�
        }
    }
}
