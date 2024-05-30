using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMovie : MonoBehaviour
{
    public GameObject buildingPrefab; // ������ ��� ������
    public float maxHeight = 1.5f; // �������� ��ġ�� �� �ִ� �ִ� ����
    public float raycastDistance = 1f; // ���̸� �� �Ÿ�
    public float distance = 1f; // ������ ��ġ ��ġ
    private bool isBuilding = false;
    public GameObject previewPrefab;
    public bool isPreviewCol = false;
    private GameObject preview;
    public GameObject[] buildArray = new GameObject[4];
    public GameObject[] previewArray = new GameObject[4];
    public int[] maxBuilds = new int[4];
    int nowBuild;

    public int[] buildNums = new int[4];

    public float rotationSpeed = 50f;
    private float height = 0;

    void Start()
    {
        //Instantiate(buildingPrefab, new Vector3(1, 1, 1), Quaternion.identity);
        buildingPrefab = buildArray[0];
        previewPrefab = previewArray[0];
    }

    void Update()
    {

       
       

        if (Input.GetKeyDown(KeyCode.R))
        {
            //r�� ������ ���� ���� ������ ��ǥ.


            if (!isBuilding)
            {
                PlacePreview();
            }
            else
            {

                DestroyPreview();
            }
            isBuilding = !isBuilding;
            Debug.Log("��� ��ȯ");
        }

        if (isBuilding)
        {

            if (Input.GetKeyDown(KeyCode.T))
            {
                if (maxBuilds[nowBuild] >= buildNums[nowBuild])
                    Build();
                else
                    Debug.Log("�����ʰ�");

                Debug.Log("��ġ");
            }
            if (Input.GetKey(KeyCode.Q))
            {
                RotateObject(-1); // �ð� �������� ȸ��
            }

            // "E" Ű�� ������ ������ �ڽ� ��ü�� �ݽð� �������� ���������� ȸ����Ŵ
            if (Input.GetKey(KeyCode.E))
            {
                RotateObject(1); // �ݽð� �������� ȸ��
            }
            switch (Input.inputString)
            {
                case "1":
                    ChangeBuild(0);
                    break;
                case "2":
                    ChangeBuild(1);
                    break;
                case "3":
                    ChangeBuild(2);
                    break;
                case "4":
                    ChangeBuild(3);
                    break;
            }
            MovePreview();
            //������ ���� ƨ��� ��� ����...
        }

    }

    void ChangeBuild(int x)
    {
        buildingPrefab = buildArray[x];
        previewPrefab = previewArray[x];
        nowBuild = x;
        DestroyPreview();
        PlacePreview();
    }


    void RotateObject(int direction)
    {

        float rotationAmount = rotationSpeed * direction * Time.deltaTime;


        preview.transform.Rotate(Vector3.up, rotationAmount);
    }
    void RotateReset()
    {

    }

    void Build()
    {
        buildNums[nowBuild]++;
        DestroyPreview();
        //PhotonNetwork.Instantiate(buildingPrefab.name, preview.transform.position, preview.transform.rotation);

        Instantiate(buildingPrefab, preview.transform.position, preview.transform.rotation);
        PlacePreview();

    }


    void DestroyPreview()
    {
        if (preview != null)
        {
            Destroy(preview);
        }
    }
    void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position + Vector3.forward * distance + Vector3.up * maxHeight, Vector3.down * raycastDistance, Color.red);
    }

    public void PlacePreview()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + transform.forward * distance + Vector3.up * maxHeight, Vector3.down, out hit, maxHeight + raycastDistance))
        {

            Vector3 buildingPosition = hit.point;
            BoxCollider boxCollider = buildingPrefab.GetComponent<BoxCollider>();


            if (boxCollider == null)
            {
                Debug.LogError("BoxCollider not found on specified GameObject.");
            }

            height = boxCollider.size.y;
            Vector3 offset = new Vector3(0f, height / 2, 0f);

            Debug.Log(height);
            Debug.Log(hit.point);
            preview = Instantiate(previewPrefab, buildingPosition, transform.rotation * Quaternion.Euler(new Vector3(0, 90, 0)));


        }
    }
    public void MovePreview()
    {


        RaycastHit hit;
        int layerMask = (-1) - (1 << LayerMask.NameToLayer("IgnoreRaycast"));

        if (Physics.Raycast(preview.transform.position + Vector3.up * 3f, Vector3.down, out hit, layerMask))
        {
            // �ٴڰ� �浹�� ���
            Vector3 floorPosition = hit.point;


            // �ڽ� ��ü�� ��ġ�� �ٴ��� ��ġ�� �̵���Ŵ

            //transform.position = transform.position+Vector3.up*floorPosition.y+offset;
            preview.transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z) + transform.forward * distance;
            preview.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal) * transform.rotation * Quaternion.Euler(new Vector3(0, 90, 0));

            Debug.Log(transform.rotation);
            Debug.Log(hit.normal);

        }


    }
}
