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
        Debug.Log("Collision detected with " + coll.name);
        if (coll.tag == "Player")
        {
            //�ӽ÷� �÷��̾ ����������
            GameManager.Instance.DefenceStart();
            PhotonView collPhotonView = coll.GetComponent<PhotonView>();
            //PhotonView collPhotonView = coll.GetComponentInParent<PhotonView>();

            string playerID = collPhotonView.Owner.NickName;
            GameManager.Instance.GetPortal(playerID);
            
            Debug.Log("ī��Ʈ �ٿ� ����");
            PhotonView PV = this.GetComponent<PhotonView>();
            if (PV.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [PunRPC]
    private void Break()
    {
      
        PhotonNetwork.Destroy(gameObject);
    }
}
