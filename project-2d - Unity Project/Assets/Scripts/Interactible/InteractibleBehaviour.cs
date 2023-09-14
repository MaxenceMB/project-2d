using UnityEngine;
using System.Collections;

public enum InteractibleType { PNJ, Chest, Sign, NotInteractible };

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

    private void Start() {
        this.GetComponent<CircleCollider2D>().radius = detectionDistance;
        this.inputPrompt = this.transform.GetChild(0).gameObject;

        this.inputPromptSprite = this.inputPrompt.GetComponent<SpriteRenderer>();
        this.inputPrompt.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + promptHeight);
        this.inputPrompt.SetActive(false);
    }

    public void Interact() {
        switch(type) {
            case InteractibleType.PNJ:
                Debug.Log("PNJ");
                break;

            case InteractibleType.Chest:
                Debug.Log("CHEST");
                this.type = InteractibleType.NotInteractible;
                break;

            case InteractibleType.Sign:
                if(interactibleObject.GetType == typeof(SignObject)) {
                    SignObject sign = (SignObject)interactibleObject;
                    sign.Interact(); 
                }
                break;
        }
    }

    public void ShowInputPrompt() {
        inputPrompt.SetActive(true);
    }

    public void HideInputPrompt() {
        inputPrompt.SetActive(false);
    }

    public void ClickInputAnimation() {
        inputPromptSprite.sprite = clicked_sprite;
    }

    public void ReleaseInputAnimation() {
        inputPromptSprite.sprite = released_sprite;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);
    }
}
