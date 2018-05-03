using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {


	public int player_life1 = 6;
	public int player_life2 = 6;
    public string zone = "";
	public bool player1_on = false;
	public bool player2_on = false;
    public GameObject prefab1;
    public GameObject prefab2;
	public GameObject player1 = null;
	public GameObject player2 = null;
	public int spawned;
	public int update_spawned;
	public GameObject option_menu;

	public Vector3 target_position; 
	public GameObject[] background;
    public GameObject[] enemies;
    //public GameObject gameover_canvas;
    public int cState;
    private GameObject spawn_manager;
    public bool start = true;
    public GameObject typeA; //player like enemy
    public GameObject typeB; //creature like enemy
    public GameObject typeC; //shield enemy
	public GameObject typeD; //small octopus
    public GameObject boss; //boss unit
	public float score;
    public GameObject gameover_canvas;
    public GameObject win_canvas;

    enum Wave{
		Start,
		Mid,
		End
	};


	void Awake(){ //to start the game with 2 unit
		background = GameObject.FindGameObjectsWithTag("background");
        spawn_manager = GameObject.FindGameObjectWithTag("SpawnManager");
    }

	// Use this for initialization
	void Start () {
		
		spawn_manager = GameObject.FindGameObjectWithTag ("SpawnManager");
        cState = (int)Wave.Start;
        zone = "A";
	}
	
	// Update is called once per frame
	void Update(){

		
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        if (player1 != null)
        {
            if (player2 != null)
            {
                Physics2D.IgnoreCollision(player1.GetComponent<BoxCollider2D>(), player2.GetComponent<BoxCollider2D>());
            }
            player1_on = true;
        }
        else {
            player1_on = false;
        }
        if (player2 != null)
        {
            if (player1 != null)
            {
                Physics2D.IgnoreCollision(player2.GetComponent<BoxCollider2D>(), player1.GetComponent<BoxCollider2D>());
            }
            player2_on = true;
        }
        else {
            player2_on = false;
        }

        //for enemy waves
        
        switch (cState) {

		case 0:
			//18
            //spawn wave 1 - need to handle different unit type
			zone = "A";
			spawn_manager.GetComponent<SpawnManager> ().SpawnSystem (6, typeA);
			break;
		case 1:
			//spawn wave 2
			zone = "B";
			spawn_manager.GetComponent<SpawnManager>().SpawnSystem(8, typeB);
			break;
	    case 2:
            //spawn wave 3
            zone = "C";
            spawn_manager.GetComponent<SpawnManager>().SpawnSystem(6, typeC);
            break;
	    case 3:
            //spawn wave 4
            zone = "D"; 
            spawn_manager.GetComponent<SpawnManager>().SpawnSystem(6, typeA);
			spawn_manager.GetComponent<SpawnManager> ().SpawnSystem (6, typeC);
            break;
		case 4:
			//spawn wave 5
			zone = "E";
			spawn_manager.GetComponent<SpawnManager> ().SpawnSystem (6, typeA);
			spawn_manager.GetComponent<SpawnManager> ().SpawnSystem (12, typeB);
			break;
		case 5:
			//spawn wave 6
			zone = "F";
			spawn_manager.GetComponent<SpawnManager> ().SpawnSystem (6, typeA);
			spawn_manager.GetComponent<SpawnManager> ().SpawnSystem (6, typeD);
			break;
		case 6:
			//spawn wave 7
			zone = "G";
			spawn_manager.GetComponent<SpawnManager> ().SpawnSystem (5, typeD);
			break;
		case 7://BOSS
			zone = "H";
			spawn_manager.GetComponent<SpawnManager> ().SpawnSystem (1, boss);
			AudioSource bgmusic = GameObject.Find ("BackgroundAudio").GetComponents<AudioSource> () [2];
			if (!(bgmusic.isPlaying) && (gameover_canvas.activeSelf == false)) {
				GameObject.Find ("BackgroundAudio").GetComponents<AudioSource> () [0].Stop ();
				bgmusic.Play ();
			}
            break;

		default:
            //show win screen and let the continue start there
			if (!(win_canvas.activeSelf)) {
				Time.timeScale = 0;
				GameObject.Find ("BackgroundAudio").GetComponents<AudioSource> () [5].Play ();
				option_menu.SetActive (false);
				win_canvas.SetActive (true);
			}
			break;
		
		}

		EnemyPresenceHandler ();

	}



	void LateUpdate () {
		GameObject target = GameObject.FindGameObjectWithTag ("SpawnPlayer");
		//if player loses all life points then its gameover
		if(player1_on == false && player_life1 > 0 && player1 == null){
			
			Debug.Log ("Instantiate player 1");

			player1 = Instantiate(prefab1, target_position, Quaternion.identity) as GameObject;
			
			for (int i = 0; i < background.Length; i++) {
				Physics2D.IgnoreCollision (player1.GetComponent<BoxCollider2D> (), background[i].GetComponent<BoxCollider2D> ());
			}
			player1.GetComponent<Rigidbody2D> ().gravityScale = 0;
			player1_on = true;

			player_life1--;

		}
		if(player2_on == false && player_life2 > 0 && player2 == null){
			
			Debug.Log ("Instantiate player 2");

			player2 = Instantiate(prefab2, target_position, Quaternion.identity) as GameObject;
			
			for (int i = 0; i < background.Length; i++) {
				Physics2D.IgnoreCollision (player2.GetComponent<BoxCollider2D> (), background[i].GetComponent<BoxCollider2D> ());
			}
			player2.GetComponent<Rigidbody2D> ().gravityScale = 0;
			player2_on = true;

			player_life2--;

		}
			
		if(player_life1 <= 0 && player_life2 <= 0){
			player_life1 = 0;
			player_life2 = 0;
			GameOver ();
		}
	}


    void EnemyPresenceHandler() {
        if (start == true && spawn_manager.GetComponent<SpawnManager>().spawned < spawn_manager.GetComponent<SpawnManager>().spawn_number)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
        else
        {
            start = false;
        }

        if (!start)
        { //check for count
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }


        if (enemies.Length == 0 && !start && spawn_manager.GetComponent<SpawnManager>().end_spawning == true)
        {
            Time.timeScale = 0;
            //gameover_canvas.SetActive(true); - not implemented yet
        }
        else if (enemies.Length == 0)
        {
            start = true;
            spawn_manager.GetComponent<SpawnManager>().spawned = 0;
            cState += 1;
            spawn_manager.GetComponent<SpawnManager>().finish_spawning = false;
        }


    }



	void GameOver(){
		
		if (!player1_on && !player2_on && gameover_canvas.activeSelf == false) {
			Debug.Log ("Game Over");
			Time.timeScale = 0;
            gameover_canvas.SetActive(true);
			option_menu.SetActive (false);
			//prompt menu (Game Over!!)
			GameObject.Find("BackgroundAudio").GetComponents<AudioSource>()[0].Stop();
			GameObject.Find("BackgroundAudio").GetComponents<AudioSource>()[2].Stop();
			GameObject.Find("BackgroundAudio").GetComponents<AudioSource>()[1].Play();
		}
	}


   


}
