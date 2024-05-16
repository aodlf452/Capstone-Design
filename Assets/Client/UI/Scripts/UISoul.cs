using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISoul : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI soulText;
    void Start()
    {
        //soulText.text = "500";
        GrowthSystem.OnSoulChanged += UpdateUI; // �̺�Ʈ ����
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
