using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;

    public Item(string name, string description){
        this.itemName = name;
        this.description = description;
    }

    public string getName(){
        return this.itemName;
    }

    public string getDescription(){
        return this.description;
    }

    public Sprite getIcon(){
        return this.icon;
    }
}
