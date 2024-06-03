using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    public GameObject effectPrefab; // 효과 프리팹
    public Transform target; // 목표 위치

    public void SpawnEffect(Vector3 spawnPosition)
    {
        if (effectPrefab == null || target == null)
        {
            Debug.LogWarning("Effect prefab or target is not assigned.");
            return;
        }

        // 효과 생성
        GameObject effectInstance = Instantiate(effectPrefab, spawnPosition, Quaternion.identity);

        // EffectController의 target 설정
        StreamEffectController effectController = effectInstance.GetComponent<StreamEffectController>();
        if (effectController != null)
        {
            effectController.target = target;
        }

        // 효과 인스턴스의 위치를 목표 위치로 향하도록 설정
        effectInstance.transform.LookAt(target);
    }
}
