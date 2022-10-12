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

	// enemy can spawn on player's left or right side, need to make sure they face the player
	public void FacePlayer()
    {
		CheckAndFlip();
    }

	// move enemy towards player
	public void Attack()
    {
		float direction = 1;
		if (transform.position.x > 0)
        {
			direction = -1;
			
		}
		rigby.velocity = new Vector2(direction * speed, 0);

	}

	// Check if enemy is on left or right side and flip accordingly
	void CheckAndFlip()
	{
		if (transform.position.x > 0 & transform.localScale.x > 0)
			Flip();
		else if (transform.position.x < 0 & transform.localScale.x < 0)
			Flip();
	}

	void Flip()
	{
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
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