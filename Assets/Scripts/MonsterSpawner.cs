using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Transform spawanPoints; // ���� ���
    public GameObject monsterPrefab; // ���� ������

    public float spawnInterval = 2f; // ���� ����(��)
    private float spawnTimer = 0f; // ���� Ÿ�̸�

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            var monster = Instantiate(monsterPrefab, spawanPoints.position, Quaternion.identity);
            spawnTimer = 0f;

        }
    }
}
