using System;
using UnityEngine;
using System.Collections;

public abstract class Dino : MonoBehaviour {

	abstract protected int baseHealth        { get; }
	abstract protected int baseSpeed         { get; }
	abstract protected int baseSurvivability { get; }
	abstract protected int baseExplosive     { get; }

	protected int baseExp { get { return 100; } }

	abstract protected Color colour { get; }
	public Weapon weapon { get; set; }

	public int health { get; set; }
	public int speed;
	public int Speed { get { return speed; } set { speed = value; }}
	public int survivability { get; set; }
	public int explosive { get; set; }
	public int exp { get; set; }
	public GameObject explosion;

	public bool playerControlled { get; set; }

	private Player player;

	double level { get; set; }

	public void Create() {
		player = GameObject.Find ("Player").GetComponent<Player>();

		if (playerControlled) {
			health = baseHealth;
			speed  = baseSpeed;
			survivability = baseSurvivability;
			explosive = baseExplosive;
			exp = 0;
		} else {
			AIInitialise ();
		}

		int weaponInt = 1;// for now, there's only one //rnd.Next (5);
		
		switch (weaponInt) {
		case 1: weapon = gameObject.AddComponent<Launcher>();
			break;
		default: weapon = gameObject.AddComponent<Launcher>();
			break;
		}
		weapon.Create ();
		//Debug.Log("Range: "+ weapon.getRange());

		/*Vector2 S = gameObject.GetComponent<SpriteRenderer> ().sprite.bounds.size;
		gameObject.GetComponent<BoxCollider2D> ().size = S;
		gameObject.GetComponent<BoxCollider2D> ().center = new Vector2 ((S.x / 2), 0);*/
	}

	public int getRange() {
		if (weapon != null) {
			return weapon.getRange ();
		} else {
			return 0;
		}
	}

	private void AIInitialise () {
		double gameLevel = Level.getLevel();
		
		renderer.material.color = colour;
		
		System.Random rnd = new System.Random ();
		int rndLevel = rnd.Next (-1, 1);
		int boss = rnd.Next (20);
		
		if (boss == 13) {
			level = gameLevel + 3;
			transform.localScale = transform.localScale * 2;
			exp = (int) (baseExp * gameLevel);
		} else {
			level = gameLevel + rndLevel;
			exp = (int) (baseExp *  level + 1);
			//Debug.Log("EXP Worth:" + exp);
		}

		int weaponInt = UnityEngine.Random.Range(0, 1);// for now, there's only one //rnd.Next (5);
		
		switch (weaponInt) {
		case 0:
			weapon = gameObject.AddComponent<Launcher> ();
			break;
		case 1:
			weapon = gameObject.AddComponent<Sniper>();
			break;
		default:
			weapon = gameObject.AddComponent<Launcher> ();
			break;
		}
		weapon.Create ();

		health = (int) (baseHealth * level);
		speed = (int) (baseSpeed * level);
		survivability = (int) (baseSurvivability * level);
		explosive = (int) (baseExplosive * level);

		gameObject.GetComponent<DinoAI> ().InsertBrain (this);

		//Debug.Log ("Health:" + health + " level:" + level + "EXP: " + exp);
	}


	public void Damage(int amount) {
		health -= amount;

		player.bloodScore += (exp / 10) * 666;

		if (health <= 0) {
			this.die();
		}
	}

	public void die() {
		player.bloodScore += exp * 666;
		DinoGenerator dinoThing = GameObject.Find ("Main Camera").GetComponent<DinoGenerator> ();
		dinoThing.RemoveDinosaur (this.gameObject);

		Vector3 pos = new Vector3 (transform.position.x, transform.position.y, 0f);
		Instantiate (dinoThing.explosion, pos, Quaternion.identity);

		Destroy (this.gameObject);

		//Add explosion stuff here
	}
}
