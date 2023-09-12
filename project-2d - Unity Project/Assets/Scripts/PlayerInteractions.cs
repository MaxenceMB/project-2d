using UnityEngine;

public class PlayerInteractions : MonoBehaviour {

    private bool selected = false;                          // True when the player is in a zone which he can interact with
    private InteractibleBehaviour lastEncountered;


    void Update() {
        // Checks if the player is in the zone of an interactible object
        if(selected) {

            // If the player presses 'E', the correct interaction starts
            if(Input.GetKeyDown(KeyCode.E)) {
                lastEncountered.Interact();
            }
        }
    }

    // Checks trigger entrances
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Interactible") {
            this.selected = true;
            if(other.gameObject.GetComponent<InteractibleBehaviour>() != null) {
                this.lastEncountered = other.gameObject.GetComponent<InteractibleBehaviour>();
            }
        }
    }

    // Checks trigger exits
    void OnTriggerExit2D(Collider2D other) {
        this.selected = false;
    }
    
}
