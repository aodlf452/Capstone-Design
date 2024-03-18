using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NicknameManager : MonoBehaviour
{
    public InputField nicknameInputField; // InputField ������Ʈ�� �Ҵ���� ����
    const string nickname ="name"; // ������� �г����� ������ ����
    public void SaveNickname(string value)
    {
        
        
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("�̸� ����");
            return;
        }

        PhotonNetwork.NickName = value;
        //nickname = value;
        PlayerPrefs.SetString(nickname, value);
        Debug.Log("�г����� ����Ǿ����ϴ�: " + PhotonNetwork.NickName); // �ֿܼ� ����� �г��� ���
    }
    void Start()
    {

        string defaultName = string.Empty;
        nicknameInputField = this.GetComponent<InputField>();
        if (nicknameInputField != null)
        {
            if (PlayerPrefs.HasKey(nickname))
            {
                defaultName = PlayerPrefs.GetString(nickname);
                nicknameInputField.text = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;
    }
}
