using UnityEngine;

public class PlayerStatistics : MonoBehaviour

{

    //base statistics of the player
    private int[] playerBaseStats;
    //gear statistics of the player
    private int[] playerGearStats;
    //armor equipped by the player
    private Armor equippedArmor;

    // Start is called before the first frame update
    void Start()
    {
        playerBaseStats = new int[] {100, 100, 20, 4};
        playerGearStats = new int[] {0, 0, 0, 0};
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// sets all of the saved statistics according to the given array of int stats
    /// </summary>
    /// <param name="stats"> stats[0] = atk, stats[1] = hp, stats[2] = def, stats[3] = spd </param>
    public void setBaseStats(int[] stats) {
        playerBaseStats = stats;
    }

    /// <summary>
    /// returns an array containing all the base statistics of the player
    /// </summary>
    /// <returns> stats[0] = atk, stats[1] = hp, stats[2] = def, stats[3] = spd </returns>
    public int[] getBaseStats() {
        return playerBaseStats;
    }

    /// <summary>
    /// returns an array containing all the gear statistics of the player
    /// </summary>
    /// <returns> stats[0] = atk, stats[1] = hp, stats[2] = def, stats[3] = spd </returns>
    public int[] getGearStats() {
        return playerGearStats;
    } 

     /// <summary>
    /// returns an array containing all the statistics of the player
    /// </summary>
    /// <returns> stats[0] = atk, stats[1] = hp, stats[2] = def, stats[3] = spd </returns>
    public int[] getStats() {
        int[] stats = new int[4];
        for (int i = 0; i<4; i++) {
            stats[i] = playerBaseStats[i]+playerGearStats[i];
        }
        return stats;
    }

    /// <summary>
    /// allows you to equip an armor to the player
    /// </summary>
    /// <param name="equippedArmor"> the armor to be equipped </param>
    public void equipArmor(Armor equippedArmor) {
        this.equippedArmor = equippedArmor;
        if (this.equippedArmor == null) {
            for (int i = 0; i<this.equippedArmor.getArmorStats().Length-1; i++) {
                playerGearStats[i+1] = 0;
            }
        } else {
            for (int i = 0; i<this.equippedArmor.getArmorStats().Length-1; i++) {
                playerGearStats[i+1] = this.equippedArmor.getArmorStats()[i+1];
            } 
        } 
    }

    /// <summary>
    /// returns the currently equiped armor of the player
    /// </summary>
    /// <returns></returns>
    public Armor getEquippedArmor() {
        return this.equippedArmor;
    }

}
