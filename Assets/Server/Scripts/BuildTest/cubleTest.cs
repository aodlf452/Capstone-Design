using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubleTest : MonoBehaviour
{
    public float moveSpeed = 5f; // �̵� �ӵ�

    void Update()
    {
        // ����� ���� �Է��� �޾� �̵� ���͸� ����մϴ�.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;

        // �̵� ���͸� ���� ��ġ�� ���մϴ�.
        transform.Translate(movement);
    }
}