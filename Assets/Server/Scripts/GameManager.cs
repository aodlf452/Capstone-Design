using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Header("�÷��̾� ����")]
    //[SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject playerSpawnPoint;

    public bool isGameover { get; private set; }

    public bool portalOwner = false;
    private static GameManager instance = null;

    public TextMeshProUGUI gameOver;
    public GameObject overPanel;
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
        // gameOver.enabled = false;
        overPanel.SetActive(false);
        playerPrefab = CharacterSelect.character;
        if (playerPrefab == null)
        {
            Debug.LogError("������ ����");
        }
        else
        {
<<<<<<< Updated upstream
            GameObject playerObj = PhotonNetwork.Instantiate(this.playerPrefab.name, playerSpawnPoint.transform.position, playerSpawnPoint.transform.rotation);
=======
 
>>>>>>> Stashed changes
            // PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0, 1, 0), Quaternion.identity);
            GameObject cameraObj = GameObject.Find("TPS Camera"); 
            if (cameraObj != null)
            {
                CameraFollow camaraFollow = cameraObj.GetComponent<CameraFollow>();
                if (camaraFollow != null)
                {
                    camaraFollow.SetPlayer(playerObj);
                }
            }
            Debug.Log("Ȯ��");
            isGameover = false;
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
    public void GetPortal()
    {
        portalOwner = true;
    }
    public void PlayerDead()
    {
        overPanel.SetActive(true);
        gameOver.enabled = true;
        gameOver.text = "YOU DIED";
    }
    public void GameFinish()
    {
        isGameover = true;
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
        PhotonNetwork.LoadLevel("MainScene");
    }
 


}

