using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour {
    
    // GameObjects
    public Rigidbody2D rb;
    public InventoryManager inventoryManager;

    // Directions
    public Camera mainCamera;
    public Vector2 mousePosition;
    public int facingDirection;

    // Weapon sprite
    public int layerOrder;
    public Vector2 weaponPosition;
    public Vector2 projectilePosition;

    // Aiming down sight
    public float chargedAim = 0f;

    void Update(){
        Item selectedItem = inventoryManager.GetSelectedItem();
        if (selectedItem is RangedWeaponItem){
            RangedWeaponItem rangedWeapon = (RangedWeaponItem) selectedItem;
            SetFacingDirection();
            if (rangedWeapon.canCharge){
                rangedWeapon.activeSprite = rangedWeapon.sprites[facingDirection];
                if (Input.GetMouseButton(0))
                {
                    SetChargedWeaponSprite(rangedWeapon);
                }
                if (Input.GetMouseButtonUp(0)){
                    chargedAim = 0;
                }
                Debug.Log(chargedAim);
                RenderWeaponSprite(rangedWeapon);
            }
        }
    }

    public void FireRangedWeapon(RangedWeaponItem rangedWeaponItem){

    }

    public void SetChargedWeaponSprite(RangedWeaponItem rangedWeapon)
    {
        if (chargedAim < rangedWeapon.aimChargeDuration){
            chargedAim += Time.deltaTime;
        }
        switch (chargedAim){
            case float n when n <= (1f / 3f) * rangedWeapon.aimChargeDuration:
                rangedWeapon.activeSprite = rangedWeapon.chargingSprites[facingDirection].chargingSprites1D[0];
                break;
            case float n when n <= (2f / 3f) * rangedWeapon.aimChargeDuration:
                rangedWeapon.activeSprite = rangedWeapon.chargingSprites[facingDirection].chargingSprites1D[1];
                break;
            default:
                rangedWeapon.activeSprite = rangedWeapon.chargingSprites[facingDirection].chargingSprites1D[2];
                break;
        }
    }

    public void SetFacingDirection(){
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimingAngle = mousePosition - rb.position;
        float aimingDirection = Mathf.Atan2(aimingAngle.y, aimingAngle.x) * Mathf.Rad2Deg + 45f;
        switch (aimingDirection){
            case float n when (n > 0f && n <= 90f):
                weaponPosition = transform.position + new Vector3(0.4f, 1f, 0);
                layerOrder = 11;
                facingDirection = 0;
                break;
            case float n when (n > 90f && n <= 180f):
                weaponPosition = transform.position + new Vector3(0, 1.5f, 0);
                layerOrder = 9;
                facingDirection = 1;
                break;
            case float n when (n > 180f && n <= 270f):
                weaponPosition = transform.position + new Vector3(-0.4f, 1f, 0);
                layerOrder = 11;
                facingDirection = 2;
                break;
            default:
                weaponPosition = transform.position + new Vector3(0, 0.75f, 0);
                layerOrder = 11;
                facingDirection = 3;
                break;
        }
    }

    public void RenderWeaponSprite(WeaponItem selectedItem){
        SpriteRenderer spriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = selectedItem.activeSprite;
        spriteRenderer.sortingOrder = layerOrder;
        transform.GetChild(0).gameObject.transform.position = weaponPosition;
    }

    public void RenderArrowSprite(){
    }
}

