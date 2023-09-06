using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int PlayerHealth = 100;
    public float PlayerSpeed;

    public static PlayerManager instance;

    private void Awake() 
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y'a plus d'une instance de PlayerManager");
            return;
        }

        instance = this;
    }

    public void TakeDamage(int damage)
    {
        PlayerHealth -= damage;
    }


}
