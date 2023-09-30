using Unity.VisualScripting;
using UnityEngine;

    [CreateAssetMenu(menuName = "Weapon/Bow")]
public class Bow : Weapon {
    
    public GameObject arrow;

    [Header("Charging")]
    public bool canCharge = true;
    
    [Range(0, 3)]
    public float maxChargedPower;
    public float minimumFiringTreshold;
    public float minimumFiringPower = 0f;

    [System.Serializable]
    public struct ChargingSprites{
        [SerializeField] public Sprite[] chargingSprites1D;
    }

    public ChargingSprites[] chargingSprites = new ChargingSprites[4];
}
