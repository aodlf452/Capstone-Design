using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerStatus : MonoBehaviour
{
    
    public BasicStats basicStats;
    public MoveStats moveStats;
    public CombatStats combatStats;

    // ������ �̺�Ʈ ����
    public delegate void StatChangedHandler(string numberText);
    public event StatChangedHandler OnMaxHPStatChanged;
    public event StatChangedHandler OnAtkStatChanged;
    public event StatChangedHandler OnDefStatChanged;
    public event StatChangedHandler OnDexStatChanged;
    public event StatChangedHandler OnIntStatChanged;

    public delegate void BarChangedIntHandler(int currentNumber, int maxNumber);
    public event BarChangedIntHandler OnHPBarChanged;

    public delegate void BarChangedFloatHandler(float currentNumber, float maxNumber);
    public event BarChangedFloatHandler OnStaminaBarChanged;

    //const float MaxHealth = 2000f;
    //const float MaxAttack = 500f;
    //const float MaxDefense = 300f;
    //const float MaxDex = 300f;
    //const float MaxInt = 300f;

    [System.Serializable]
    public class BasicStats
    {
        public int hp = 100;
        public int maxhp = 100;
        public int def = 10;
        public int atk = 30;
        public int dex = 30;
        public int intell = 30;
        public int luk;
    }

    [System.Serializable]
    public class MoveStats
    {
        public float stamina = 100f;
        public float maxStamina = 100f;
        public int speed = 10;
        //public float jumpPower = 7.0f;
    }

    [System.Serializable]
    public class CombatStats
    {
        public float constant_def = 100.0f;
        public float attack_rate=0.2f;
        public float critical_rate;
        public float critical_damage;
        public float cc_resistance;
    }



    Rigidbody rigid;
    Player player;

    private void Awake()
    {
        player = GetComponent<Player>();  
        rigid = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        // ������ �� ������ ������
        OnMaxHPStatChanged(basicStats.maxhp.ToString());
        OnDefStatChanged(basicStats.def.ToString());
        OnAtkStatChanged(basicStats.atk.ToString());
        OnDexStatChanged(basicStats.dex.ToString());
        OnIntStatChanged(basicStats.intell.ToString());

        OnHPBarChanged(basicStats.hp, basicStats.maxhp);
    }
    public void IncreaseMaxHealth()
    {
        basicStats.maxhp += 100;
        OnMaxHPStatChanged(basicStats.maxhp.ToString()); // UI ������Ʈ �̺�Ʈ �߻�
        OnHPBarChanged(basicStats.hp, basicStats.maxhp); // ü�¹� UI ������Ʈ �̺�Ʈ �߻�
    }
    public void IncreaseDefense()
    {
        basicStats.def += 10;
        OnDefStatChanged(basicStats.def.ToString()); // UI ������Ʈ �̺�Ʈ �߻�
    }
    public void IncreaseAttack() 
    {
        basicStats.atk += 30;
        OnAtkStatChanged(basicStats.atk.ToString()); // UI ������Ʈ �̺�Ʈ �߻�
    }
    public void IncreaseDex()
    {
        basicStats.dex += 30;
        OnDexStatChanged(basicStats.dex.ToString()); // UI ������Ʈ �̺�Ʈ �߻�
    }
    public void IncreaseInt()
    {
        basicStats.intell += 30;
        OnIntStatChanged(basicStats.intell.ToString()); // UI ������Ʈ �̺�Ʈ �߻�
    }
    public void DecreaseHP(int damage)
    {
        basicStats.hp -= damage;
        OnHPBarChanged(basicStats.hp, basicStats.maxhp); // ü�¹� UI ������Ʈ �̺�Ʈ �߻�
    }
    public void DecreaseStamina(float amount)
    {
        moveStats.stamina -= amount; // ���¹̳� ����
        OnStaminaBarChanged(moveStats.stamina, moveStats.maxStamina); // ���¹̳��� UI ������Ʈ �̺�Ʈ �߻�

    }
    public void IncreaseStamina(float amount) 
    {
        moveStats.stamina += amount; // ���¹̳� ȸ��
        OnStaminaBarChanged(moveStats.stamina, moveStats.maxStamina); // ���¹̳��� UI ������Ʈ �̺�Ʈ �߻�
    }

}