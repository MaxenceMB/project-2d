using System.Collections;
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

    // Arrow sprite
    public GameObject arrowGO;
    private float arrowOffset = 0.25f;

    // Aiming down sight
    private float chargedPower = 0f;

    void Update(){
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Item selectedItem = inventoryManager.GetSelectedItem();
        if (selectedItem is RangedWeaponItem){
            RangedWeaponItem rangedWeapon = (RangedWeaponItem) selectedItem;
            if (Input.GetMouseButton(0)){
                ChargeRangedWeapon(rangedWeapon);
            } else if (Input.GetMouseButtonUp(0) && chargedPower >= rangedWeapon.minimumFiringTreshold){
                FireRangedWeapon(rangedWeapon);
            } else {
                chargedPower = 0;
            }
            RenderWeaponSprite(rangedWeapon);  
            RenderArrowSprite(rangedWeapon);
        }
    }

    public void ChargeRangedWeapon(RangedWeaponItem rangedWeapon){
        if (chargedPower < rangedWeapon.maxChargedPower){
            chargedPower += Time.deltaTime;
        }
    }

    public void FireRangedWeapon(RangedWeaponItem rangedWeapon){
        float arrowSpeed = chargedPower * 10f;
        chargedPower = 0f;

        // Calculating the spawning arrow's rotation
        Vector2 aimingAngle = mousePosition - new Vector2(transform.GetChild(0).gameObject.transform.position.x, transform.GetChild(0).gameObject.transform.position.y);
        float aimingDirection = Mathf.Atan2(aimingAngle.y, aimingAngle.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, aimingDirection));

        Arrow arrow = Instantiate(rangedWeapon.arrow, transform.GetChild(0).gameObject.transform.position, rotation).GetComponent<Arrow>();
        arrow.arrowVelocity = Mathf.Min(arrowSpeed + rangedWeapon.minimumFiringPower, rangedWeapon.maxChargedPower * 10f);
    }

    public void SetRangedWeaponSprite(RangedWeaponItem rangedWeapon){
        switch (chargedPower){
            case float n when n <= (1f / 3f) * rangedWeapon.maxChargedPower:
                arrowOffset = 0.03f;
                rangedWeapon.activeSprite = rangedWeapon.chargingSprites[facingDirection].chargingSprites1D[0];
                break;
            case float n when n <= (2f / 3f) * rangedWeapon.maxChargedPower:
                arrowOffset = -0.03f;
                rangedWeapon.activeSprite = rangedWeapon.chargingSprites[facingDirection].chargingSprites1D[1];
                break;
            default:
                arrowOffset = -0.06f;
                rangedWeapon.activeSprite = rangedWeapon.chargingSprites[facingDirection].chargingSprites1D[2];
                break;
        }
    }

    public void SetFacingDirection(){
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
            case float n when (n > 180f || n < -90f):
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
        if (chargedPower == 0f){
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

    public void RenderArrowSprite(RangedWeaponItem rangedWeapon){
        if (rangedWeapon.arrow != null){
            transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            SpriteRenderer arrowSprite = arrowGO.GetComponent<SpriteRenderer>();
            arrowSprite.sprite = rangedWeapon.arrow.GetComponent<SpriteRenderer>().sprite;
            if (chargedPower == 0f){
                arrowOffset = 0.25f;
                arrowGO.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
                arrowGO.transform.position = weaponPosition + new Vector2(0f, -arrowOffset);
            } else {
                switch(facingDirection){
                    case 0:
                        arrowGO.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        arrowGO.transform.position = weaponPosition + new Vector2(arrowOffset, 0f);
                        break;
                    case 1:
                        arrowGO.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                        arrowGO.transform.position = weaponPosition + new Vector2(0f, arrowOffset);
                        break;
                    case 2:
                        arrowGO.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                        arrowGO.transform.position = weaponPosition + new Vector2(-arrowOffset, 0f);
                        break;
                    default:
                        arrowGO.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
                        arrowGO.transform.position = weaponPosition + new Vector2(0f, -arrowOffset);
                        break;
                }
            }
            arrowSprite.sortingOrder = layerOrder + 1;
        }
    }
}

