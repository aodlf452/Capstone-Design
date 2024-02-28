using UnityEngine;

public class SpawnPortal : MonoBehaviour
{
    public GameObject objectToSpawn; // ������ ������Ʈ�� �����ϴ� ���� ����
    public float spawnDelay = 5.0f; // ���������� ���� �ð��� �����ϴ� ���� ����

    // Start �޼���� ���� ���۵� �� ȣ��Ǵ� �޼����Դϴ�.
    void Start()
    {
        // Invoke �޼���� ������ �޼��带 ���� �ð� �Ŀ� ȣ���ϴ� �Լ��Դϴ�.
        // ���⼭�� "SpawnObject" �޼��带 spawnDelay �ð� �Ŀ� ȣ���ϵ��� �����߽��ϴ�.
        Invoke("SpawnObject", spawnDelay);
    }

    // ������Ʈ�� �����ϴ� �޼����Դϴ�.
    void SpawnObject()
    {
        // Instantiate �޼���� ������ ������Ʈ�� �������� �����ϴ� �Լ��Դϴ�.
        // ���⼭�� objectToSpawn ������Ʈ�� �����Ͽ� �� ������Ʈ�� �����մϴ�.
        Instantiate(objectToSpawn, transform.position, Quaternion.identity);
    }
}
