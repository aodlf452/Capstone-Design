using UnityEngine;
using Photon.Pun;

public class TerrainPortal : MonoBehaviour
{
    public Transform receiver;

    private bool playerIsOverlapping = false;
    private Transform overlappingPlayer = null; // ��ġ�� �÷��̾ ������ ����

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.gameObject.GetComponent<PhotonView>().IsMine)
        {
            playerIsOverlapping = true;
            overlappingPlayer = other.transform; // ��ġ�� �÷��̾��� Transform�� ����
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && other.gameObject.GetComponent<PhotonView>().IsMine)
        {
            playerIsOverlapping = false;
            overlappingPlayer = null; // ��ġ�� �÷��̾� ������ �ʱ�ȭ
        }
    }

    void Update()
    {
        if (playerIsOverlapping && overlappingPlayer != null)
        {
            Vector3 portalToPlayer = overlappingPlayer.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            // �÷��̾ ��Ż�� ����ߴ��� Ȯ��
            if (dotProduct < 0f)
            {
                // �÷��̾ �ٸ� ��Ż�� ��ġ�� �̵�
                float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
                rotationDiff += 180;
                overlappingPlayer.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                overlappingPlayer.position = receiver.position + positionOffset;

                playerIsOverlapping = false;
                overlappingPlayer = null; // �̵� �� ��ġ�� �÷��̾� ���� �ʱ�ȭ
            }
        }
    }
}
