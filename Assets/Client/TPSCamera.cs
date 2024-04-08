using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraArm;
    [SerializeField] private Transform player;
    [SerializeField] private float sensivity = 1.0f;
    [SerializeField] private float speed = 3.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseRotate();
        Move();
    }

    private void Move()
    {
        float inputX = Input.GetAxis("Horizontal"); // x�� �̵�(-1/1)
        float inputZ = Input.GetAxis("Vertical"); // z�� �̵�(-1/1)

        Vector3 moveVec = new Vector3(inputX, 0, inputZ);
        bool isMove = moveVec.magnitude != 0;

        if (isMove)
        {
            // y�� ���� ���ŵ� ī�޶��� z��, x�� ���⺤��
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            
            Vector3 moveDir = lookForward * moveVec.z + lookRight * moveVec.x;

            player.forward = lookForward;
            transform.position += moveDir * speed * Time.deltaTime;
        }

    }

    private void MouseRotate()
    {
        float mouseValueX = Input.GetAxis("Mouse X");
        float mouseValueY = Input.GetAxis("Mouse Y");
        
        Debug.Log($"mouseX: {mouseValueX}, mouseY: {mouseValueY}");

        Vector3 cameraAngle = cameraArm.rotation.eulerAngles; // ���Ϸ���

        float cameraX = cameraAngle.x - (mouseValueY * sensivity); // x�� ȸ����(����)
        float cameraY = cameraAngle.y + (mouseValueX * sensivity); // y�� ȸ����(����)
        float cameraZ = cameraAngle.z;
        // 90~0(�Ʒ�)~360~270(��)
        if(cameraX > 180f) 
        {
            // ���� ���� ����(��)
            cameraX = Mathf.Clamp(cameraX, 300f, 360f);
        }
        else
        {
            // ���� ���� ����(�Ʒ�)
            cameraX = Mathf.Clamp(cameraX, -1f, 90f);
        }
        Debug.Log($"cameraX: {cameraX}");

        // ����)���� ȸ���� rotation���� Quaternion ���� �̿��Ѵ�
        // Quaternion.Euler : ���Ϸ� -> ���ʹϾ�
        cameraArm.rotation = Quaternion.Euler(cameraX, cameraY, cameraZ);

        player.rotation = Quaternion.Euler(0, cameraArm.rotation.eulerAngles.y, 0); // ī�޶� �������� ĳ���� ȸ��

    }
}
