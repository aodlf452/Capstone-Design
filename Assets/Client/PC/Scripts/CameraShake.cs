using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform mainCamera;
    public bool shakeRotate = false;

    public Vector3 originPos;
    public Quaternion originRot;

    private float defaultZoomInTime = 0.2f; // �� �ο� �ɸ��� �ð�
    private float defaultZoomOutTime = 0.2f; // �� �ƿ��� �ɸ��� �ð�
    private float defaultZoomWaitTime = 0.0f; // ���� �����ϴ� �ð�
    private float defaultZoomAmount = 30f; // �� ��/�ƿ��� ���� (FOV ���淮)

    bool isZoom;
    bool isShake;
    private int defence=0;
    public void SetPlayer(GameObject clone)
    {
        
        mainCamera = clone.GetComponent<Transform>();
        originPos = mainCamera.localPosition;
        originRot = mainCamera.localRotation;
    }
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = this.transform;
        originPos = mainCamera.localPosition;
        originRot = mainCamera.localRotation;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.mode == 1&& defence ==0)
        {
            Shaking(4f, 5f);
            defence = 1;
        }
    }
    public void Shaking(float shakeTime = 0.1f, float amount = 5.0f)
    {
        if (!isShake)
        {
            StartCoroutine(Shake(shakeTime,amount));
        }
    }

    public void Zoom(float zoomInTime = 0.2f, float zoomOutTime = 0.2f, float zoomWaitTime = 0.0f, float zoomAmount = 30.0f)
    {
        if (!isZoom)
        {
            StartCoroutine(ZoomCamera(zoomInTime, zoomOutTime, zoomWaitTime, zoomAmount));
        }
    }

    public IEnumerator Shake(float shakeTime, float amount) //�ǰ�, �浹 �� �پ��ϰ� ���
    {
        isShake = true;
        Vector3 originPoint = mainCamera.localPosition;
        float elapsedTime = 0.0f;

        while (elapsedTime < shakeTime)
        {
            Vector3 randomPoint = originPoint + Random.insideUnitSphere * amount;//��������
            mainCamera.localPosition = Vector3.Lerp(mainCamera.localPosition, randomPoint, Time.deltaTime * 1.0f);

            yield return null;

            elapsedTime += Time.deltaTime;
        }
        mainCamera.localPosition = originPoint;
        isShake = false;
    }


    //1. ���� �ɸ��� �ð�, 2. �� �ƿ� �ɸ��� �ð�, 3. ���� �����Ǵ� �ð�  4. ���̵Ǵ� ���� FOV�� ���淮
    public IEnumerator ZoomCamera(float zoomInTime, float zoomOutTime, float zoomWaitTime, float zoomAmount)    //ū ���� �� �Ͻ����� ���� ȿ��
    {
        isZoom = true;
        float originalFoV = mainCamera.GetComponent<Camera>().fieldOfView;
        float zoomedFoV = originalFoV - zoomAmount; // �� �� �� FOV ����
        float elapsedTime = 0.0f;

        // �� ��
        while (elapsedTime < zoomInTime)
        {
            mainCamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(originalFoV, zoomedFoV, elapsedTime / zoomInTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(zoomWaitTime);

        elapsedTime = 0.0f; // �ð� ����

        // �� �ƿ�
        while (elapsedTime < zoomOutTime)
        {
            mainCamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(zoomedFoV, originalFoV, elapsedTime / zoomOutTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isZoom = false;
    }

}
