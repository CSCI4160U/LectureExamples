using UnityEngine;

public class CollectibleHealth : Collectible {
    [SerializeField] private Health playerHealth;
    [SerializeField] private int healthGain = 20;

    public override void Collect() {
        // heal the player
        playerHealth.hp += healthGain;
        if (playerHealth.hp > playerHealth.maxHP) {
            playerHealth.hp = playerHealth.maxHP;
        }
    }
}
