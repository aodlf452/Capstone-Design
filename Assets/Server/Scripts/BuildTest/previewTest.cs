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
        RaycastHit hit;
        if (Physics.Raycast(transform.position+Vector3.up*3f, Vector3.down, out hit))
        {
            // �ٴڰ� �浹�� ���
            Vector3 floorPosition = hit.point;
            Debug.Log(hit.point);
            // �ڽ� ��ü�� ��ġ�� �ٴ��� ��ġ�� �̵���Ŵ

            BoxCollider boxCollider = GetComponent<BoxCollider>();

            // �ڽ� �ݶ��̴��� �����ϴ��� Ȯ��
            if (boxCollider == null)
            {
                Debug.LogError("BoxCollider not found on specified GameObject.");
            }
            // �ǹ��� �ٴڿ� ��ġ�ϱ� ���� �ǹ��� ���� ������ ����
            float height = boxCollider.size.y;
            Vector3 offset = new Vector3(0f, height / 2, 0f);
            //transform.position = transform.position+Vector3.up*floorPosition.y+offset;
            Debug.Log(transform.position);
        }
        
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
