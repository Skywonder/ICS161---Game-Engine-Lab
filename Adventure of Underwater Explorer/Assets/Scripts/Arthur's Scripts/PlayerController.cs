using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


	// Declare variables
	public float m_Speed = 5.0f;
	public int m_Switch = 0;
	public Color color1;
	public Color color2;
	// Get Custom Inputs from Input Manager
	public string m_Input_Horizontal;
	public string m_Input_Vertical;
	public string m_Input_Switch;
    public float wait;
    public GameObject s_dir_indicator;

    // Declare Rigidbody
    private Rigidbody2D m_Rigidbody;
    private BoxCollider2D m_getCollider;

    void Awake()
    {
		color1 = GetComponent<SpriteRenderer> ().color;
		color2 = GetComponent<SpriteRenderer> ().color;
		color1.a = 0.5f;
		color2.a = 1.0f;
        if (this.transform.tag == "Player1")
        {
            s_dir_indicator = GameObject.FindGameObjectWithTag("P1F");
        }
        if (this.transform.tag == "Player2")
        {
            s_dir_indicator = GameObject.FindGameObjectWithTag("P2F");
        }
    }

    void Start () {
		m_Rigidbody = GetComponent<Rigidbody2D> ();
        m_getCollider = GetComponent<BoxCollider2D>();
		GetComponent<SpriteRenderer> ().color = color1;
        wait = Time.time;
	}

	void SwitchBulletAngle() {
		if (m_Switch >= 2) {
            m_Switch = 0;
		}
        else {
			m_Switch = m_Switch + 1;
            s_dir_indicator.transform.eulerAngles = new Vector3(0, 0, -90);
        }

        if (m_Switch == 0) {
            s_dir_indicator.transform.eulerAngles = new Vector3(0, 0, -90);
        }
        if (m_Switch == 1) {
            s_dir_indicator.transform.eulerAngles = new Vector3(0, 0, -125);
        }
        if (m_Switch == 2) {
            s_dir_indicator.transform.eulerAngles = new Vector3(0, 0, -45);
        }
	}

	void Update() {

		//Switch Bullet's Angle
		if (Input.GetButtonDown (m_Input_Switch)) {
			SwitchBulletAngle ();
		}
        if (Time.time >= wait + 2f)
        {
            GetComponent<SpriteRenderer>().color = color2;
            m_getCollider.enabled = true;
        }

	}

	void FixedUpdate () {


		//Movement
		Vector2 move_vector = new Vector2 ();

		//Horizontal
		if (Input.GetAxis (m_Input_Horizontal) < -0.5f) {
			Debug.Log ("Player is moving left!");
			move_vector += new Vector2 (-1, 0);
		} 
		if (Input.GetAxis (m_Input_Horizontal) > 0.5f) {
			Debug.Log ("Player is moving right!");
			move_vector += new Vector2 (1, 0);
		}
		//Vertical
		if (Input.GetAxis (m_Input_Vertical) < -0.5f) {
			Debug.Log ("Player is moving left!");
			move_vector += new Vector2 (0, -1);
		} 
		if (Input.GetAxis (m_Input_Vertical) > 0.5f) {
			Debug.Log ("Player is moving right!");
			move_vector += new Vector2 (0, 1);
		}

		//Apply
		m_Rigidbody.MovePosition ((m_Rigidbody.position + move_vector * m_Speed * Time.deltaTime));
	}
}