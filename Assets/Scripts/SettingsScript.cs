using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsScript : MonoBehaviour
{
    //This pauses the game and shows the settings menu
    public void showSettings(){
        this.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    // This hides settings and resumes the game
    public void hideSettings(){
        this.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    // This allows the user to quit the game
    public void QuitGame(){
        Application.Quit();
    }

    // This allows the user to go back to the menu scene
    public void MainMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}
