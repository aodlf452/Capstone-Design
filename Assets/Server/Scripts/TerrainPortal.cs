using UnityEngine;
using Photon.Pun; 

public class TerrainPortal : MonoBehaviour
{
    public Transform player;
    public Transform receiver;

    private bool playerIsOverlapping = false;

    void Start() 
    {
        FindLocalPlayer();
    }

    void FindLocalPlayer()
    {
        GameObject localPlayerObj = null;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (obj.GetComponent<PhotonView>().IsMine)
            {
                localPlayerObj = obj;
                break;
            }
        }
        if (localPlayerObj != null)
        {
            player = localPlayerObj.transform;
        }
        else
        {
            Debug.LogWarning("���� �÷��̾ ã�� �� �����ϴ�.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.gameObject.GetComponent<PhotonView>().IsMine)
        {
            playerIsOverlapping = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && other.gameObject.GetComponent<PhotonView>().IsMine)
        {
            playerIsOverlapping = false;
        }
    }

    void Update()
    {
        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            // �÷��̾ ��Ż�� ����ߴ��� Ȯ��
            if (dotProduct < 0f)
            {
                // �÷��̾ �ٸ� ��Ż�� ��ġ�� �̵�
                float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
                rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = receiver.position + positionOffset;

                playerIsOverlapping = false;
            }
        }
    }
}
