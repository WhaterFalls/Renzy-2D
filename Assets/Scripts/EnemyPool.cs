using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
	public int enemyPoolSize = 5;
	public GameObject EnemyPrefab;
	public float spawnRate = .5f;

	private GameObject[] enemies;
	private Vector2 objectPoolPosition = new Vector2(-15f, -25f);
	private float timeSinceLastSpawned;
	private int currentEnemy = 0;

	// Use this for initialization
	void Start () {
		enemies = new GameObject[enemyPoolSize];
		for (int i = 0; i < enemyPoolSize; i++) {
			enemies[i] = (GameObject) Instantiate(EnemyPrefab, objectPoolPosition, Quaternion.identity);
		}
	}

	// Update is called once per frame
	void Update () {
		float spawnXPosition = 10;
		float factor = 1;
		timeSinceLastSpawned += Time.deltaTime;
		if (!GameControl.instance.gameOver && timeSinceLastSpawned > spawnRate) {
			timeSinceLastSpawned = 0;
			var rand = Random.Range (0, 2);
			if (rand == 0)
				factor = -1;
			else if (rand == 1)
				factor = 1;
			enemies [currentEnemy].transform.position = new Vector2 (factor * spawnXPosition, -3.43f);
			CheckAndFlip (enemies [currentEnemy]);
			enemies [currentEnemy].GetComponent<Rigidbody2D> ().velocity = new Vector2 (-factor * 10, 0);
			currentEnemy++;
			if (currentEnemy % enemyPoolSize == 0) {
				currentEnemy = 0;
			}
		} else if (GameControl.instance.gameOver){
			for (int i = 0; i < enemyPoolSize; i++) {
				enemies [i].GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				enemies [i].GetComponent<Animator> ().SetTrigger ("Idle");
			}
		}
	}

	void CheckAndFlip(GameObject g) {
		if (g.transform.position.x > 0 & g.transform.localScale.x > 0)
			Flip (g);
		else if (g.transform.position.x < 0 & g.transform.localScale.x < 0)
			Flip (g);
	}

	void Flip(GameObject g){
		Vector3 theScale = g.transform.localScale;
		theScale.x *= -1;
		g.transform.localScale = theScale;
	}
}