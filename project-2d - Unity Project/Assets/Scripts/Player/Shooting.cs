using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour {
    
    public Camera mainCamera;
    public Vector2 mousePosition;
    public Rigidbody2D rb;
    public FacingDirection facingDirection;
    public InventoryManager inventoryManager;
    public float aimingAngle;

    void Update(){
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimingDirection = mousePosition - rb.position;
        aimingAngle = Mathf.Atan2(aimingDirection.y, aimingDirection.x) * Mathf.Rad2Deg + 22.5f;
        SetFacingDirection();
        SetWeaponSprite();
    }

    public int SetFacingDirection(){
        switch (aimingAngle){
            case float n when (n > 0f && n <= 45f):
                facingDirection = FacingDirection.RIGHT;
                return 0;
            case float n when (n > 45f && n <= 90f):
                facingDirection = FacingDirection.RIGHT;
                return 1;
            case float n when (n > 90f && n <= 135f):
                facingDirection = FacingDirection.TOP;
                return 2;
            case float n when (n > 135f && n <= 180f):
                facingDirection = FacingDirection.LEFT;
                return 3;
            case float n when (n > -45f && n <= 0f):
                facingDirection = FacingDirection.RIGHT;
                return 4;
            case float n when (n > -90f && n <= 45f):
                facingDirection = FacingDirection.BOTTOM;
                return 5;
            case float n when (n > -135f && n <= -90f):
                facingDirection = FacingDirection.LEFT;
                return 6;
            default:
                facingDirection = FacingDirection.LEFT;
                return 7;
        }
    }

    public void SetWeaponSprite(){
        Item itemInHands = inventoryManager.GetSelectedItem();
        if (itemInHands is WeaponItem){
            WeaponItem weaponItemInHands = (WeaponItem) itemInHands;
            weaponItemInHands.activeSprite = weaponItemInHands.sprites[SetFacingDirection()];
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = weaponItemInHands.activeSprite;
        }
    }
}

public enum FacingDirection{
    TOP,
    LEFT,
    BOTTOM,
    RIGHT
}
