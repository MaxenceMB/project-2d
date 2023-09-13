using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour {
    
    public Camera mainCamera;
    public Vector2 mousePosition;
    public Rigidbody2D rb;
    public AimingDirection aimingDirection;

    void Update(){
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimingDirection = mousePosition - rb.position;
        float aimingAngle = Mathf.Atan2(aimingDirection.y, aimingDirection.x) * Mathf.Rad2Deg - 90f;
        //Debug.Log(aimingAngle);
    }

    public int SetWeaponSprite(float aimingAngle){
        switch (aimingAngle){
            case float n when (n > -22.75 && n <= 22.75):
                aimingDirection = AimingDirection.TOP;
                return 0;
            case float n when (n > 22.75 && n <= 67.75):
                aimingDirection = AimingDirection.LEFT;
                return 1;
            case float n when (n > 67.75 && n <= 112.75):
                aimingDirection = AimingDirection.LEFT;
                return 2;
            case float n when (n > 112.75 && n <= 157.75):
                aimingDirection = AimingDirection.LEFT;
                return 3;
            case float n when (n > 112.75 && n <= 157.75):
                aimingDirection = AimingDirection.BOTTOM;
                return 4;
            case float n when (n > 157.75 && n <= 202.75):
                aimingDirection = AimingDirection.RIGHT;
                return 5;
            case float n when (n > 202.75 && n <= 247.75):
                aimingDirection = AimingDirection.RIGHT;
                return 6;
            case float n when (n > 247.75 && n <= 292.75):
                aimingDirection = AimingDirection.RIGHT;
                return 7;
            default:
                return 0;
        }

    }
}

public enum AimingDirection{
    TOP, LEFT, BOTTOM, RIGHT
}
