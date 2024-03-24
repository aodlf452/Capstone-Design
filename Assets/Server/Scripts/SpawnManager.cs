using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject monster;
    public GameObject timer;
    public GameObject portal;
    public GameObject portalSpawnPoint;
    //��Ż ���� ����Ʈ
    public GameObject monsterSpawnPoint;
    //���� ���� ����Ʈ
    void Start()
    {
        PhotonNetwork.InstantiateRoomObject(monster.name, monsterSpawnPoint.transform.position, transform.rotation, 0);
        PhotonNetwork.InstantiateRoomObject(timer.name, transform.position, transform.rotation, 0);
        PhotonNetwork.InstantiateRoomObject(portal.name, portalSpawnPoint.transform.position, transform.rotation, 0);
        //���� ����� ������Ʈ ��ġ���� �����ǰ� �����Ǿ����� ������ ������ ���� ���� �ִ� ����̳�
        //public GameObject spawnpoint ��ġ �������� �ٲٴ� �� ������...
        //instantiate�� instatiateRoomObject���� ���� : ���ڴ� �������� ������ �� �ı� ���ڴ� �������� 
        //�����͸� ������ �� ���� �̰� �̿��ؼ� Ÿ�̸� ����... �ϸ� ���� 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
