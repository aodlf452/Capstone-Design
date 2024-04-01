using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using Photon.Realtime;

public class SimpleLauncher : MonoBehaviourPunCallbacks
{

    public PhotonView playerPrefab;
    bool isConnecting;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Connect()
    {

        //SceneLoader.instance.LoadScene(1);
        PhotonNetwork.ConnectUsingSettings();
        isConnecting = true;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("�����");

        //�� ����
        PhotonNetwork.CreateRoom(null, new RoomOptions());
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room.");
        PhotonNetwork.LoadLevel("ServerTestScene");//�� �̸�
        Debug.Log("�� ��");
        // PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0, 1, 0), Quaternion.identity);

    }

}
