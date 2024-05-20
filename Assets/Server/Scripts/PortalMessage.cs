using UnityEngine;
using UnityEngine.UI;

public class PortalMessage : MonoBehaviour
{
    public Text messageText;  // UI Text ��Ҹ� �����մϴ�.

    void Start()
    {
        messageText.enabled = false;  // ������ �� �ؽ�Ʈ�� ��Ȱ��ȭ�մϴ�.
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // �÷��̾ ��Ż�� �����ϸ�
        {
            messageText.text = "���� ������ �̵��Ͻðڽ��ϱ�?";
            messageText.enabled = true;  // �ؽ�Ʈ�� Ȱ��ȭ�մϴ�.
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  // �÷��̾ ��Ż���� �־�����
        {
            messageText.enabled = false;  // �ؽ�Ʈ�� ��Ȱ��ȭ�մϴ�.
        }
    }
}
