using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
//����� Ÿ�̸� RPC�� �̿� �ʰ� ���� ���� ����ȭ �� ��Ȯ�� �ð� ������ �ʿ��ϸ� ��� ���� �ʿ�.
public class DefenceTImer : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI timerUI;
    private int time;
    private PhotonView PV;

    void Start()
    {
     
    }

    public override void OnConnectedToMaster()
    {
        PV = GetComponent<PhotonView>();
        //Debug.Log("timertest");
        // MasterClient������ Ÿ�̸� ����
       if (PhotonNetwork.IsMasterClient)
       {
            time = 10;
            StartCoroutine("TimerCoroutine");
            Debug.Log("timertest");
        }
  
    }



    IEnumerator TimerCoroutine()
    {
        while (time > 0)
        {
        
            time -= 1;
            PV.RPC("ShowTimer", RpcTarget.All, time);
            yield return new WaitForSeconds(1);
        }
     
            Debug.Log("timer finish");
            yield break;

            //����� ����

        
    }

    //����ȭ �޾Ƽ� ǥ��
    [PunRPC]
    private void ShowTimer(int timerValue)
    {
        // ��� Ŭ���̾�Ʈ���� ȣ��Ǿ� Ÿ�̸Ӹ� ����ȭ
        Debug.Log("timertest12");
        timerUI.text = timerValue.ToString();
    }


    /*
  // Update is called once per frame
  void Update()
  {
      if (PhotonNetwork.IsMasterClient)
      {
          // Master Client�� ��� Ÿ�̸� ���� �����ϰ� ����ȭ
          if(time > 0) {
              time -= Time.deltaTime;
              playerPrefab.RPC("SyncTimer", RpcTarget.AllBuffered, time);
          }
          else
          {
              //Ÿ�̸� ������ �۵��� �ڵ� �����⿡ ���� 
          }

      }


  }
    
       void timer()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            timerUI.text = time.ToString("#.##");
        }
        else
        {
            timerUI.text = "finish";   
        }

    }
     */

}
