using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    [SerializeField] TMP_Text PouchDisplay;

    private int balance;

    // Start is called before the first frame update
    void Start()
    {
        balance = 0;
        PouchDisplay.SetText(balance.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            gainMoney(500);
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            loseMoney(200);
        }
    }

    public void gainMoney(int amount) {
        balance+=amount;
        PouchDisplay.SetText(balance.ToString());
    } 

    public void loseMoney(int amount) {
        balance-=amount;
        PouchDisplay.SetText(balance.ToString());
    }

    public int getBalance() {
        return balance;
    }

}
