using UnityEngine;
using System.Collections;

public class PortalTrigger : MonoBehaviour
{
    public ParticleSystem portalParticleSystem;
    public ParticleSystem destroyParticleSystem;
    private bool isActivated = false;
    private void Start()
    {
        portalParticleSystem.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActivated && other.CompareTag("WaterBall")) // 공의 태그가 "WaterBall"인 경우
        {
            Debug.Log("Portal Triggered!");
            portalParticleSystem.Stop();
            
            // 공 제거
            Destroy(other.gameObject);
            // 포탈과 공의 소멸 이펙트 생성
            if (destroyParticleSystem != null)
            {
                destroyParticleSystem.Play();
            }
            StartCoroutine(DestroyPortalAfterEffect(destroyParticleSystem.main.duration));

            // GameManager에 WaterBall 통과 알림
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.OnWaterBallPassThroughPortal(transform.position);
            }
        }
    }

    private IEnumerator DestroyPortalAfterEffect(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
