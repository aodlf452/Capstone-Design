using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CollisionManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider coll)
    {
        Debug.Log("�浹");
        if (coll.gameObject.tag == "PortalSpawner")
        {
            Countdown.mode = 1;
            Destroy(coll.gameObject);
            GameManager.portalOwner = true;
            
            SpawnManager.Instance.portalSpawn();
            Debug.Log("ī��Ʈ �ٿ� ����");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
