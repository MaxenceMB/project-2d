using UnityEngine;

public class PlayerInteractions : MonoBehaviour {

    private bool selected = false;                                                // True when the player is in a zone which he can interact with
    private InteractibleBehaviour lastEncountered;                                // Saves the current object the player can interact with

    private PlayerMovement pm;


    private void Start() {
        pm = this.GetComponent<PlayerMovement>();
    }

    private void Update() {

        // Checks if the player is in the zone of an interactible object
        if(selected && lastEncountered.type != InteractibleType.NotInteractible) {

            // If the prompt wasn't visible yet, show it
            if(!lastEncountered.promptVisible) {
                lastEncountered.ShowInputPrompt();
            }

            // If the player presses 'E' and he wasn't interacting already, the correct interaction starts
            if(Input.GetKeyDown(KeyCode.E)) {
                PauseScript.SetCanPause(false);

                lastEncountered.Interact();
                lastEncountered.ClickInputAnimation();
            }

            if(Input.GetKeyUp(KeyCode.E)) {
                lastEncountered.ReleaseInputAnimation();
            }
            
        } else {
            if(lastEncountered != null && lastEncountered.promptVisible) {
                lastEncountered.ReleaseInputAnimation();
                lastEncountered.HideInputPrompt();
            }
        }
    }

    // Checks trigger entrances
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Interactible") {
            this.selected = true;
            if(other.gameObject.GetComponent<InteractibleBehaviour>() != null) {
                this.lastEncountered = other.gameObject.GetComponent<InteractibleBehaviour>();
            }
        }
    }

    // Checks trigger exits
    private void OnTriggerExit2D(Collider2D other) {
        this.selected = false;
    }
    
}
