using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Countdown : MonoBehaviour
{
    [SerializeField] int setTime = 100;
    [SerializeField] Text countdownText;
    //[SerializeField] GameObject Player;
    //[SerializeField] SpawnPortal spawnPortal;
    int playerCount = 0;
    int mode = 0;
    private int time;
    private PhotonView PV;

    private bool portalSpawned = false;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        int initialMinutes = Mathf.FloorToInt(setTime / 60); // ������ ���� ��
        int initialSeconds = Mathf.FloorToInt(setTime - initialMinutes * 60); // ������ ���� ��
        countdownText.text = string.Format("{0:00}:{1:00}", initialMinutes, initialSeconds); // ������ ���� �ð��� �ؽ�Ʈ�� ����
    }

    void Update()
    {
        playerCount = PhotonNetwork.PlayerList.Length;
        /*
        if (Player != null && playerCount == 1 && !portalSpawned)
        {
            StartCoroutine(DelayedSpawn(spawnPortal.spawnDelay)); 
            portalSpawned = true;
        }

        if (playerCount == 2)
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
        */
      

        if (mode == 0)
        {
            if (PhotonNetwork.IsMasterClient && !portalSpawned && playerCount == 1)
            {
                
                mode = 1;
                StartCoroutine("TimerCoroutine");
                Debug.Log("timertest");
                
            }
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log("�浹");
        if (coll.gameObject.tag == "Portal"&&mode==1)
            {
            setTime = 20;
            mode = 2;
            countdownText.color = Color.red;
            StartCoroutine("TimerCoroutine");
            Debug.Log("Open Portal");
           
        }
    }


    IEnumerator TimerCoroutine()
    {
        while (setTime > 0)
        {
            if (mode == 2)
            {
                mode = 3;
                yield break;
            }
            setTime -= 1;
            PV.RPC("ShowTimer", RpcTarget.All, setTime);
            yield return new WaitForSeconds(1);
        }
        portalSpawned = true;
        Debug.Log("timer finish");
        mode = 1;
        yield break;

        //����� ����


    }

    [PunRPC]
    private void ShowTimer(int setTime)
    {
        // ��� Ŭ���̾�Ʈ���� ȣ��Ǿ� Ÿ�̸Ӹ� ����ȭ
       // Debug.Log("timertest12");
        int minutes = Mathf.FloorToInt(setTime / 60);
        int seconds = Mathf.FloorToInt(setTime - minutes * 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //countdownText.text = timerValue.ToString();
    }


    IEnumerator DelayedSpawn(float delay)
    {
        yield return new WaitForSeconds(delay); // ���� �ð� ���
        //spawnPortal.SpawnObject(); 
    }

}