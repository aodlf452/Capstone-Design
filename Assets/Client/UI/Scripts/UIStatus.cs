using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    //[SerializeField] private PlayerStatus status;
    [SerializeField] private GrowthSystem growthSystem;
    //private GrowthSystem growthSystem;
    [SerializeField] private GameObject statBar;
    [SerializeField] private Button hpEnhanceButton;
    [SerializeField] private Button atkEnhanceButton;
    [SerializeField] private Button defEnhanceButton;
    [SerializeField] private Button dexEnhanceButton;
    [SerializeField] private Button intEnhanceButton;
    private void Start()
    {
        //growthSystem = FindObjectOfType<GrowthSystem>();
        
        // ��ư�� ��� ������ ����
        hpEnhanceButton.onClick.RemoveAllListeners();
        atkEnhanceButton.onClick.RemoveAllListeners();
        defEnhanceButton.onClick.RemoveAllListeners();
        dexEnhanceButton.onClick.RemoveAllListeners();
        intEnhanceButton.onClick.RemoveAllListeners();

        // �ν��Ͻ� ID ���
        Debug.Log("UIStatus���� ������ growthSystem ID: " + growthSystem.GetInstanceID());

        //if (growthSystem!=null)
        //{
        //    if (growthSystem.GetDicCnt()>0)
        //    {
        //        Debug.Log("����"+growthSystem.GetDicCnt());
        //    }
        //}

        // ������ ���
        hpEnhanceButton.onClick.AddListener(growthSystem.OnPressEnhanceHealthBtn);
        atkEnhanceButton.onClick.AddListener(growthSystem.OnPressEnhanceAttackBtn);
        defEnhanceButton.onClick.AddListener(growthSystem.OnPressEnhanceDefenseBtn);
        dexEnhanceButton.onClick.AddListener(growthSystem.OnPressEnhanceDexBtn);
        intEnhanceButton.onClick.AddListener(growthSystem.OnPressEnhanceIntBtn);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("ȣ���");
            statBar.SetActive(!statBar.activeSelf);
        }
    }
    
}
