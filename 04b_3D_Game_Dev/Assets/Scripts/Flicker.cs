using UnityEngine;

public class Flicker : MonoBehaviour {
    [SerializeField] private float minIntensity = 10.0f;
    [SerializeField] private float maxIntensity = 10.0f;
    [SerializeField] private float flickerSpeed = 2f;

    private Light lightSource = null;

    void Awake() {
        lightSource = GetComponent<Light>();
    }

    private void Start() {
        lightSource.type = LightType.Point;
        lightSource.shadows = LightShadows.Soft;
        lightSource.range = 15f;
    }

    void Update() {
        float intensityRange = maxIntensity - minIntensity;
        float newIntensity = minIntensity + Mathf.PerlinNoise(Time.time * flickerSpeed, 0) * intensityRange;
        lightSource.intensity = newIntensity;
    }
}
