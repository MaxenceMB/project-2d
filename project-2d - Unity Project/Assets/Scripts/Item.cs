using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;
    [SerializeField] private int quantity;

    public void OnEnable(){
        this.quantity = 0;
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

    public int getQuantity(){
        return this.quantity;
    }

    public void AddQuantity(int quantity = 1){
        this.quantity += quantity;
    }

    public void RemoveQuantity(int quantity = 1){
        this.quantity -= quantity;
    }
}
