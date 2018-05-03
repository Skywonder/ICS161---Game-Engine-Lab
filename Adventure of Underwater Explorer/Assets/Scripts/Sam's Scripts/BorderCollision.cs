using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderCollision : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.tag != "Player1" && collider.gameObject.tag != "Player2") {
            Physics2D.IgnoreCollision(collider.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}