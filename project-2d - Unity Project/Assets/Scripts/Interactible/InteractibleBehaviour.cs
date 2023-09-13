using UnityEngine;
using System.Threading.Tasks;

public enum Interactible { PNJ, Chest, NotInteractible };

public class InteractibleBehaviour : MonoBehaviour {

    [SerializeField] private float detectionDistance, promptHeight;
    [SerializeField] public Interactible type;

    [Header("Input Prompt")]
    [SerializeField] private Sprite released_sprite;
    [SerializeField] private Sprite clicked_sprite;
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
            case Interactible.PNJ:
                Debug.Log("PNJ");
                break;

            case Interactible.Chest:
                Debug.Log("CHEST");
                this.type = Interactible.NotInteractible;
                break;
        }
    }

    public void ShowInputPrompt() {
        inputPrompt.SetActive(true);
    }

    public void ClickInputPrompt() {
        inputPromptSprite.sprite = clicked_sprite;
    }

    public void HideInputPrompt() {
        inputPrompt.SetActive(false);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);
    }
}
