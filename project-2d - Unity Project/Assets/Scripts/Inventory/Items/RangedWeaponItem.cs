using Unity.VisualScripting;
using UnityEngine;

    [CreateAssetMenu(menuName = "Items/Ranged weapon")]
public class RangedWeaponItem : WeaponItem {
    
    public GameObject projectile;

    [Header("Charging")]
    public bool canCharge = true;
    
    [Range(0, 3)]
    public float aimChargeDuration;

    [System.Serializable]
    public struct ChargingSprites{
        [SerializeField] public Sprite[] chargingSprites1D;
    }

    public ChargingSprites[] chargingSprites = new ChargingSprites[4];
}
