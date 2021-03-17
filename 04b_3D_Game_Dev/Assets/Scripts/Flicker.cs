using UnityEngine;

public class Flicker : MonoBehaviour {
    [SerializeField] private float minIntensity = 10.0f;
    [SerializeField] private float maxIntensity = 10.0f;
    [SerializeField] private float flickerSpeed = 2f;

    private Light light = null;

    void Awake() {
        light = GetComponent<Light>();
    }

    private void Start() {
        light.type = LightType.Point;
        light.shadows = LightShadows.Soft;
        light.range = 15f;
    }

    void Update() {
        float intensityRange = maxIntensity - minIntensity;
        float newIntensity = minIntensity + Mathf.PerlinNoise(Time.time * flickerSpeed, 0) * intensityRange;
        light.intensity = newIntensity;
    }
}
