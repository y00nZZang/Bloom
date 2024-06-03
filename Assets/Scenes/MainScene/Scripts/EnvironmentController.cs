using UnityEngine;
using System.Collections;

public class EnvironmentController : MonoBehaviour
{
    public Material skyboxMaterial;
    public Light directionalLight;
    public Light spotLight;
    private GameObject currentCenterTerrain;
    private GameObject currentSurroundingTerrain;
    private float directionalLightIntensity = 0.1f;
    private float spotLightIntensity = 0.2f;
    private float exposure = 0.2f;

    public void SetEnvironment(GameObject centerTerrain = null, GameObject surroundingTerrain = null, float? newSpotLightIntensity = null, float? newDirectionalLightIntensity = null, float? newExposure = null)
    {
        // 현재 활성화된 TerrainSet 비활성화
        if (currentCenterTerrain != null) currentCenterTerrain.SetActive(false);
        if (currentSurroundingTerrain != null) currentSurroundingTerrain.SetActive(false);

        // 새로운 TerrainSet 활성화
        if (centerTerrain != null)
        {
            centerTerrain.SetActive(true);
            currentCenterTerrain = centerTerrain;
        }
        else if (currentCenterTerrain != null)
        {
            currentCenterTerrain.SetActive(true);
        }

        if (surroundingTerrain != null)
        {
            surroundingTerrain.SetActive(true);
            currentSurroundingTerrain = surroundingTerrain;
        }
        else if (currentSurroundingTerrain != null)
        {
            currentSurroundingTerrain.SetActive(true);
        }

        // 조명 설정
        if (newDirectionalLightIntensity.HasValue)
        {
            directionalLightIntensity = newDirectionalLightIntensity.Value;
            directionalLight.intensity = directionalLightIntensity;
        }

        if (newSpotLightIntensity.HasValue)
        {
            spotLightIntensity = newSpotLightIntensity.Value;
            spotLight.intensity = spotLightIntensity;
        }

        // 노출 설정
        if (newExposure.HasValue)
        {
            exposure = newExposure.Value;
            RenderSettings.skybox = skyboxMaterial;
            RenderSettings.skybox.SetFloat("_Exposure", exposure);
        }
    }
}
