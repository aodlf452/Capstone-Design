using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SimpleLauncher : MonoBehaviourPunCallbacks
{

    public PhotonView playerPrefab;
    bool isConnecting;

    public Slider loadingProgressBar;
    public GameObject loadingUI;

    // Start is called before the first frame update
    void Start()
    {
        //Connect();
    }

    public void OnLoginButtonClicked()
    {
        loadingUI.SetActive(true);
        Connect();
    }
    public void Connect()
    {

        //SceneLoader.instance.LoadScene(1);
        isConnecting = true;
        PhotonNetwork.ConnectUsingSettings();

        // �ε� UI Ȱ��ȭ
        //loadingUI.SetActive(true);
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
        //PhotonNetwork.LoadLevel("MainScene");//�� �̸�
        StartCoroutine(LoadLevelWithProgress("MainScene"));
        Debug.Log("�� ��");
        // PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0, 1, 0), Quaternion.identity);

    }
    IEnumerator LoadLevelWithProgress(string sceneName)
    {
        // �ε� UI Ȱ��ȭ
        //loadingUI.SetActive(true);

        // Scene �񵿱� �ε� ����
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName); // SceneManager�� ���

        loadingProgressBar.value = 0;
        float targetProgress = 0;
        float lerpSpeed = 10f;
        // �ε��� �Ϸ�� ������ ���
        while (!asyncLoad.isDone)
        {
            // ���α׷��� �� ������Ʈ
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingProgressBar.value = Mathf.Lerp(loadingProgressBar.value, targetProgress, Time.deltaTime * lerpSpeed);

            yield return null;
        }

        // �ε� �Ϸ� �� �ε� UI ��Ȱ��ȭ
        loadingUI.SetActive(false);
    }

}
