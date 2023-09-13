using UnityEngine;

    [CreateAssetMenu(menuName = "Items/Weapon item")]
public class WeaponItem : Item {
    
    public Sprite[] sprites;
    public Sprite activeSprite;
    public int damage = 1;
}
