using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GamePaused = false;

    public GameObject pauseMenuUI;
    public GameObject TranistionManager;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            
        }
    }
    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }



    public void QuitGame() {
        
        LevelLoaderScript levelload = TranistionManager.gameObject.GetComponent<LevelLoaderScript>();
        levelload.LoadNextLevel("Start Menu");
        Time.timeScale = 1f;

    }
}

