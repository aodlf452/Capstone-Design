using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class previewTest : MonoBehaviour
{
    private Renderer previewRenderer;
    private Material originalMaterial;
    private Material collisionMaterial;


    // Start is called before the first frame update
    void Start()
    {
        previewRenderer = GetComponentInChildren<Renderer>();
        // �̸������� �⺻ ��Ƽ������ ����
        originalMaterial = previewRenderer.material;
      
      
    }

    private void Update()
    {
        // �ٴ��� ��ġ�� Ȯ���ϱ� ���� ����ĳ��Ʈ
      
        
    }
   

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("�浹");
        // �浹�� ������Ʈ�� ���� ��쿡�� ó��
        if (!collision.gameObject.CompareTag("floor"))
        {
            // previewPrefab�� ��Ƽ������ ������
            collisionMaterial = originalMaterial;

            // ���ο� ������ ����� ���� ����
            Color color = collisionMaterial.color;
            color.a = 0.5f;

            // ����� ������ ��Ƽ���� ����
            collisionMaterial.color = color;
            // ������ �����Ͽ� �浹 ���θ� ǥ��
            previewRenderer.material = collisionMaterial;
        }
    }

    // �浹�� ����� �� ȣ���
    void OnCollisionExit(Collision collision)
    {
        // ���� �������� �ǵ���
        previewRenderer.material = originalMaterial;
    }
}
