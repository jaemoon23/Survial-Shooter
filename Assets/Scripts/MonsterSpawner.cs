using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Transform spawanPoints; // 스폰 장소
    public GameObject monsterPrefab; // 몬스터 프리팹

    public float spawnInterval = 2f; // 스폰 간격(초)
    private float spawnTimer = 0f; // 스폰 타이머

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
