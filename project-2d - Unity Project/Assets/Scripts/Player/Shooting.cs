using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour {
    
    // GameObjects
    public Rigidbody2D rb;
    public InventoryManager inventoryManager;

    // Directions
    public Camera mainCamera;
    public Vector2 mousePosition;
    public FacingDirection facingDirection;

    // Weapon sprite
    public int layerOrder;
    public Vector2 weaponPosition;

    // Aiming down sight
    public float aimChargeDuration = 1.2f;
    public float chargedAim = 0f;

    void Update(){
        SetFacingDirection();
        SetWeaponSprite();
        if (Input.GetMouseButton(1)){
            if (chargedAim < aimChargeDuration){
                chargedAim += Time.deltaTime;
            }
        }
        if (Input.GetMouseButtonUp(1)){
            chargedAim = 0;
        }
        Debug.Log(chargedAim);
    }

    public int SetFacingDirection(){
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimingAngle = mousePosition - rb.position;
        float aimingDirection = Mathf.Atan2(aimingAngle.y, aimingAngle.x) * Mathf.Rad2Deg + 45f;
        switch (aimingDirection){
            case float n when (n > 0f && n <= 90f):
                weaponPosition = transform.position + new Vector3(0.4f, 1f, 0);
                layerOrder = 11;
                facingDirection = FacingDirection.RIGHT;
                return 0;
            case float n when (n > 90f && n <= 180f):
                weaponPosition = transform.position + new Vector3(0, 1.5f, 0);
                layerOrder = 9;
                facingDirection = FacingDirection.TOP;
                return 1;
            case float n when (n > 180f && n <= 270f):
                weaponPosition = transform.position + new Vector3(-0.4f, 1f, 0);
                layerOrder = 11;
                facingDirection = FacingDirection.LEFT;
                return 2;
            default:
                weaponPosition = transform.position + new Vector3(0, 0.75f, 0);
                layerOrder = 11;
                facingDirection = FacingDirection.BOTTOM;
                return 3;
        }
    }

    public void SetWeaponSprite(){
        Item itemInHands = inventoryManager.GetSelectedItem();
        if (itemInHands is WeaponItem){
            WeaponItem weaponItemInHands = (WeaponItem) itemInHands;
            weaponItemInHands.activeSprite = weaponItemInHands.sprites[SetFacingDirection()];
            SpriteRenderer spriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = weaponItemInHands.activeSprite;
            spriteRenderer.sortingOrder = layerOrder;
            transform.GetChild(0).gameObject.transform.position = weaponPosition;
        }
    }
}

public enum FacingDirection{
    TOP,
    LEFT,
    BOTTOM,
    RIGHT
}
