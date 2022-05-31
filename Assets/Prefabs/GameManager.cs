using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //player stats
    public int lives;
    public int score;

    //variables link to the textboxes in the Canvas element within the game
    public Text livesText;
    public Text scoreText;
    public Text levelText;
    public Text highScoreText;
    public InputField highscoreInput;

    public bool gameOver;
    public bool levelChange;
    public GameObject gameOverPanel;
    public GameObject loadLevelPanel;
    public int numberOfBricks;

    public Transform[] levels;
    public int currentLevelIndex = 0;

    public GameObject TranistionManager;
    private AcheivementManager acheivementManager;
    // Start is called before the first frame update
    void Start()
    {
        acheivementManager = GameObject.FindObjectOfType<AcheivementManager>();
        livesText.text = "Lives: " + lives;

        scoreText.text = "Score: " + score;

        levelText.text = "Level: " + (currentLevelIndex + 1);

        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;

        //reset acheivements here for test
        //PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //change update the amount of lives based on an event that has just occured
   

    public void updateScore(int points)
    {
        score += points;
        if (score == 10)
        {

            StartCoroutine(UnlockAchievement(" First Brick!"));
        }
        if (score == 100)
        {
            
            StartCoroutine(UnlockAchievement(" Scored 100 points in a game!"));
        }
        scoreText.text = "Score: " + score;
    }

    public void UpdateNumerOfBricks()
    {
        numberOfBricks--;
        if (numberOfBricks <= 0)
        {
            //if level has been completed  and there are no more levels left
            if (currentLevelIndex >= levels.Length - 1)
            {
                GameOver();
                StartCoroutine(UnlockAchievement(" Beat The Game!!"));
            }
            else
            {
                levelChange = true;
                StartCoroutine(UnlockAchievement(" Completed a Level"));
                loadLevelPanel.SetActive(true);
                levelText.text = "Level: " + (currentLevelIndex + 2);
                if (currentLevelIndex == 1) {
                    StartCoroutine(UnlockAchievement("Final Level!!"));
                }
                //gameOver = true;              
                //load the next level in 3 seconds
                Invoke("LoadLevel", 3f);
                
            }
        }
    }

    void LoadLevel()
    {
        currentLevelIndex++;
        //create new level at it's current postion
        Instantiate(levels[currentLevelIndex], Vector2.zero, Quaternion.identity);
        //get number of bricks
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
        gameOver = false;
        loadLevelPanel.SetActive(false);
    }

    void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);

        //update highscore
        int highScore = PlayerPrefs.GetInt("HIGHSCORE");
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HIGHSCORE", score);

            highScoreText.text = "NEW HIGH SCORE!  " + "\n" + " ENTER NAME";
            highscoreInput.gameObject.SetActive(true);

            //highscore beat achievment
            if (highScore > 0)
            {
                //not the first highscore set
                StartCoroutine(UnlockAchievement(" Beat Highscore!"));
            }
        }
        else
        {
            highScoreText.text = "Score Aquired: " + score + "\n" + "The Highscore is " + highScore + " by: " + PlayerPrefs.GetString("HIGHSCORENAME");
        }

    }
    public void NewHighscore()
    {
        string highScoreName = highscoreInput.text;
        PlayerPrefs.SetString("HIGHSCORENAME", highScoreName);
        highscoreInput.gameObject.SetActive(false);
        highScoreText.text = "Well done " + highScoreName + "\n" + " Score: " + score;
    }


    public void PlayAgain()
    {
        LevelLoaderScript levelload = TranistionManager.gameObject.GetComponent<LevelLoaderScript>();
        levelload.LoadNextLevel("SampleScene");
    }

    public void Quit()
    {
        LevelLoaderScript levelload = TranistionManager.gameObject.GetComponent<LevelLoaderScript>();
        levelload.LoadNextLevel("Start Menu");
    }

    public void UpdateLives(int changeInLives)
    {
        //update with parsed amount of lives to change
        lives += changeInLives;

        //need to check if there's no lives left and inititate end of game
        if (lives <= 0)
        {
            //UNLOCK ACHIEVMENT DIE once
            StartCoroutine(UnlockAchievement("DIE!!"));
            //fix incorrect live display (if occured)
            lives = 0;
            //call the gameover function
            GameOver();
        }

        livesText.text = "Lives: " + lives;
    }

    public IEnumerator UnlockAchievement(string achID)
    {
        //has the user completed this or not
        string achCompleted = PlayerPrefs.GetString(achID);
        //whenever an achievment is called in the game the achievment player prefs are chcked first before the acheivmentManager is called
        if (achCompleted == "")
        {
            Debug.Log("DEBUG Achievment ID: " + achID);
            acheivementManager.NotifAchievmentComplete(achID);
            yield return new WaitForSeconds(Random.value * 6f);
            PlayerPrefs.SetString(achID, "COMPLETED");
        }
    }
    
}
