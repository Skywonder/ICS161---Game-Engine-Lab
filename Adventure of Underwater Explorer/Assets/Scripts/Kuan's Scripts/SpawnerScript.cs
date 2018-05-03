using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

    public GameObject prefab;
	public GameObject[] background;
	public Vector3 rotation_z;
    private float randomX;
    private float randomY;

    // Use this for initialization
    void Start () {
		background = GameObject.FindGameObjectsWithTag ("background");  
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Spawning(GameObject input_prefab)
    {
    
        randomX = Random.Range(-GetComponent<CircleCollider2D>().radius, GetComponent<CircleCollider2D>().radius);
        randomY = Random.Range(-GetComponent<CircleCollider2D>().radius, GetComponent<CircleCollider2D>().radius);
     
		GameObject enemy = Instantiate(input_prefab, transform.position + new Vector3(randomX, randomY, 0), Quaternion.identity) as GameObject;
		enemy.GetComponent<SpriteRenderer> ().sortingOrder = 5;

		

		Vector3 rotation = new Vector3 (0, 0, 0);
		enemy.transform.Rotate (rotation);

		for (int i = 0; i < background.Length; i++) {
			Physics2D.IgnoreCollision (enemy.GetComponent<BoxCollider2D> (), background[i].GetComponent<BoxCollider2D> ());
            if (enemy.name == "Boss(Clone)") {
                //Debug.Log("Found boss");
                Physics2D.IgnoreCollision(enemy.GetComponent<CompositeCollider2D>(), background[i].GetComponent<BoxCollider2D>());

            }

        }
        enemy.SetActive(true);
    }

   


}
