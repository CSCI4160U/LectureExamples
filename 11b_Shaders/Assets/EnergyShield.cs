using UnityEngine;
public class EnergyShield : MonoBehaviour {
    [SerializeField] private GameObject shield;
    [SerializeField] private int maxStrength = 50;
    [SerializeField] private float initialHitRadius = 0.2f;
    [SerializeField] private float hitRadiusDecay = 0.01f;
    private int strength;
    private float hitRadius = 0f;
    private Material material;
    private void Start() {
        strength = maxStrength;
        material = GetComponent<MeshRenderer>().material;
    }
    private void Update() {
        if (hitRadius > -0.5f) { 
            hitRadius -= hitRadiusDecay;
            material.SetFloat("HitRadius", hitRadius);
        }
    }
    public void RegisterHit(int damage, Vector3 hitPosition) {
        strength -= damage;
        if (strength <= 0) {
            Destroy(shield, 0.5f);
            Destroy(this.gameObject, 0.5f);
        }
        hitRadius = initialHitRadius;
        material.SetVector("HitPosition", hitPosition);
        material.SetFloat("HitRadius", hitRadius);
    }
}