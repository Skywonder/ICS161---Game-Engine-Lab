using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UI_Control_Script : MonoBehaviour {

    public GameObject control_canvas = null;
    public GameObject pause_canvas = null;
    public GameObject gameover_canvas = null;
    public GameObject mini_map = null;
    public GameObject spawnmanager = null;
    public float cd;
    public float cdtimer;
    public bool active;
    public PlayerController player;
    public Text hp;
    public bool startscene = false;
    public bool controlscene = false;
    public GameObject[] panels;
    public GameObject[] swordimages;
    public Text describe = null;
    public Text wave;
    public GameObject swordcounter = null;
    public float swordcount;
    public float swordtotal;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        if (!startscene) {
            pause_canvas.SetActive(false);
            control_canvas.SetActive(false);
            gameover_canvas.SetActive(false);
            mini_map.SetActive(true);

            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            swordimages = GameObject.FindGameObjectsWithTag("SwordImage");
            swordtotal = player.swordcount;
        }
  	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyUp(KeyCode.Escape))// && cdtimer <= Time.time)
        {
           
            if (Time.timeScale == 1)
            {
                mini_map.SetActive(false);
                pause_canvas.SetActive(true);
                
                Time.timeScale = 0;
                active = true;
            }
            else{
                mini_map.SetActive(true);
                pause_canvas.SetActive(false);
                Time.timeScale = 1;
                active = false;
            }
        }

	}

    void LateUpdate() {
        if (!startscene)
        {
            hp.text = player.health.ToString() + "/ 100";
            if (player.health <= 0)
            {
                player.health = 0;
                Time.timeScale = 0;
                mini_map.SetActive(false);
                gameover_canvas.SetActive(true);
            }
            wave.text = spawnmanager.GetComponent<SpawnManagerScript>().wave;

            
            swordcount = player.swordsummon;

            if (swordcount == 0) {
                int i;
                for (i = 0; i < swordtotal; i++) {
                    swordimages[i].SetActive(false);
                }
            }
            if (swordcount == 1) {
                
                swordimages[0].SetActive(true);
                swordimages[1].SetActive(false);
                swordimages[2].SetActive(false);
            }
            if (swordcount == 2)
            {

                swordimages[0].SetActive(true);
                swordimages[1].SetActive(true);
                swordimages[2].SetActive(false);
            }
            if (swordcount == 3)
            {

                swordimages[0].SetActive(true);
                swordimages[1].SetActive(true);
                swordimages[2].SetActive(true);
            }
        }
    }


    public void RestartGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void QuitGame() {

        SceneManager.LoadScene("StartScene");
    }

    public void Introduction() {
        SceneManager.LoadScene("IntroductionScene");
    }

    public void OpenControls() {
        control_canvas.SetActive(true);
    }

    public void OpenControlScene() {
        SceneManager.LoadScene("ControlScene");
    }

    public void QuitControl() {
        control_canvas.SetActive(false);
    }

    public void StartScene() {
        SceneManager.LoadScene("MainGame");
    }

    public void EndScene() {
        SceneManager.LoadScene("EndScene");
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void nextPanel() {

        int i = 0;
        


		for (i = 0; i < panels.Length; i++) {
            if (panels[i].activeSelf == true)
            {
                
                if (i == panels.Length - 1)
                {
                    Debug.Log("Next Restart");
                    panels[i].SetActive(false);
					Debug.Log (i);

					describe.text = "Basic Movement Controls";         

                    panels[0].SetActive(true);
                    break;
                }
                else
                {
                    Debug.Log("Next in");
                    panels[i].SetActive(false);
                    panels[i+1].SetActive(true);
					Debug.Log (i);
					i++;
					if (i == 1)
					{
						describe.text = "Summon the Celestial Sword";
					}
					if (i == 2)
					{
						describe.text = "Use Sword";
					}
                    break;
                }
            }

        }



    }

}
