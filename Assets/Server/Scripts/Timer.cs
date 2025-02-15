using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Timer : MonoBehaviourPun
{

    Text countdownText;
    int setTime;
    int Time;
    int playerCount = 0;
    private int time;
    private PhotonView PV;
    private int timerStop = 0;
    void OnEnable()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        setTimer();
        setTime = GameManager.Instance.setTime;
        StartTimer(setTime);
        Debug.Log("타이머 생성"+ PhotonNetwork.IsMasterClient);
    }

    // Start is called before the first frame update
    void Start()
    {

        
    }
    void setTimer()
    {
        PV = GetComponent<PhotonView>();
        countdownText = GameObject.Find("Countdown").GetComponent<Text>();
        
        int initialMinutes = Mathf.FloorToInt(setTime / 60); // 시작할 때의 분
        int initialSeconds = Mathf.FloorToInt(setTime - initialMinutes * 60); // 시작할 때의 초
        countdownText.text = string.Format("{0:00}:{1:00}", initialMinutes, initialSeconds); // 시작할 때의 시간을 텍스트로 설정
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartTimer(int time)
    {
        setTime = time;
        StartCoroutine("TimerCoroutine");
    }
    public void StopTimer()
    {
     
            PhotonNetwork.Destroy(gameObject);
        
    }
    IEnumerator TimerCoroutine()
    {
       
        while (setTime > 0)
        {
            setTime -= 1;
            PV.RPC("ShowTimer", RpcTarget.All, setTime);
            yield return new WaitForSeconds(1);
        }
        if (GameManager.Instance.mode == 0)
        {
            GameManager.Instance.GameFinish();
            StopTimer();
        }
        else {
            //GameManager.Instance.GameFinish();
            StopTimer();
                }
        yield break;

    }
    [PunRPC]
    private void ShowTimer(int setTime)
    {

        if (PV == null || countdownText == null)
            setTimer();
        int minutes = Mathf.FloorToInt(setTime / 60);
        int seconds = Mathf.FloorToInt(setTime - minutes * 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    
    }
}
