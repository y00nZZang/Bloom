using UnityEngine;
using System.Collections;

public class WaterBallSpawner : MonoBehaviour
{
    public GameObject waterBallPrefab;
    public Transform player;
    public float spawnInterval = 2.0f;
    public int maxWaterBalls = 5;
    private int currentWaterBallCount = 0;
    private Coroutine spawnCoroutine;

    private void Start()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnWaterBalls());
        }
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnWaterBalls()
    {
        while (true)
        {
            if (currentWaterBallCount < maxWaterBalls)
            {
                Vector3 spawnPosition = player.position + player.forward * 5.0f + Random.insideUnitSphere * 2.0f;
                Instantiate(waterBallPrefab, spawnPosition, Quaternion.identity);
                currentWaterBallCount++;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void OnDestroy()
    {
        currentWaterBallCount--;
        Debug.Log("Water ball destroyed. Current count: " + currentWaterBallCount);
    }
}
