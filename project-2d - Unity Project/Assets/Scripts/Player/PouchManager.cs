using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// manages the player's pouch
/// </summary>

public class PouchManager : MonoBehaviour
{

    /// <summary>
    /// the text field displaying the amount inside the pouch
    /// </summary>
    [SerializeField] TMP_Text PouchDisplay;

    /// <summary>
    /// the current amount of money in the player's pouch
    /// </summary>
    private int balance;

    /// <summary>
    /// indicates whether the co-routine shakePouch is running or not
    /// </summary> 
    private bool CRShakePouch_running;

    // Start is called before the first frame update
    void Start()
    {
        balance = 200;
        PouchDisplay.SetText(balance.ToString());
        CRShakePouch_running = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// co-routine making the pouch shake, usually to signify
    /// the current amount of money isnt enough
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShakePouch() {
        //declares that the co-routine is running
        CRShakePouch_running = true;
        //makes the pouch shake
        float x = PouchDisplay.transform.position.x;
        float y = PouchDisplay.transform.position.y;
        float z = PouchDisplay.transform.position.z;
        PouchDisplay.transform.position = new Vector3(x+3,y,z);
        yield return new WaitForSeconds(0.1f);
        PouchDisplay.transform.position = new Vector3(x-3,y,z);
        yield return new WaitForSeconds(0.1f);
        PouchDisplay.transform.position = new Vector3(x,y,z);
        yield return null;
        //declares that the co-routine isn't running anymore
        CRShakePouch_running = false;
    }

    /// <summary>
    /// adds money to the pouch
    /// </summary>
    /// <param name="amount"> amount of money added </param>
    public void GainMoney(int amount) {
        balance+=amount;
        PouchDisplay.SetText(balance.ToString());
    } 

    /// <summary>
    /// removes money from the pouch
    /// </summary>
    /// <param name="amount"> amount of money removed </param>
    public void LoseMoney(int amount) {
        balance-=amount;
        PouchDisplay.SetText(balance.ToString());
    }

    /// <summary>
    /// returns the balance of the pouch
    /// </summary>
    /// <returns> balance of the pouch </returns>
    public int GetBalance() {
        return balance;
    }

    /// <summary>
    /// checks if the player can currently afford to pay a certain amount,
    /// starts the co-routine ShakePouch() if they can't 
    /// </summary>
    /// <param name="amount"> the amount of money the player has to afford </param>
    /// <returns> true if the player can afford the indicated amount, false if they can't </returns>
    public bool CanAfford(int amount) {
        if (balance < amount) {
            if (!CRShakePouch_running) {
                StartCoroutine(ShakePouch());
            }
        }
        return balance >= amount;
    }

}
