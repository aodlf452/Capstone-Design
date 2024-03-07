using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NicknameManager : MonoBehaviour
{
    public InputField nicknameInputField; // InputField ������Ʈ�� �Ҵ���� ����
    private string nickname; // ������� �г����� ������ ����

    public void SaveNickname()
    {
        nickname = nicknameInputField.text; // InputField�� ���� nickname ������ ����
        Debug.Log("�г����� ����Ǿ����ϴ�: " + nickname); // �ֿܼ� ����� �г��� ���
    }
}
