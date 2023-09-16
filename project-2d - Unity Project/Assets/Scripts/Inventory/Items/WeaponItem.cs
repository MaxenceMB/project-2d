using UnityEngine;

    [CreateAssetMenu(menuName = "Items/Weapon item")]
public class WeaponItem : Item {
    
    public int damage = 1;
    [Header("Sprites (RIGHT, TOP, LEFT, BOTTOM)")]
    public Sprite[] sprites = new Sprite[4];
    public Sprite activeSprite;
    
}
