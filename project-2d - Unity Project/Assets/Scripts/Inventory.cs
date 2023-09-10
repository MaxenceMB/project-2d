using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class Inventory : MonoBehaviour {

    private int maxSlots;
    private List<Item> items;

    private void Start(){
        this.maxSlots = 10;
        this.items = new List<Item>();
    }

    public void AddItem(Item item, int quantity = 1){
        // Exception
        if (this.items.Count == maxSlots){
            throw new OverflowException("Inventory has reached maximum slots !");
        }
        // End exceptions

        if (this.items.Contains(item)){
            this.items[this.items.IndexOf(item)].AddQuantity(quantity);
        } else {
            this.items.Add(item);
            this.items[this.items.IndexOf(item)].AddQuantity(quantity);
        }
    }

/*
    public void RemoveItemAt(int index, int quantity = 1){
        // Exceptions
        if (index < 0 || index >= this.items.Count){
            throw new ArgumentOutOfRangeException("Index is out of range of inventory !");
        } 
        if (quantity < 1 || quantity > this.quantityOfItems[index]){
            throw new ArgumentOutOfRangeException("Quantity is negative or higher than current item's !");
        }
        // End exceptions

        if (quantity == this.quantityOfItems[index]){
            this.items.RemoveAt(index);
            this.quantityOfItems.RemoveAt(index);
        } else {
            this.quantityOfItems[index] -= quantity;
        }
    }

    public void RemoveItem(Item item, int quantity = 1){
        // Exceptions
        if (!this.items.Contains(item)){
            throw new ArgumentException("Item not found in inventory !");
        }
        int indexOfItem = this.items.IndexOf(item);
        if (quantity < 1 || quantity > this.quantityOfItems[indexOfItem]){
            throw new ArgumentOutOfRangeException("Quantity is negative or higher than current item's !");
        }
        // End exceptions
        
        if (quantity == this.quantityOfItems[indexOfItem]){
            this.items.RemoveAt(indexOfItem);
            this.quantityOfItems.RemoveAt(indexOfItem);
        } else {
            this.quantityOfItems[indexOfItem] -= quantity;
        }
    }

    public void clearInventory(){
        this.items.Clear();
        this.quantityOfItems.Clear();
    }*/

    public override string ToString(){
        string str = "Inventory : [\n";
        for (int i = 0; i < this.items.Count; i++){
            str += "Slot " + (i+1) + " : " + this.items[i].getName() + " (" + this.items[i].getQuantity() + ")\n";
        }
        str += "]";
        return str;
    }

}
