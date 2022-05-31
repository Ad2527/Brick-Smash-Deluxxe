using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartMenuScript : MonoBehaviour
{

    public GameObject TranistionManager;
    public Text playerText;
    void Start()
    {
        string name = PlayerPrefs.GetString("HIGHSCORENAME");
        playerText.text = name + "    " + PlayerPrefs.GetInt("HIGHSCORE");
    }
    public void QuitGame() {
        Application.Quit();
        Debug.Log("Game has been quit");
    }

    public void StartGame() {
        LevelLoaderScript levelload = TranistionManager.gameObject.GetComponent<LevelLoaderScript>();
        levelload.LoadNextLevel("SampleScene");

    }
}
