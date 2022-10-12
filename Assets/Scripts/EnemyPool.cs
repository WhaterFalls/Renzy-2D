using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
	public int enemyPoolSize = 5;
	[SerializeField]
	GameObject[] EnemyPrefab = new GameObject[2];
	public float spawnRate = .5f;

	private GameObject[] enemies;
	private Vector2 objectPoolPosition = new Vector2(-15f, -25f);
	private float timeSinceLastSpawned;
	private int currentEnemy = 0;

	// Use this for initialization
	void Start () {
		enemies = new GameObject[enemyPoolSize];
		for (int i = 0; i < enemyPoolSize; i++) {
			int rand = Random.Range(0, 2);
			enemies[i] = (GameObject) Instantiate(EnemyPrefab[rand], objectPoolPosition, Quaternion.identity);
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

			// ABSTRACTION -- enemy pool only needs to spawn enemies, enemy class has public functions such as face the player and attack
			Enemy enemy = enemies[currentEnemy].GetComponent<Enemy>();
			enemy.FacePlayer();
			enemy.Attack();
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
}