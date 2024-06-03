using UnityEngine;
using System.Collections;

public class SproutLanding : MonoBehaviour
{
    public ParticleSystem landingParticleSystem;
    public ParticleSystem popParticleSystem;
    public float landingDuration = 5.0f;
    [HideInInspector]

    public float popDuration = 2.0f;
    [HideInInspector]
    public float popChargeDuration = 1.0f;
    public float startY = 30.0f;
    public AnimationCurve fallCurve;
    public AnimationCurve fadeCurve;

    private ParticleSystem.MainModule mainModule;
    private Color initialColor = new Color(0, 0, 0, 0);
    private Color targetColor = new Color(0, 0, 0, 1);
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        mainModule = landingParticleSystem.main;
        initialPosition = new Vector3(transform.position.x, startY, transform.position.z);
        targetPosition = transform.position;
        transform.position = initialPosition;
        Debug.Log(initialPosition);
        StartCoroutine(LandingEffect());
        StartCoroutine(PlayPopParticleSystemAfterDelay());
    }

    private IEnumerator LandingEffect()
    {
        landingParticleSystem.Play();
        float elapsedTime = 0f;

        while (elapsedTime < landingDuration)
        {
            elapsedTime += Time.deltaTime;

            // 하강 위치 계산
            float t = elapsedTime / landingDuration;
            float currentY = Mathf.Lerp(initialPosition.y, targetPosition.y, fallCurve.Evaluate(t));
            transform.position = new Vector3(initialPosition.x, currentY, initialPosition.z);

            // 페이드 인 효과 계산
            Color currentColor = Color.Lerp(initialColor, targetColor, fadeCurve.Evaluate(t));
            mainModule.startColor = currentColor;

            yield return null;
        }

        // 최종 위치 및 색상 설정
        transform.position = targetPosition;
        mainModule.startColor = targetColor;

        landingParticleSystem.Stop();
    }

    IEnumerator PlayPopParticleSystemAfterDelay()
    {
        yield return new WaitForSeconds(landingDuration);
        popParticleSystem.Play();
        yield return new WaitForSeconds(popDuration);
        Destroy(gameObject);
    }
}
