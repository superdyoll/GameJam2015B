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

	public int health { get; set; }
	public int speed { get; set;}
	public int survivability { get; set; }
	public int explosive { get; set; }
	public int exp { get; set; }

	double level { get; set; }

	// Use this for initialization
	protected void Start () {
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
		
		health 		= (int)Math.Pow (baseHealth, level);
		speed 		= (int)Math.Pow (baseSpeed, level);
		survivability 	= (int)Math.Pow (baseSurvivability, level);
		explosive 	= (int)Math.Pow (baseExplosive, level);
		exp 		= (int)Math.Pow (baseExp, level);
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
