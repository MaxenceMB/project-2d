using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PouchManager : MonoBehaviour
{

    [SerializeField] TMP_Text PouchDisplay;

    private int balance;
    private bool CR_running;

    // Start is called before the first frame update
    void Start()
    {
        balance = 0;
        PouchDisplay.SetText(balance.ToString());
        CR_running = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            GainMoney(500);
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            if (balance < 200 && !CR_running) {
                CR_running = true;
                StartCoroutine(ShakePouch());
            } else if (balance >= 200) {
                LoseMoney(200);
            }
        }
    }

    public IEnumerator ShakePouch() {
        float x = PouchDisplay.transform.position.x;
        float y = PouchDisplay.transform.position.y;
        float z = PouchDisplay.transform.position.z;
        PouchDisplay.transform.position = new Vector3(x+3,y,z);
        yield return new WaitForSeconds(0.1f);
        PouchDisplay.transform.position = new Vector3(x-3,y,z);
        yield return new WaitForSeconds(0.1f);
        PouchDisplay.transform.position = new Vector3(x,y,z);
        yield return null;
        CR_running = false;
    }

    public void GainMoney(int amount) {
        balance+=amount;
        PouchDisplay.SetText(balance.ToString());
    } 

    public void LoseMoney(int amount) {
        balance-=amount;
        PouchDisplay.SetText(balance.ToString());
    }

    public int GetBalance() {
        return balance;
    }

    public bool CanAfford(int amount) {
        return balance >= amount;
    }

}
