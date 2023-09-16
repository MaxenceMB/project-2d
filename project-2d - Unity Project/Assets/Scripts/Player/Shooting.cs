using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour {
    
    // GameObjects
    public Rigidbody2D rb;
    public InventoryManager inventoryManager;

    // Directions
    public Camera mainCamera;
    private Vector2 mousePosition;
    private int facingDirection;

    // Weapon sprite
    private int layerOrder;
    private Vector2 weaponPosition;

    // Projectile sprite
    private Vector2 projectilePosition;
    public GameObject projectileGO;
    private float arrowScale;
    private float arrowRotation;
    public float arrowOffset = 0.25f;

    // Aiming down sight
    private float chargedAim = 0f;

    void Update(){
        Item selectedItem = inventoryManager.GetSelectedItem();
        if (selectedItem is RangedWeaponItem){
            RangedWeaponItem rangedWeapon = (RangedWeaponItem) selectedItem;
            if (Input.GetMouseButton(0)){
                ChargeRangedWeapon(rangedWeapon);
            } else if (Input.GetMouseButtonUp(0)){
                FireRangedWeapon(rangedWeapon);
            }
            RenderWeaponSprite(rangedWeapon);
            RenderProjectileSprite(rangedWeapon);
        }
    }

    public void ChargeRangedWeapon(RangedWeaponItem rangedWeapon){
        if (chargedAim < rangedWeapon.aimChargeDuration){
            chargedAim += Time.deltaTime;
        }
    }

    public void FireRangedWeapon(RangedWeaponItem rangedWeapon){
        chargedAim = 0f;
    }

    public void SetRangedWeaponSprite(RangedWeaponItem rangedWeapon){
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
                layerOrder = 8;
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

    public void RenderWeaponSprite(RangedWeaponItem rangedWeapon){
        SpriteRenderer spriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        if (chargedAim == 0f){
            layerOrder = 11;
            weaponPosition = transform.position + new Vector3(0, 0.75f, 0);
            rangedWeapon.activeSprite = rangedWeapon.sprites[3];
        } else {
            SetFacingDirection();
            SetRangedWeaponSprite(rangedWeapon);
        }
        spriteRenderer.sprite = rangedWeapon.activeSprite;
        spriteRenderer.sortingOrder = layerOrder;
        transform.GetChild(0).gameObject.transform.position = weaponPosition;
    }

    public void RenderProjectileSprite(RangedWeaponItem rangedWeapon){
        if (rangedWeapon.projectile != null){
            SpriteRenderer projectileSprite = projectileGO.GetComponent<SpriteRenderer>();
            projectileSprite.sprite = rangedWeapon.projectile.GetComponent<SpriteRenderer>().sprite;
            if (chargedAim == 0f){
                arrowOffset = 0.25f;
                projectileGO.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
                projectileGO.transform.position = weaponPosition + new Vector2(0f, -arrowOffset);
            } else {
                switch(facingDirection){
                    case 0:
                        projectileGO.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        projectileGO.transform.position = weaponPosition + new Vector2(arrowOffset, 0f);
                        break;
                    case 1:
                        projectileGO.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                        projectileGO.transform.position = weaponPosition + new Vector2(0f, arrowOffset);
                        break;
                    case 2:
                        projectileGO.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                        projectileGO.transform.position = weaponPosition + new Vector2(-arrowOffset, 0f);
                        break;
                    default:
                        projectileGO.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
                        projectileGO.transform.position = weaponPosition + new Vector2(0f, -arrowOffset);
                        break;
                }
            }
            projectileSprite.sortingOrder = layerOrder + 1;
        }
    }
}

