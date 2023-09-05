using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private string name;
    private string description;

    public Item(string name, string description){
        this.name = name;
        this.description = description;
    }

    public string getName(){
        return this.name;
    }

    public string getDescription(){
        return this.description;
    }
}
