using UnityEngine;

public class SpawnPortal : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnDelay = 5.0f;

    void Start()
    {
        Invoke("SpawnObject", spawnDelay);
    }

    // ������Ʈ�� �����ϴ� �޼����Դϴ�.
    void SpawnObject()
    {
        Instantiate(objectToSpawn, transform.position, Quaternion.identity);
    }
}