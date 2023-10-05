using TMPro;
using UnityEngine;

public class PauseScript : MonoBehaviour {

    private static bool canPause = true;
    private static bool isPaused = false;

    private bool setup = false;

    [SerializeField] private GameObject pauseMenu;
    
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) && canPause) {
            if(!isPaused) {
                PauseGame();
            } else {
                UnpauseGame();
            }
        }
    }

    public static void SetCanPause(bool b) {
        canPause = b;
    }

    private void PauseGame() {
        if(!setup) {
            SetupPause();
        }
        
        Time.timeScale = 0;

        isPaused = true;
        pauseMenu.SetActive(true);
    }

    private void UnpauseGame() {
        isPaused = false;
        pauseMenu.SetActive(false);

        Time.timeScale = 1;
    }

    private void SetupPause() {
        GameObject pausePanel = pauseMenu.transform.GetChild(0).gameObject;
        GameObject pauseText  = pauseMenu.transform.GetChild(1).gameObject;

        float[] canvasSize = ScreenTexts.GetCanvasSize();

        pausePanel.transform.position = new Vector2(0.5f*canvasSize[0], 0.5f*canvasSize[1]);
        pausePanel.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasSize[0], canvasSize[1]);

        pauseText.transform.position = new Vector2(0.5f*canvasSize[0], 0.8f*canvasSize[1]);
        pauseText.GetComponent<RectTransform>().sizeDelta = new Vector2(0.5f*canvasSize[0], 0.15f*canvasSize[1]);
        pauseText.GetComponent<TMP_Text>().fontSize = ScreenTexts.CorrectFontSize(pauseText.GetComponent<TMP_Text>().fontSize);

        pauseMenu.SetActive(false);

        setup = true;
    }
}
