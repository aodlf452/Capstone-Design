using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks
{

    public GameObject playerPrefab;
    public static bool portalOwner = false;
    private static GameManager instance = null;
    public TextMeshProUGUI gameOver;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    void Start()
    {
        gameOver.enabled = false;
        if (playerPrefab == null)
        {
            Debug.LogError("������ ����");
        }
        else
        {
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0, 1, 0), Quaternion.identity);

            // PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0, 1, 0), Quaternion.identity);
            Debug.Log("Ȯ��");
        }
       
    }
    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    public void GameFinish()
    {
        gameOver.enabled = true;
        if (portalOwner)
        {
            gameOver.text = "WIN";
        }
        else
        {
            gameOver.text = "LOSE";
        }

    }
    void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            return;
        }
        Debug.LogFormat("PhotonNetwork : Loading Level : ServerTestScene", PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("ServerTestScene");
    }
 


}

