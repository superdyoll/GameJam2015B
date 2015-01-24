using UnityEngine;
using System.Collections;

public class DinoAI : MonoBehaviour {
	private Dino dinoStats;
	private Vector3 playerPosition;
	private Player player;

	public void InsertBrain (Dino dinoStats) {
		this.dinoStats = dinoStats;
		player = GameObject.Find ("Player").GetComponent<Player> ();
	}

	void Update () {
		playerPosition = player.GetPosition ();
		Vector3 currentPosition = new Vector2 (transform.position.x, transform.position.y);
		float distanceToPlayer = Vector2.Distance (playerPosition, currentPosition);

		//if(distanceToPlayer > dinoStats.weapon.range){
			Vector3 directionToPlayer = playerPosition - currentPosition;
			directionToPlayer.Normalize ();
			transform.position += 0.1f * dinoStats.speed * directionToPlayer;
		//}
	}
}
