using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(menuName = "Items/Item")]
public class Item : ScriptableObject
{
    [Header("Gameplay")]
    [SerializeField] public string itemName;
    [SerializeField] public string description;
    [SerializeField] public ItemCategory category;

    [Header("UI")]
    [SerializeField] public Sprite icon;
    [SerializeField] public bool stackable;
}

public enum ItemCategory {
    Consumable,
    Weapon
}