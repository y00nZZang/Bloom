using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Environment Setting 0")]
    public GameObject centerTerrainSet0;
    public GameObject surroundingTerrainSet0;
    public float spotLightIntensity0;
    public float directionalLightIntensity0;
    public float exposure0;

    [Header("Environment Setting 1")]
    public GameObject centerTerrainSet1;
    public GameObject surroundingTerrainSet1;
    public float spotLightIntensity1;
    public float directionalLightIntensity1;
    public float exposure1;
    public GameObject sproutLandingPrefab;
    public float sproutSpawnTime = 5.0f;
    public Transform sproutSpawnPoint;
    public float transitionTiming1;

    [Header("Environment Setting 2")]
    public GameObject centerTerrainSet2;
    public GameObject surroundingTerrainSet2;
    public float spotLightIntensity2;
    public float directionalLightIntensity2;
    public float exposure2;
    public GameObject transitionEffectPrefab2;

    [Header("Controllers")]
    public EnvironmentController environmentController;
    public WaterBallSpawner waterBallSpawner;
    public PortalSpawner portalSpawner;
    public EffectSpawner effectSpawner;

    [Header("others")]
    public ParticleSystem sunLightParticleSystem;
    public Light spotlight;
    public float spotLightMaxIntensity = 100.0f;
    
    private int waterBallPortalPassCount = 0;

    private void Start()
    {
        // 게임 시작 시 환경 설정
        environmentController.SetEnvironment(centerTerrainSet0, surroundingTerrainSet0, spotLightIntensity0, directionalLightIntensity0, exposure0);
        
        // 빛 파티클 시스템 재생
        sunLightParticleSystem.Play();

        StartCoroutine(IncreaseIntensityOverTime());

        // 빛 파티클이 끝난 후 그린 파티클 시작
        StartCoroutine(StartSproutLanding(sproutSpawnTime));
    }

    IEnumerator IncreaseIntensityOverTime()
    {
        float elapsedTime = 0;

        while (elapsedTime < sproutSpawnTime)
        {
            spotlight.intensity = Mathf.Lerp(0, spotLightMaxIntensity, elapsedTime / sproutSpawnTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        spotlight.intensity = spotLightMaxIntensity; // Ensure the intensity reaches the maximum
    }

    private IEnumerator StartSproutLanding(float delay)
    {
        yield return new WaitForSeconds(delay);
        // sproutSpawnTime 초 후 sproutLandingPrefab 생성
        Instantiate(sproutLandingPrefab, sproutSpawnPoint.position, Quaternion.identity);
        
        yield return new WaitForSeconds(transitionTiming1);
        // SproutLanding 스크립트가 종료된 후 sproutPrefab 생성
        environmentController.SetEnvironment(centerTerrainSet1, surroundingTerrainSet1, spotLightIntensity1, directionalLightIntensity1, exposure1);

        // Stage 1 시작
        StartCoroutine(StartStage1());
    }

    private IEnumerator StartStage1()
    {
        // WaterBallSpawner와 PortalSpawner 활성화
        waterBallSpawner.gameObject.SetActive(true);
        portalSpawner.gameObject.SetActive(true);
        
        portalSpawner.SpawnPortal();

        // WaterBall이 Portal을 3번 통과할 때까지 대기
        while (waterBallPortalPassCount < 3)
        {
            yield return null;
        }

        // 3번 통과 후 환경 변경
        environmentController.SetEnvironment(centerTerrainSet2, surroundingTerrainSet2, spotLightIntensity2, directionalLightIntensity2, exposure2);
    }

    public void OnWaterBallPassThroughPortal(Vector3 portalPosition)
    {
        waterBallPortalPassCount++;
        
        effectSpawner.SpawnEffect(portalPosition);

        portalSpawner.SpawnPortal();

    }
}
