using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoScript : MonoBehaviour
{
    public GameObject mainContent;
    public GameObject buildingsContent;
    public GameObject controlsContent;
    public GameObject shipsContent;
    public GameObject resourcesContent;

    //This pauses the game and shows the info screen
    public void showInfoScreen(){
        this.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    // This hides info and resumes the game
    public void hideInfoScreen(){
        this.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void showBuildingInfo(){
        mainContent.SetActive(false);
        buildingsContent.SetActive(true);
    }
    
    public void showControlsInfo(){
        mainContent.SetActive(false);
        controlsContent.SetActive(true);
    }

    public void showShipsInfo(){
        mainContent.SetActive(false);
        shipsContent.SetActive(true);
    }

    public void showResourcesInfo(){
        mainContent.SetActive(false);
        resourcesContent.SetActive(true);
    }

    public void showMainInfo(GameObject currentContent){
        currentContent.SetActive(false);
        mainContent.SetActive(true);
    }
}
