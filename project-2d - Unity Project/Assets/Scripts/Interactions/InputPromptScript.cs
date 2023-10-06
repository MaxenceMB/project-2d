using UnityEngine;
using System.Collections;

public class InputPromptScript : MonoBehaviour {

    [SerializeField] private Sprite released_sprite;
    [SerializeField] private Sprite clicked_sprite;
    [Space(10)]
    [SerializeField] private float promptHeight;

    private SpriteRenderer inputPromptSprite;
    [HideInInspector] public bool promptVisible;



    /// <summary>
    /// Setup the prompt correctly (height, active state etc)
    /// </summary>
    public void Setup(Transform parent, float height) {
        this.inputPromptSprite = this.gameObject.GetComponent<SpriteRenderer>();
        this.gameObject.SetActive(false);
        this.promptHeight = height;
        this.promptVisible = false;
    }


    /// <summary>
    /// Animates the prompt correctly
    /// </summary>
    /// <param name="selected"> bool: true if the player is in the zone of the interactible </param>
    public void InputPromptAnimation(bool selected) {
        if(selected) {
            if(!promptVisible) {
                ShowInputPrompt();                  // Going up Animation
            }

            if(Input.GetKeyDown(KeyCode.E)) {
                ClickInputAnimation();              // Pressed key sprite
            }

            if(Input.GetKeyUp(KeyCode.E)) {
                ReleaseInputAnimation();            // Released key sprite
            }
        } else {
            if(promptVisible) {
                ReleaseInputAnimation();            // Put release sprite if player leaves without releasing it
                HideInputPrompt();                  // Going down Animation
            }
        }
    }


    /// <summary>
    /// Animates the prompt: goes up
    /// </summary>
    private void ShowInputPrompt() {
        promptVisible = true;
        this.gameObject.SetActive(true);

        Vector2 dest = new Vector2(this.gameObject.transform.parent.position.x, this.gameObject.transform.parent.position.y + promptHeight);
        StopAllCoroutines();
        StartCoroutine(PromptAnimation(dest));
    }


    /// <summary>
    /// Animates the prompt: goes down
    /// </summary>
    private void HideInputPrompt() {
        promptVisible = false;

        Vector2 dest = new Vector2(this.gameObject.transform.parent.position.x, this.gameObject.transform.parent.position.y);
        StopAllCoroutines();
        StartCoroutine(PromptAnimation(dest));
    }


    /// <summary>
    /// Animates the prompt: key pressed
    /// </summary>
    private void ClickInputAnimation() { inputPromptSprite.sprite = clicked_sprite; }


    /// <summary>
    /// Animates the prompt: key released
    /// </summary>
    private void ReleaseInputAnimation() { inputPromptSprite.sprite = released_sprite; }



    ////////// COROUTINES \\\\\\\\\\


    /// <summary>
    /// Animates the prompt
    /// </summary>
    /// <param name="endPos"> Vector2: The position the prompt will go to </param>
    /// <returns></returns>
    private IEnumerator PromptAnimation(Vector2 endPos) {
        Vector2 currentPos = this.gameObject.transform.position;

        float elapsedTime = 0.0f;
        float waitTime    = 0.25f;

        while (elapsedTime < waitTime) {
            this.gameObject.transform.position = Vector2.Lerp(currentPos, endPos, elapsedTime / waitTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        } 

        // Make sure we got there
        this.gameObject.transform.position = endPos;
        this.gameObject.SetActive(promptVisible);
        yield return null;
    }
}