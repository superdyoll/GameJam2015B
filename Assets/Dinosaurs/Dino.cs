using System;
using UnityEngine;
using System.Collections;

public abstract class Dino : MonoBehaviour {

	abstract protected int baseHealth        { get; }
	abstract protected int baseSpeed         { get; }
	abstract protected int baseSurvivability { get; }
	abstract protected int baseExplosive     { get; }
	abstract protected int baseExp           { get; }

	abstract protected Color colour { get; }
	public Weapon weapon { get; set; }

	public int health { get; set; }
	public int speed;
	public int Speed { get { return speed; } set { speed = value; }}
	public int survivability { get; set; }
	public int explosive { get; set; }
	public int exp { get; set; }

	public bool playerControlled { get; set; }

	double level { get; set; }

	// Use this for initialization
	protected void Start () {

	}

	public void Create() {
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
		Debug.Log("Range: "+ weapon.getRange());
	}

	public int getRange() {
		if (weapon != null) {
			return weapon.getRange ();
		} else {
			return 0;
		}
	}

	private void AIInitialise () {
		double gameLevel = 1;
		
		renderer.material.color = colour;
		
		System.Random rnd = new System.Random ();
		int rndLevel = rnd.Next (-1, 1);
		int boss = rnd.Next (20);
		
		if (boss == 13) {
			level = gameLevel + 1.5;
			transform.localScale = transform.localScale * 2;
		} else {
			level = gameLevel + rndLevel;
		}
		
		Debug.Log ("Base:" + baseSpeed + " level:" + level);
		
		health 		  = (int)Math.Pow (baseHealth, level);
		speed 		  = (int)Math.Pow (baseSpeed, level);
		survivability = (int)Math.Pow (baseSurvivability, level);
		explosive 	  = (int)Math.Pow (baseExplosive, level);
		exp 		  = (int)Math.Pow (baseExp, level);
	}
	
	// Update is called once per frame
	protected void Update () {
	}


	public void Damage(int amount) {
		health -= amount;
		Debug.Log (health);
		if (health <= 0) {
			this.die();
		}
	}

	public void die() {
		Destroy (this.gameObject);

		//Add explosion stuff here
	}
}
