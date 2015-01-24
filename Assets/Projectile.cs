using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour{
	private int range, radius, bounce, speed, damage;
	private Vector3 direction;
	private GameObject player;
	private Animation explosion;
	private string hostileTo;

	public void Go(int range, int radius, int bounce, int speed, int damage, Vector2 direction, string hostileTo){
		this.range = range;
		this.radius = radius;
		this.bounce = bounce;
		this.speed = speed;
		this.damage = damage;
		this.direction = direction;
		this.hostileTo = hostileTo;
		explosion = gameObject.GetComponent<Animation> ();
		player = GameObject.Find ("Player");

		Vector3 anchor = transform.position + (Vector3)direction;

		float rotationZ = Mathf.Acos ((transform.position.y - anchor.y) / Vector3.Distance (transform.position, anchor));
		rotationZ = rotationZ * Mathf.Rad2Deg - 90f;

		if(anchor.x < transform.position.x)
		{
			rotationZ = -rotationZ;
		}

		Vector3 vectRotation = new Vector3 (0f, 0f, rotationZ);
		Quaternion rot = new Quaternion ();
		rot.eulerAngles = vectRotation;
		transform.rotation = rot;
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

	void OnCollisionEnter2D(Collision2D enemy){
		if (enemy.transform.tag == hostileTo) {
			enemy.gameObject.GetComponent<Dino>().Damage(damage);
			Destroy (gameObject);
		}
	}
}
