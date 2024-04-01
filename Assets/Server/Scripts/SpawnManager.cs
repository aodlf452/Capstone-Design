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
    public GameObject portalSpawner;
    public GameObject portalSpawnPoint;
    //��Ż ���� ����Ʈ
    public GameObject monsterSpawnPoint;
    //���� ���� ����Ʈ
    private static SpawnManager instance = null;
    void Start()
    {
        PhotonNetwork.InstantiateRoomObject(timer.name, transform.position, transform.rotation, 0);
        PhotonNetwork.InstantiateRoomObject(portalSpawner.name, portalSpawnPoint.transform.position, transform.rotation, 0);
        //���� ����� ������Ʈ ��ġ���� �����ǰ� �����Ǿ����� ������ ������ ���� ���� �ִ� ����̳�
        //public GameObject spawnpoint ��ġ �������� �ٲٴ� �� ������...
        //instantiate�� instatiateRoomObject���� ���� : ���ڴ� �������� ������ �� �ı� ���ڴ� �������� 
        //�����͸� ������ �� ���� �̰� �̿��ؼ� Ÿ�̸� ����... �ϸ� ���� 
        if (instance == null)
        {
            instance = this;
     
        }
        else
        {
            
            Destroy(this.gameObject);
        }
  
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void portalSpawn()
    {
        PhotonNetwork.InstantiateRoomObject(portal.name, portalSpawnPoint.transform.position, transform.rotation, 0);
    }
    public static SpawnManager Instance
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



}
