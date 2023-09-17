using Unity.VisualScripting;
using UnityEngine;

    [CreateAssetMenu(menuName = "Items/Ranged weapon")]
public class RangedWeaponItem : WeaponItem {
    
    public GameObject arrow;

    [Header("Charging")]
    public bool canCharge = true;
    
    [Range(0, 3)]
    public float maxChargedPower;
    public float minimumFiringTreshold;
    public float minimumFiringPower = 0f;
    //public float rechargeTime;

    [System.Serializable]
    public struct ChargingSprites{
        [SerializeField] public Sprite[] chargingSprites1D;
    }

    public ChargingSprites[] chargingSprites = new ChargingSprites[4];
}
