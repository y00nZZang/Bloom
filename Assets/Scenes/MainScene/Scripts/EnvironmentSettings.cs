using UnityEngine;

[CreateAssetMenu(fileName = "EnvironmentSettings", menuName = "ScriptableObjects/EnvironmentSettings", order = 1)]
public class EnvironmentSettings : ScriptableObject
{
    public string centerTerrainName;
    public string surroundingTerrainName;
    public float spotLightIntensity;
    public float directionalLightIntensity;
    public float skyboxExposure;
}
