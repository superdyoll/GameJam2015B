using UnityEngine;
using System.Collections;

public class DinoAI : MonoBehaviour {
	private Dino dinoStats;
	private Vector3 playerPosition;
	private Player player;
	private bool ready = false;
	private Vector3 movingDirection;
	private float timer = 0.5f;
	private float delay = 0.5f;

	public void InsertBrain (Dino dinoStats) {
		this.dinoStats = dinoStats;
		player = GameObject.Find ("Player").GetComponent<Player> ();
		movingDirection = new Vector3 (Random.Range (-1.00f, 1.00f), Random.Range (-1.00f, 1.00f), 0f);
		ready = true;
	}

	void Update () {
		if(ready){
			playerPosition = player.GetPosition ();
			Vector3 currentPosition = new Vector2 (transform.position.x, transform.position.y);
			float distanceToPlayer = Vector2.Distance (playerPosition, currentPosition);

			if(distanceToPlayer > 5f){
				Vector3 directionToPlayer = playerPosition - currentPosition;
				directionToPlayer.Normalize ();
				float moveDirMod = -0.008f * distanceToPlayer + 1.4f;
				transform.position += 0.1f * dinoStats.speed * (directionToPlayer + (movingDirection * moveDirMod / 1.2f));
			}

			if(transform.position.y > 50f){
				transform.position = new Vector3(transform.position.x, 50f, 0f);
			}
			if(transform.position.y < 0f){
				transform.position = new Vector3(transform.position.x, 0f, 0f);			
			}
			if(transform.position.x > 50f){
				transform.position = new Vector3(50f, transform.position.y, 0f);			
			}
			if(transform.position.x < 0f){
				transform.position = new Vector3(0f, transform.position.y, 0f);			
			}

			if(distanceToPlayer < dinoStats.weapon.range){
				FireWeapon();
			}
		}
	}

	private void FireWeapon(){
		if (timer == 0f) {
			timer = Time.time;
			delay = ((float)Random.Range(20, 50)) / 100f;
			//Debug.Log("Delay:" + delay);

			for(int i = 0; i < dinoStats.weapon.numberOfProjectiles; ++i){
				Vector3 positionToTarget = (Vector3)player.GetPosition() - transform.position;
				positionToTarget = new Vector3(Random.Range (positionToTarget.x - 5, positionToTarget.x + 5), Random.Range (positionToTarget.y - 5, positionToTarget.y + 5), 0f);
				GameObject projInst = (GameObject)Instantiate (player.projectile, transform.position + positionToTarget.normalized, Quaternion.identity);
				Projectile projScript = projInst.GetComponent<Projectile> ();
				projScript.Go (dinoStats.getRange(), 10, 10, 4, dinoStats.weapon.damage / 3, positionToTarget.normalized, "Player", dinoStats);
				projInst.GetComponent<TrailRenderer>().material = player.sovietRed;
			}
		}
		if(timer + delay < Time.time)
		{
			timer = 0f;
		}
	}
}


