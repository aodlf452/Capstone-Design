using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PortalSpawnerManager : MonoBehaviourPun
{
    // Start is called before the first frame update
    void OnEnable()
    {
        
    }
    void OnTriggerEnter(Collider coll)
    {
        Debug.Log("�浹");
        if (coll.tag == "Melee")
        {
            Countdown.mode = 1;
            PhotonView collPhotonView = coll.GetComponentInParent<PhotonView>();
            
            string playerID = collPhotonView.Owner.NickName;
            GameManager.Instance.GetPortal(playerID);
            
            Debug.Log("ī��Ʈ �ٿ� ����");
            Destroy(gameObject);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
