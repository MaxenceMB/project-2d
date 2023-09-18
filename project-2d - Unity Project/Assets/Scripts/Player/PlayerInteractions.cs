using UnityEngine;

public enum InteractStates { None, Interacting, End }

public class PlayerInteractions : MonoBehaviour {

    private bool selected = false;                          // True when the player is in a zone which he can interact with
    private InteractibleBehaviour lastEncountered;
    [HideInInspector] public static InteractStates state = InteractStates.None;

    private PlayerMovement pm;


    private void Start() {
        pm = this.GetComponent<PlayerMovement>();
    }

    private void Update() {

        // Checks if the player is in the zone of an interactible object
        if(selected && lastEncountered.type != InteractibleType.NotInteractible) {
            lastEncountered.ShowInputPrompt();

            // If the player presses 'E' and he wasn't interacting already, the correct interaction starts
            if(Input.GetKeyDown(KeyCode.E)) {
                state = InteractStates.Interacting;
                lastEncountered.Interact();
                lastEncountered.ClickInputAnimation();
            }
            if(Input.GetKeyUp(KeyCode.E)) {
                lastEncountered.ReleaseInputAnimation();
            }
        } else {
            if(lastEncountered != null) {
                lastEncountered.ReleaseInputAnimation();
                lastEncountered.HideInputPrompt();
            }
        }

        if(state == InteractStates.Interacting) {
            pm.canMove = false;
        } else {
            pm.canMove = true;
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
