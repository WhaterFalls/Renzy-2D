using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	private Rigidbody2D rigby;
	private Collider2D enemyCollider;
	private Collider2D playerCollider;

	// Use this for initialization
	void Start () {
		rigby = GetComponent<Rigidbody2D> ();
		enemyCollider = GetComponent<PolygonCollider2D> ();
		playerCollider = GameObject.FindGameObjectsWithTag ("Player") [0].GetComponent<PolygonCollider2D> ();
	}

	private void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Sword"){
			if (!enemyCollider.IsTouching (playerCollider)){
				GameControl.instance.PlayerScored ();
				rigby.velocity = Vector2.zero;
				//			anim.SetTrigger ("Die");
				this.transform.position = new Vector2 (-15f, -25f);
			}
		}
	}
}