using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject loading;
    public Slider slider;
    public Text progressText;

    // This jumps to the game scene
    public void PlayGame() {
        PlayerPrefs.SetInt("LoadScene", 0);
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex + 1));
    }
    
    // This exits the application
    public void QuitGame() {
        Application.Quit();
    }

    // This loads a saved game
    public void LoadGame(){
        PlayerPrefs.SetInt("LoadScene", 1);
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex + 1));
    }

    // Loads next scene while showing loading screen
    IEnumerator LoadAsynchronously (int sceneIndex) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loading.SetActive(true);

        // Whilst loading, if not yet finished loading, show the slider and text progress
        while (operation.isDone == false) {
            // Clamp the progress so that it reaches 100% instead of 90%
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            float progressScaled = progress*100.0f;
            progressText.text = ((int) progressScaled) + "%";

            yield return null;
        }
    }
}
