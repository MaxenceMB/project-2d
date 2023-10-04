using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EquipementManager : MonoBehaviour
{

    // stat representing the player's attack
    private int playerAtk;
    //amount of health point the player has
    private int playerHP;
    //stat representing the player's def
    private int playerDef;
    //stat representing the player's speed
    private int playerSpd;

    [SerializeField] Armor[] armorsInInventory;

    private Armor equippedArmor;

    // Start is called before the first frame update
    void Start()
    {
        this.loadStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) {
            Debug.Log("atk: "+playerAtk);
            Debug.Log("hp: "+playerHP);
            Debug.Log("def: "+playerDef);
            Debug.Log("spd: "+playerSpd);
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            this.equipArmor(armorsInInventory[0]);
            Debug.Log("current armor:"+this.equippedArmor.getName());
        }

        if (Input.GetKeyDown(KeyCode.M)) {
            this.equipArmor(armorsInInventory[1]);
            Debug.Log("current armor:"+this.equippedArmor.getName());
        }

    }

    /// <summary>
    /// loads all the saved statistics 
    /// </summary>
    public void loadStats() {
        int[] stats = GameObject.Find("StatsManager").GetComponent<PlayerStatistics>().getStats();
        playerAtk = stats[0];
        playerHP = stats[1];
        playerDef = stats[2];
        playerSpd = stats[3];
        GameObject.Find("Player").GetComponent<PlayerMovement>().setSpeed(playerSpd);
        this.equippedArmor = GameObject.Find("StatsManager").GetComponent<PlayerStatistics>().getEquippedArmor();
    }

    /// <summary>
    /// sets to the designated statistic the given value
    /// </summary>
    /// <param name="stat"> statistic to set </param>
    /// <param name="statToSet"> value to set </param>
    public void setOneBaseStat(int stat, string statToSet) {
        int[] stats = GameObject.Find("StatsManager").GetComponent<PlayerStatistics>().getBaseStats();
        switch (statToSet) {
            case "atk":
                stats[0] = stat;
                break;
            case "hp":
                stats[1] = stat;
                break;
            case "def":
                stats[2] = stat;
                break;
            case "spd":
                stats[3] = stat;
                break;
        }
        GameObject.Find("StatsManager").GetComponent<PlayerStatistics>().setBaseStats(stats);
        this.loadStats();
    }

    /// <summary>
    /// sets the base stats of the player
    /// </summary>
    /// <param name="stats"> array of all the values to set </param>
    public void setBaseStats(int[] stats) {
        GameObject.Find("StatsManager").GetComponent<PlayerStatistics>().setBaseStats(stats);
    }

    /// <summary>
    /// returns the designated statistic 
    /// </summary>
    /// <param name="statToGet"> name designating the stat to get </param>
    /// <returns> the designated stat </returns>
    public int getOneStat(string statToGet) {
        switch (statToGet) {
            case "atk":
                return GameObject.Find("StatsManager").GetComponent<PlayerStatistics>().getStats()[0];
            case "hp":
                return GameObject.Find("StatsManager").GetComponent<PlayerStatistics>().getStats()[1];
            case "def":
                return GameObject.Find("StatsManager").GetComponent<PlayerStatistics>().getStats()[2];
            case "spd":
                return GameObject.Find("StatsManager").GetComponent<PlayerStatistics>().getStats()[3];
            default:
                return 0;
        }
    }

    /// <summary>
    /// equips the given armor to the player
    /// </summary>
    /// <param name="armor"> armor to equip </param>
    public void equipArmor(Armor armor) {
        GameObject.Find("StatsManager").GetComponent<PlayerStatistics>().equipArmor(armor);
        this.loadStats();
    }

    /// <summary>
    /// returns the equipped armor by the player
    /// </summary>
    /// <returns> the equipped armor </returns>
    public Armor getEquippedArmor() {
        return this.equippedArmor;
    }

}
