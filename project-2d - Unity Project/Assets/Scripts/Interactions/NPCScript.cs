using UnityEngine;

public class NPCScript : MonoBehaviour {

    [Header("NPC")]
    [SerializeField] private NPCObject  npcObject;
    [SerializeField] private Sprite     npcSprite;
    [Space(10)]
    [SerializeField] private float      detectionDistance;
                     private bool       selected;
                     private bool       left = true;

    [Header("Input Prompt")]
    [SerializeField] private GameObject        ipPrefab;
    [SerializeField] private float             promptHeight;
                     private GameObject        ip;
                     private InputPromptScript ips;

    

    /// <summary>
    /// 
    /// </summary>
    private void Start() {
        this.GetComponent<SpriteRenderer>().sprite = npcSprite;             // Sets correct sprite
        this.GetComponent<CircleCollider2D>().radius = detectionDistance;   // Sets detection distance

        ip = GameObject.Instantiate(ipPrefab, this.transform);
        this.ips = ip.GetComponent<InputPromptScript>();
        this.ips.Setup(this.gameObject.transform, this.promptHeight);
    }


    /// <summary>
    /// 
    /// </summary>
    private void Update() {
        
        if(selected && npcObject.canInteract) {
            if(Input.GetKeyDown(KeyCode.E)) {
                PauseScript.SetCanPause(false);
                this.npcObject.Interact();
            }
        }
        
        ips.InputPromptAnimation(selected);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            this.selected = npcObject.canInteract;
            this.left     = !npcObject.canInteract;
        }
    }

    // Checks trigger exits
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player") this.selected = false;
    }


    
    ////////// EDITOR FUNCTIONS \\\\\\\\\\
    

    /// <summary>
    /// 
    /// </summary>
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);
    }
}
