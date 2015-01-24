using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour{
	private int range, radius, bounce, speed;
	private Vector3 direction;
	private GameObject player;
	private Animation explosion;

	public void Go(int range, int radius, int bounce, int speed, Vector2 direction){
		this.range = range;
		this.radius = radius;
		this.bounce = bounce;
		this.speed = speed;
		this.direction = direction;
		explosion = gameObject.GetComponent<Animation> ();
		player = GameObject.Find ("Player");
	}

	void Update(){
		transform.position += direction * speed * 0.06f;

		float distanceToPlayer = Vector3.Distance (transform.position, player.transform.position); 

		if(distanceToPlayer > range){
			explosion.Play();

			if(!explosion.isPlaying){
				Destroy(this.gameObject);
			}
		}
	}
}
