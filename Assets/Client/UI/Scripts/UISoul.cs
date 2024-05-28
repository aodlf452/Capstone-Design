using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISoul : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI soulText;
    private void Awake()
    {
        GrowthSystem.OnSoulChanged += UpdateUI; // �̺�Ʈ ����
        
    }
    void Start()
    {
        //soulText.text = "500";
    }
    /// <summary>
    /// �ҿ��� �� Update
    /// </summary>
    /// <param name="quantity">ǥ���� �ҿ��� ��</param>
    public void UpdateUI(string quantity)
    {
        Debug.Log("UpdateUI called with quantity: " + quantity);
        soulText.text = quantity;
    }
    private void OnDestroy()
    {
        GrowthSystem.OnSoulChanged -= UpdateUI; // �̺�Ʈ ���� ����
    }
}
