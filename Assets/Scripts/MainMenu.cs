using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SettingsScript settingsScript;

    // This jumps to the game scene
    public void PlayGame() {
        PlayerPrefs.SetInt("LoadScene", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    // This exits the application
    public void QuitGame() {
        Application.Quit();
    }

    // This loads a saved game
    public void LoadGame(){
        PlayerPrefs.SetInt("LoadScene", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
