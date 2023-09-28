using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(menuName = "Items/Item")]
public class Item : ScriptableObject
{
    [Header("Infos")]
    [SerializeField] public string itemName;
    [SerializeField] public string description;

    [Header("UI")]
    [SerializeField] public Sprite icon;
    [SerializeField] public bool stackable;

    [SerializeField] public int price;
}
