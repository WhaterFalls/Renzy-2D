using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private bool isDead = false;
	private bool facingRight = true;
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (!isDead) {
			if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor) {
				if (Input.GetKeyDown (KeyCode.LeftArrow)) {
					if (facingRight)
						Flip ();
					anim.SetTrigger ("Swing");
				} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
					if (!facingRight)
						Flip ();
					anim.SetTrigger ("Swing");
				}
			} else if (Application.platform == RuntimePlatform.IPhonePlayer) {
				if (Input.touchCount > 0)
				{
					//Store the first touch detected.
					Touch myTouch = Input.touches[0];

					//Check if the phase of that touch equals Began
					if (myTouch.phase == TouchPhase.Began)
					{
						if (myTouch.position.x < (Screen.width/2)) {
							if (facingRight)
								Flip();
							anim.SetTrigger ("Swing");
						}
						else if (myTouch.position.x > (Screen.width/2)) {
							if (!facingRight)
								Flip();
							anim.SetTrigger ("Swing");
						}
					}
				}
			}
		}
	}

	// Flip the character
	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.GetComponent<Enemy> () != null) {
			isDead = true;
			anim.SetTrigger ("Die");
			GameControl.instance.PlayerDied ();
		}
	}
}