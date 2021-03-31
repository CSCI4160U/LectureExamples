using UnityEngine;

public class CollectibleWeapon : Collectible {
    [SerializeField] private PlayerWeapon playerWeapon;
    [SerializeField] private int ammoGain = 14;
    [SerializeField] private bool includesWeapon = false;

    public override void Collect() {
        playerWeapon.rounds += ammoGain;
        if (includesWeapon) {
            playerWeapon.weaponExists = true;
        }
    }
}
