using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
	protected Rigidbody2D rigby;
	protected Collider2D enemyCollider;
    protected Collider2D playerCollider;

	protected float speed;

	#region accessors
	// ENCAPSULATION -- only children class can set the speed
	public float Speed {
		get { return speed; }
		set { speed = value; }
	}
    #endregion

    protected void Start()
	{
		rigby = GetComponent<Rigidbody2D>();
		enemyCollider = GetComponent<PolygonCollider2D>();
		playerCollider = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PolygonCollider2D>();
	}

	protected void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Sword")
		{
			if (!enemyCollider.IsTouching(playerCollider))
			{
				GameControl.instance.PlayerScored();
				rigby.velocity = Vector2.zero;
				this.transform.position = new Vector2(-15f, -25f);
			}
		}
	}
}