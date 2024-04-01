using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Countdown : MonoBehaviour
{
    [SerializeField] int setTime = 100;
    [SerializeField] int setPortalTime = 40;
    [SerializeField] Text countdownText;
    //[SerializeField] GameObject Player;
    //[SerializeField] SpawnPortal spawnPortal;
    int playerCount = 0;
    public static int mode = 0;
    //��� 0 : �ʱ� ����
    //��� 1 : ��Ż ���� ���� 
    private int time;
    private int PortalMode = 0;
    private PhotonView PV;
    private int timerStop = 0;
    private bool portalSpawned = false;
    private bool gameStarted = false;

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
  
        if (PhotonNetwork.IsMasterClient)
        {
           if(!gameStarted)
            {
                StartTimer(setTime);
                gameStarted = true;
                Debug.Log("timertest");
            }
            if (mode ==1) {

                ResetTimer(setPortalTime);
                portalSpawned = true;
                mode = 2;
                Debug.Log("��Ż ����");
            } 

        }


   
    }

    public void StartTimer(int time)
    {
        setTime = time;
        StartCoroutine("TimerCoroutine");
        Debug.Log("timertest ����");
    }
    public void StopTimer()
    {

    }
    public void ResetTimer(int time)
    {
        timerStop = 1;
        setTime = time;
        StartCoroutine("TimerCoroutine");
        Debug.Log("Ÿ�̸� ����");
    }


    IEnumerator TimerCoroutine()
    {

        while(setTime > 0)
        {
            if (timerStop == 1)
            {
                Debug.Log("stop ����");
                timerStop = 0;
                yield break;
            }
    
            setTime -= 1;
            PV.RPC("ShowTimer", RpcTarget.All, setTime);
            yield return new WaitForSeconds(1);
        }
            Debug.Log("Ÿ�̸� ����");
        GameManager.Instance.GameFinish();

        yield break;

    }

    [PunRPC]
    private void ModeChange(int modenum)
    {
        mode = modenum;
        Debug.Log("��� ���� Ȯ��"+mode);
        if (mode == 2)
        {
            Debug.Log("��� 2 ���� Ȯ��");
            countdownText.color = Color.red;
            PortalMode = 1;
        }
        


    }
   


    [PunRPC]
    private void ShowTimer(int setTime)
    {
        // ��� Ŭ���̾�Ʈ���� ȣ��Ǿ� Ÿ�̸Ӹ� ����ȭ
       Debug.Log("timertest RPC");
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