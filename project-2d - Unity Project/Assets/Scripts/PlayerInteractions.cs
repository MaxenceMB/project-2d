using UnityEngine;

enum InteractCases { None, Pnj, Chest }
public class PlayerInteractions : MonoBehaviour {

    private bool selected = false;                          // True when the player is in a zone which he can interact with
    private InteractCases interact = InteractCases.None;    // Keep in memory which type of interaction the player can have


    void Update() {
        // Checks if the player is in the zone of an interactible object
        if(selected && this.interact != InteractCases.None) {

            // If the player presses 'E', the correct interaction starts
            if(Input.GetKeyDown(KeyCode.E)) {
                StartInteraction();
            }
        }
    }

    // Starts the corrects 
    private void StartInteraction() {
        switch(this.interact) {
            case InteractCases.Pnj:
                Debug.Log("Interacted with a NPC!");
                break;

            case InteractCases.Chest:
                Debug.Log("Interacted with a Chest!");
                break;
        }
    }

    // Checks trigger entrances
    void OnTriggerEnter2D(Collider2D other) {
        this.selected = true;

        switch(other.gameObject.tag) {
            case "interact_PNJ":
                this.interact = InteractCases.Pnj;
                break;

            case "interact_Chest":
                this.interact = InteractCases.Chest;
                break;

            default:
                this.interact = InteractCases.None;
                break;
        }
    }

    // Checks trigger exits
    void OnTriggerExit2D(Collider2D other) {
        this.selected = false;
    }
    
}
