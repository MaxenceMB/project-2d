using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int health = 100;
    public static PlayerHealth instance;

    private void Awake() {
        if(instance != null) {
            Debug.LogWarning("Il y'a plus d'une instance de PlayerManager");
            return;
        }

        instance = this;
    }

    public void TakeDamage(int damage) {
        this.health -= damage;
    }
}
