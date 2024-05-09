using UnityEngine;

public class TerrainPortal : MonoBehaviour
{
    public Transform player;
    public Transform receiver; // �ٸ� ��Ż�� ��ġ

    private bool playerIsOverlapping = false;

    // Ʈ���� �ݶ��̴��� ���� �����ϸ� ȣ��˴ϴ�.
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = true;
        }
    }

    // Ʈ���� �ݶ��̴����� ���� ������ ȣ��˴ϴ�.
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = false;
        }
    }

    // �� �����Ӹ��� ȣ��˴ϴ�.
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