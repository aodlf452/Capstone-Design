using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class TPSCamera : MonoBehaviourPun
{
    //[SerializeField] private Transform cameraArm;
    [SerializeField] private Player target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float sensivity = 1.0f;
    [SerializeField] private float speed = 3.0f;
    float currentY = 0f;


    // �÷��̾��� �������� ������ ī�޶� �̵�

    private void Start()
    {
        PhotonView photonView = GetComponentInParent<PhotonView>();
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            this.enabled = false;
        }
        target = GetComponentInParent<Player>();
    }
    private void Update()
    {
        PhotonView photonView = GetComponentInParent<PhotonView>();
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            this.enabled = false;
        }
    }
    private void LateUpdate()
    {
      
        // -z�� �������� offset ũ�⸸ŭ ������ ���� 
        Vector3 direction = new Vector3(0, 0, -offset.magnitude);

        // ī�޶��� ���� ȸ�� �� = Ÿ�ٿ��� ����ؿ�
        // ī�޶��� ���� ȸ�� �� = Ÿ���� ���� y�� ȸ�� �� 
        Quaternion rotation = Quaternion.Euler(target.VRotation, target.transform.eulerAngles.y, 0);

        // ī�޶��� position = Ÿ���� ��ġ + ������ �Ÿ����Ϳ� ī�޶� ȸ�� ����
        transform.position = target.transform.position + rotation * direction;

        // ī�޶� �׻� Ÿ���� �ٶ󺸵��� ����
        transform.LookAt(target.transform.position + Vector3.up * offset.y);

    }

}
