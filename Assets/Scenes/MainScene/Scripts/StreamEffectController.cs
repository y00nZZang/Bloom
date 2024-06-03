using UnityEngine;
using System.Collections;

public class StreamEffectController : MonoBehaviour
{
    public float speed = 5.0f; // 마법 효과의 이동 속도
    public float noiseScale = 1.0f; // 노이즈 스케일 (이동 경로의 흔들림 정도)
    public float noiseFrequency = 1.0f; // 노이즈 주파수 (흔들림의 빈도)
    public Transform target; // 마법 효과가 날아갈 목표 위치
    public ParticleSystem cloud; // 초기 클라우드 효과
    public ParticleSystem cloudExpansion; // 목표에 도달 후 확장되는 클라우드 효과
    public ParticleSystem rainHeavy; // 목표에 도달 후 비 효과
    public float cloudExpansionDuration = 2.0f; // 클라우드 확장 효과의 지속 시간
    public float rainHeavyDuration = 3.0f; // 비 효과의 지속 시간
    private Vector3 noiseOffset; // 노이즈 오프셋
    private bool targetReached = false; // 목표 위치에 도달했는지 여부

    private void Start()
    {
        // 노이즈 오프셋 초기화
        noiseOffset = new Vector3(Random.value, Random.value, Random.value) * 10f;// 초기에는 cloud만 활성화
        cloud.Play();
        cloudExpansion.Stop();
        rainHeavy.Stop();
    }

    private void Update()
    {
        if (target != null && !targetReached)
        {
            // 목표 위치로의 방향 벡터 계산
            Vector3 direction = (target.position - transform.position).normalized;

            // 노이즈 계산
            float noiseX = Mathf.PerlinNoise(Time.time * noiseFrequency + noiseOffset.x, noiseOffset.y) * 2f - 1f;
            float noiseY = Mathf.PerlinNoise(noiseOffset.x, Time.time * noiseFrequency + noiseOffset.y) * 2f - 1f;
            float noiseZ = Mathf.PerlinNoise(noiseOffset.y, noiseOffset.z + Time.time * noiseFrequency) * 2f - 1f;
            Vector3 noise = new Vector3(noiseX, noiseY, noiseZ) * noiseScale;

            // 이동 경로에 노이즈 추가
            Vector3 moveDirection = direction + noise;
            moveDirection.Normalize();

            // 목표 위치로 이동
            transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDirection, speed * Time.deltaTime);

            // 목표 위치에 도달하면
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                targetReached = true;

                // cloud 비활성화
                cloud.Stop();

                transform.rotation = Quaternion.identity;
                
                // cloudExpansion 및 rainHeavy 활성화
                cloudExpansion.Play();
                rainHeavy.Play();

                // 효과 종료 후 오브젝트 제거
                StartCoroutine(PlayEffectsAndDestroy());
            }
        }
    }

    private IEnumerator PlayEffectsAndDestroy()
    {
        // cloudExpansion 효과 실행
        yield return new WaitForSeconds(cloudExpansionDuration);

        // rainHeavy 효과 실행
        yield return new WaitForSeconds(rainHeavyDuration);

        // 오브젝트 제거
        Destroy(gameObject);
    }
}
