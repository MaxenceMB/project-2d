using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class Inventory {

    private int maxSlots = 5;
    private List<Item> items;
    private List<int> quantityOfItems;

    public Inventory(int maxSlots){
        this.maxSlots = maxSlots;
        this.items = new List<Item>();
        this.quantityOfItems = new List<int>();
    }

    public void AddItem(Item item, int quantity = 1){
        // Exception
        if (this.items.Count == maxSlots){
            throw new OverflowException("Inventory has reached maximum slots !");
        }
        // End exceptions

        if (this.items.Contains(item)){
            this.quantityOfItems[this.items.IndexOf(item)] += 1;
        } else {
            this.items.Add(item);
            this.quantityOfItems.Add(quantity);
        }
    }

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
    }

    public override string ToString(){
        string str = "Inventory : [\n";
        for (int i = 0; i < this.items.Count; i++){
            str += "Slot " + (i+1) + " : " + this.items[i].getName() + " (" + this.quantityOfItems[i] + ")\n";
        }
        str += "]";
        return str;
    }

}
