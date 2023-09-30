using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Firing : MonoBehaviour {
    
    // GameObjects
    public Rigidbody2D rb;
    public InventoryManager inventoryManager;

    // Directions
    public Camera mainCamera;
    private Vector2 mousePosition;
    private int facingDirection;

    // Held bow
    private Bow heldBow = null;

    // Weapon sprite
    private int layerOrder;
    private Vector2 weaponPosition;

    // Arrow sprite
    private GameObject arrowGO;
    private float arrowOffset = 0.25f;

    // Aiming down sight
    private float chargedPower = 0f;

    void Update(){
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Item selectedItem = inventoryManager.GetSelectedItem();
        if (selectedItem is Bow){
            SetHeldBow(selectedItem);
            if (Input.GetMouseButton(0)){
                ChargeBow();
            } else if (Input.GetMouseButtonUp(0) && chargedPower >= heldBow.minimumFiringTreshold){
                FireBow();
            } else {
                chargedPower = 0;
            }
            RenderBowSprite();  
            RenderArrowSprite();
        } else {
            SetHeldBow(null, true);
        }
    }


    /// <summary>
    /// Sets the attribute 'heldBow' to the currently held bow by the player or back to null
    /// </summary>
    /// <param name="selectedItem"> Item: the bow held by the player </param>
    /// <param name="setNull"     > bool: Specifies wether the attribute should be set 
    ///                                   to the held bow or to null </param>
    public void SetHeldBow(Item selectedItem, bool setNull = false){
        if (heldBow == null && !setNull){
            heldBow = (Bow) selectedItem;
        } else if (heldBow != null && setNull){
            heldBow = null;
        }
    }


    /// <summary>
    /// Increases the charged power by the time that passes in a frame
    /// if the charged power hasn't reached the weapon's maximum charge
    /// </summary>
    public void ChargeBow(){
        if (chargedPower < heldBow.maxChargedPower){
            chargedPower += Time.deltaTime;
        }
    }


    /// <summary>
    /// Sets the charged power back to zero and spawns a projectile 
    /// headed in the direction of the mouse's position
    /// </summary>
    public void FireBow(){
        float arrowSpeed = chargedPower * 10f;
        chargedPower = 0f;

        // Calculating the spawning arrow's rotation
        Vector2 aimingAngle = mousePosition - new Vector2(transform.GetChild(0).gameObject.transform.position.x, transform.GetChild(0).gameObject.transform.position.y);
        float aimingDirection = Mathf.Atan2(aimingAngle.y, aimingAngle.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, aimingDirection));

        ArrowBehaviour arrow = Instantiate(heldBow.arrow, transform.GetChild(0).gameObject.transform.position, rotation).GetComponent<ArrowBehaviour>();
        arrow.arrow.velocity = Mathf.Min(arrowSpeed + (heldBow.minimumFiringPower * 10f), heldBow.maxChargedPower * 10f);
    }


    /// <summary>
    /// Sets the facing direction based on the mouse's position and the player position
    ///     -   Sets the 'facingDirection' attribute to a value of the Direction enum
    ///     -   Sets the 'layerOrder' attribute depending on whether the bow's sprite should be behind or in front of the player
    ///     -   Sets the 'weaponPosition' attribute to the position the bow should be in based on the facing direction
    /// </summary>
    public void SetFacingDirection(){
        Vector2 aimingAngle = mousePosition - rb.position;
        float aimingDirection = Mathf.Atan2(aimingAngle.y, aimingAngle.x) * Mathf.Rad2Deg + 45f;
        switch (aimingDirection){
            case float n when (n > 0f && n <= 90f):
                weaponPosition = transform.position + new Vector3(0.4f, 1f, 0);
                layerOrder = 11;
                facingDirection = (int) Direction.TOP;
                break;
            case float n when (n > 90f && n <= 180f):
                weaponPosition = transform.position + new Vector3(0, 1.5f, 0);
                layerOrder = 8;
                facingDirection = (int) Direction.RIGHT;
                break;
            case float n when (n > 180f || n < -90f):
                weaponPosition = transform.position + new Vector3(-0.4f, 1f, 0);
                layerOrder = 11;
                facingDirection = (int) Direction.BOTTOM;
                break;
            default:
                weaponPosition = transform.position + new Vector3(0, 0.75f, 0);
                layerOrder = 11;
                facingDirection = (int) Direction.LEFT;
                break;
        }
    }


    /// <summary>
    /// Set the bow's active sprite based on the charged amount and the facing direction :
    ///     -   slightly charged if the charged amount is less than 1/3 of maximum charge amount
    ///     -   half charged if the charged amount is between 1/3 and 2/3 of maximum charge amount
    ///     -   fully charged if the charged amount is greater than 2/3 of maximum charge amount
    /// </summary>
    public void SetBowActiveSprite(){
        switch (chargedPower){
            case float n when n <= (1f / 3f) * heldBow.maxChargedPower:
                arrowOffset = 0.03f;
                heldBow.activeSprite = heldBow.chargingSprites[facingDirection].chargingSprites1D[0];
                break;
            case float n when n <= (2f / 3f) * heldBow.maxChargedPower:
                arrowOffset = -0.03f;
                heldBow.activeSprite = heldBow.chargingSprites[facingDirection].chargingSprites1D[1];
                break;
            default:
                arrowOffset = -0.06f;
                heldBow.activeSprite = heldBow.chargingSprites[facingDirection].chargingSprites1D[2];
                break;
        }
    }


    /// <summary>
    /// Renders the bow active sprite
    /// </summary>
    public void RenderBowSprite(){
        SpriteRenderer spriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        if (chargedPower == 0f){
            layerOrder = 11;
            weaponPosition = transform.position + new Vector3(0, 0.75f, 0);
            heldBow.activeSprite = heldBow.sprites[3];
        } else {
            SetFacingDirection();
            SetBowActiveSprite();
        }
        spriteRenderer.sprite = heldBow.activeSprite;
        spriteRenderer.sortingOrder = layerOrder;
        transform.GetChild(0).gameObject.transform.position = weaponPosition;
    }


    /// <summary>
    /// Renders the arrow's sprite based on the facing direction
    /// </summary>
    public void RenderArrowSprite(){
        if (heldBow.arrow != null){
            transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            arrowGO = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
            SpriteRenderer arrowSprite = arrowGO.GetComponent<SpriteRenderer>();
            arrowSprite.sprite = heldBow.arrow.GetComponent<SpriteRenderer>().sprite;
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

