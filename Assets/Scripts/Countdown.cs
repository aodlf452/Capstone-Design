using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Countdown : MonoBehaviour
{
    [SerializeField] float setTime = 100.0f;
    [SerializeField] Text countdownText;
    [SerializeField] GameObject Player;

    int playerCount = 0;

    void Start()
    {
        int initialMinutes = Mathf.FloorToInt(setTime / 60F); // ������ ���� ��
        int initialSeconds = Mathf.FloorToInt(setTime - initialMinutes * 60); // ������ ���� ��
        countdownText.text = string.Format("{0:00}:{1:00}", initialMinutes, initialSeconds); // ������ ���� �ð��� �ؽ�Ʈ�� ����
    }

    void Update()
    {
        playerCount = PhotonNetwork.PlayerList.Length;

        if (Player != null && playerCount == 2)
        {
            if (setTime > 0)
            {
                setTime -= Time.deltaTime;
            }
            else if (setTime <= 0)
            {
                Time.timeScale = 0.0f;
            }
            int minutes = Mathf.FloorToInt(setTime / 60F);
            int seconds = Mathf.FloorToInt(setTime - minutes * 60);
            countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}