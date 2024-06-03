using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    public GameObject portalPrefab;
    public Transform[] spawnPoints; // 포털이 생성될 고정 위치 배열
    public Transform heading; // 포털이 향할 목표 위치

    public void SpawnPortal()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned.");
            return;
        }

        // 무작위로 위치 선택
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // 포털 생성
        GameObject portal = Instantiate(portalPrefab, spawnPoint.position, Quaternion.identity);

        // 포털이 목표 위치를 향하도록 회전
        if (heading != null)
        {
            portal.transform.LookAt(heading);
        }
    }
}
