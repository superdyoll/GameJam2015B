using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour{
	private int range, radius, bounce, speed, damage;
	private Vector3 direction;
	private GameObject player;
	public Animation explosion;
	private string hostileTo;
	private DinoGenerator dinoGenerator; 

	public void Go(int range, int radius, int bounce, int speed, int damage, Vector2 direction, string hostileTo){
		this.range = range;
		this.radius = radius;
		this.bounce = bounce;
		this.speed = speed;
		this.damage = damage;
		this.direction = direction;
		this.hostileTo = hostileTo;
		explosion = transform.FindChild("ExplosionAnim").GetComponent<Animation> ();
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

		dinoGenerator = GameObject.Find ("Main Camera").GetComponent<DinoGenerator> ();
	}

	void Update(){
		transform.position += direction * speed * 0.06f;

		float distanceToPlayer = Vector3.Distance (transform.position, player.transform.position); 

		if(distanceToPlayer > range){
			//explosion.Play();

			//if(!explosion.isPlaying){
				Debug.Log ("Destroyed bullet after range");
				Destroy(this.gameObject);
			//}
		}
	}

	void OnCollisionEnter2D(Collision2D enemy){
		if (enemy.transform.tag == hostileTo) {
			enemy.gameObject.GetComponent<Dino>().Damage(damage);
			GameObject nearestEnemy = null;
			float distanceToNearestEnemy = 1000f;

			for(int i = 0; i < dinoGenerator.dinosOnScreen.Count; ++i)
			{
				float tempDist = Vector3.Distance(dinoGenerator.dinosOnScreen[i].transform.position, transform.position);
				if(tempDist < player.GetComponent<Player>().dinosaur.explosive){//radius){
					dinoGenerator.dinosOnScreen[i].gameObject.GetComponent<Dino>().Damage(1 * Level.getLevel());
				}

				if (tempDist < distanceToNearestEnemy) {
					tempDist = distanceToNearestEnemy;
					nearestEnemy = dinoGenerator.dinosOnScreen[i].gameObject;
				}
			}

			if (bounce > 0) {
				--bounce;
				GameObject projectile = (GameObject)Instantiate (player.GetComponent<Player>().projectile, transform.position, Quaternion.identity);
				Vector3 target = nearestEnemy.transform.position - transform.position;
				projectile.GetComponent<Projectile> ().Go (range, radius, bounce, speed, damage, target, hostileTo);
			}

			Destroy (gameObject);
			Debug.Log("Destroyed bullet");
		}
		else { //Must be YOU!!!
			player.GetComponent<Player>().Damage(damage);
		}
	}
}
