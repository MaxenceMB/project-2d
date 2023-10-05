using UnityEngine;
using System.Collections;

public enum InteractibleType { NPC, Chest, Sign, NotInteractible };


public class InteractibleBehaviour : MonoBehaviour {

    [Header("Interactible")]
    [SerializeField] private float detectionDistance;
    [SerializeField] public InteractibleType type;
    [SerializeField] ScriptableObject interactibleObject;

    [Header("Input Prompt")]
    [SerializeField] private Sprite released_sprite;
    [SerializeField] private Sprite clicked_sprite;
    [SerializeField] private float promptHeight;

    private GameObject inputPrompt;
    private SpriteRenderer inputPromptSprite;
    [HideInInspector] public bool promptVisible = false;

    private Vector2 velocity = Vector2.zero;

    private void Start() {
        // Sets detection distance
        this.GetComponent<CircleCollider2D>().radius = detectionDistance;

        // Setups the 'E' prompt
        this.inputPrompt = this.transform.GetChild(0).gameObject;
        this.inputPromptSprite = this.inputPrompt.GetComponent<SpriteRenderer>();
        this.inputPrompt.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
        this.inputPrompt.SetActive(false);
    }

    public void Interact() {
        switch(type) {
            case InteractibleType.NPC:
                if(interactibleObject is NPCObject) {
                    NPCObject npc = (NPCObject)interactibleObject;
                    npc.Interact(); 
                }
                break;
            case InteractibleType.Chest:
                if(interactibleObject is ChestObject) {
                    ChestObject chest = (ChestObject)interactibleObject;
                    chest.Interact(); 
                }
                break;

            case InteractibleType.Sign:
                if(interactibleObject is SignObject) {
                    SignObject sign = (SignObject)interactibleObject;
                    sign.Interact(); 
                }
                break;
        }
    }


    // Input prompt functions
    public void ShowInputPrompt() {
        promptVisible = true;
        this.inputPrompt.SetActive(true);

        Vector2 dest = new Vector2(this.transform.position.x, this.transform.position.y + promptHeight);
        StartCoroutine(PromptAnimation(dest));
    }

    public void HideInputPrompt() {
        Vector2 dest = new Vector2(this.transform.position.x, this.transform.position.y);
        StartCoroutine(PromptAnimation(dest));

        promptVisible = false;
    }

    public void ClickInputAnimation() {
        inputPromptSprite.sprite = clicked_sprite;
    }

    public void ReleaseInputAnimation() {
        inputPromptSprite.sprite = released_sprite;
    }

    private IEnumerator PromptAnimation(Vector2 endPos) {
        Vector2 currentPos = inputPrompt.transform.position;

        float elapsedTime = 0.0f;
        float waitTime    = 0.25f;

        while (elapsedTime < waitTime) {
            inputPrompt.transform.position = Vector2.Lerp(currentPos, endPos, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        } 

        // Make sure we got there
        inputPrompt.transform.position = endPos;
        inputPrompt.SetActive(promptVisible);
        yield return null;
    }

    
    // Editor functions
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);
    }

}
