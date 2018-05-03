using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreChecker : MonoBehaviour {

    public int score;
    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;
    public Text text5;
    public bool pause;
    public bool GameOver;
    bool seeControls;
    public Player player;
    // Use this for initialization
    void Start () {
        score = 0;
        text2.text = "";
        text3.text = "";
        text5.text = "";
        GameObject playerChecker = GameObject.FindWithTag("Player");
        if (playerChecker != null)
        {
            player = playerChecker.GetComponent<Player>();
            text4.text = "Health: " + player.health;
        }
        else
        {
            Debug.Log("Cannot find Player Script");
        }
        GameOver = false;
        pause = false;
        seeControls = false;
    }
	
	// Update is called once per frame
	void Update () {
        text1.text = "Score: " + score;
        if (Input.GetKeyDown(KeyCode.Space))
            pause = !pause;

        if (pause)
        {
            Time.timeScale = 0;
            text2.text = "PAUSED";
            text3.text = "Press R to Restart\nPress C to see controls\nPress Q to return to title screen\nPress Esc to return to Desktop";
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("scene 1");
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene("Title", LoadSceneMode.Single);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
            if(Input.GetKeyDown(KeyCode.C))
            {
                seeControls = !seeControls;
                if (seeControls)
                    text5.text = "A / Left Arrow Key: Move Left\nD / Right Arrow Key: Move Right\nLeft Mouse Click: Shoot Left\nRight Mouse Click: Shoot Right\nSpacebar: Pause Game\nR: Restart Game";
                else
                    text5.text = "";
            }
        }
        else if (!pause && !GameOver)
        {
            Time.timeScale = 1;
            text2.text = "";
            text3.text = "";
            text5.text = "";
            seeControls = false;
        }
        else if (GameOver)
        {
            if (score >= 5000)
                text2.text = "YOU WON!!!!";
            else
                text2.text = "YOU LOSE!";
            text3.text = "Press R to Restart\nPress C to see controls\nPress Q to return to title screen\nPress Esc to return to Desktop";
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("scene 1");
            }
            if(Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene("Title", LoadSceneMode.Single);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
            if (Input.GetKeyDown(KeyCode.C))
            {
                seeControls = !seeControls;
                if (seeControls)
                    text5.text = "A / Left Arrow Key: Move Left\nD / Right Arrow Key: Move Right\nLeft Mouse Click: Shoot Left\nRight Mouse Click: Shoot Right\nSpacebar: Pause Game\nR: Restart Game";
                else
                    text5.text = "";
            }
        }
	}

    public void IncreaseScore(int newScore)
    {
        score += newScore;
    }

    public void DisplayHealth(int health)
    {
        text4.text = "Health: " + player.health;
    }
}
