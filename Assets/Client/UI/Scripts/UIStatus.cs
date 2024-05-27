using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStatus : MonoBehaviour
{
    
    [SerializeField] private GrowthSystem growthSystem; 
    [SerializeField] private PlayerStatus playerStatus; // �̺�Ʈ ������

    [SerializeField] private GameObject statBar;
    [SerializeField] private Button hpEnhanceButton;
    [SerializeField] private Button atkEnhanceButton;
    [SerializeField] private Button defEnhanceButton;
    [SerializeField] private Button dexEnhanceButton;
    [SerializeField] private Button intEnhanceButton;

    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private TextMeshProUGUI defText;
    [SerializeField] private TextMeshProUGUI dexText;
    [SerializeField] private TextMeshProUGUI intText;
    [SerializeField] private TextMeshProUGUI soulText;
    private void Awake()
    {
        // ��ư�� ��� ������ ����
        hpEnhanceButton.onClick.RemoveAllListeners();
        atkEnhanceButton.onClick.RemoveAllListeners();
        defEnhanceButton.onClick.RemoveAllListeners();
        dexEnhanceButton.onClick.RemoveAllListeners();
        intEnhanceButton.onClick.RemoveAllListeners();

        // �ν��Ͻ� ID ���
        Debug.Log("UIStatus���� ������ growthSystem ID: " + growthSystem.GetInstanceID());

        // ��ư�� ������ ���
        hpEnhanceButton.onClick.AddListener(growthSystem.OnPressEnhanceHealthBtn);
        atkEnhanceButton.onClick.AddListener(growthSystem.OnPressEnhanceAttackBtn);
        defEnhanceButton.onClick.AddListener(growthSystem.OnPressEnhanceDefenseBtn);
        dexEnhanceButton.onClick.AddListener(growthSystem.OnPressEnhanceDexBtn);
        intEnhanceButton.onClick.AddListener(growthSystem.OnPressEnhanceIntBtn);

        // �̺�Ʈ ���(����)
        playerStatus.OnMaxHPStatChanged += UpdateMaxHP;
        playerStatus.OnAtkStatChanged += UpdateAttack;
        playerStatus.OnDefStatChanged += UpdateDefense;
        playerStatus.OnDexStatChanged += UpdateDex;
        playerStatus.OnIntStatChanged += UpdateInt;

        // �̺�Ʈ ���(�ҿ� ����)
        GrowthSystem.OnSoulChanged += UpdateSoul;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("ȣ���");
            statBar.SetActive(!statBar.activeSelf);
        }
    }
    private void OnDestroy()
    {
        // ��ư ������ ����
        hpEnhanceButton.onClick.RemoveAllListeners();
        atkEnhanceButton.onClick.RemoveAllListeners();
        defEnhanceButton.onClick.RemoveAllListeners();
        dexEnhanceButton.onClick.RemoveAllListeners();
        intEnhanceButton.onClick.RemoveAllListeners();

        // �̺�Ʈ ����
        playerStatus.OnMaxHPStatChanged -= UpdateMaxHP;
        playerStatus.OnAtkStatChanged -= UpdateAttack;
        playerStatus.OnDefStatChanged -= UpdateDefense;
        playerStatus.OnDexStatChanged -= UpdateDex;
        playerStatus.OnIntStatChanged -= UpdateInt;

        GrowthSystem.OnSoulChanged -= UpdateSoul;
    }
    public void UpdateMaxHP(string numberText)
    {
        hpText.text = numberText;
    }
    public void UpdateAttack(string numberText)
    {
        atkText.text = numberText;
    }
    public void UpdateDefense(string numberText)
    {
        defText.text = numberText;
    }
    public void UpdateDex(string numberText)
    {
        dexText.text = numberText;
    }
    public void UpdateInt(string numberText)
    {
        intText.text = numberText;
    }
    public void UpdateSoul(string count)
    {
        soulText.text = count;
    }
    
}
