using UnityEngine;

[CreateAssetMenu(fileName = "New_Armor", menuName = "Gear/Armor/Armor")]
public class Armor : ScriptableObject {
    
    [Header("Armor Object")]
    [SerializeField] private string armorName;

    [Header("Stats")]
    [SerializeField] private int armorDef;
    [SerializeField] private int armorHP;
    [SerializeField] private int armorSpd;

    [Header ("Description")]
    [SerializeField] private string description;

    /// <summary>
    /// returns the name of the armor
    /// </summary>
    /// <returns> name of the armor </returns>
    public string getName() {
        return this.armorName;
    }

    /// <summary>
    /// returns a single stat of the armor corresponding to the asked stat
    /// </summary>
    /// <param name="stat"> asked statistic </param>
    /// <returns> value of the asked statistic </returns>
    public int getOneArmorStat(string stat) {
        switch (stat) {
            case "def":
                return armorDef;
            case "hp":
                return armorHP;
            case "spd":
                return armorSpd;
            default: 
                return 0;
        }
    }

    /// <summary>
    /// returns all of the armors stats in an array
    /// </summary>
    /// <returns> array of all stats of the armor </returns>
    public int[] getArmorStats() {
        int[] stats =  {0, armorDef, armorHP, armorSpd};
        return stats; 
    }

}
