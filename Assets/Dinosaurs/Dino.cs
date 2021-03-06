﻿using System;
using UnityEngine;
using System.Collections;

public abstract class Dino : MonoBehaviour {

	abstract protected int baseHealth        { get; }
	abstract protected int baseSpeed         { get; }
	abstract protected int baseSurvivability { get; }
	abstract protected int baseExplosive     { get; }

	protected int baseExp { get { return 17; } }

	abstract protected Color colour { get; }
	public Weapon weapon { get; set; }

	public int health { get; set; }
	public int speed;
	public int Speed { get { return speed; } set { speed = value; }}
	public int survivability { get; set; }
	public int explosive { get; set; }
	public int exp { get; set; }
	
	public bool playerControlled { get; set; }

	private Player player;

	double level { get; set; }

	public void Create() {
		player = GameObject.Find ("Player").GetComponent<Player>();
		GrabWeapon ();

		if (playerControlled) {
			health = (int) (baseHealth * level + 1);
			speed  = baseSpeed;
			survivability = baseSurvivability;
			explosive = baseExplosive;
			exp = 0;
		} else {
			AIInitialise ();
		}

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

	private void GrabWeapon(){
		int weaponInt = UnityEngine.Random.Range(0, 4);// for now, there's only one //rnd.Next (5);

		switch (weaponInt) {
		case 0:
			weapon = gameObject.AddComponent<Launcher> ();
			break;
		case 1:
			weapon = gameObject.AddComponent<Sniper>();
			break;
		case 2:
			weapon = gameObject.AddComponent<Shotgun>();
			break;
		case 3:
			weapon = gameObject.AddComponent<HandCannon>();
			break;
		default:
			weapon = gameObject.AddComponent<Launcher> ();
			break;
		}
		weapon.Create ();
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

		health = (int) (baseHealth * level * level);
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
			for(int i = 0; i < UnityEngine.Random.Range(0, 15); ++i)
			{
				Vector3 pos = new Vector3(UnityEngine.Random.Range(transform.position.x - 0.75f, transform.position.x + 0.75f), UnityEngine.Random.Range (transform.position.y - 0.75f, transform.position.y + 0.75f), 0f);
				float zRot = UnityEngine.Random.Range (0, 360);
				Vector3 eulerRot = new Vector3(0f, 0f, zRot);
				Quaternion quatRot = new Quaternion();
				quatRot.eulerAngles = eulerRot;
				GameObject temp = (GameObject)Instantiate (GameObject.Find ("Main Camera").GetComponent<DinoGenerator>().bloodSplat, pos, quatRot);
				float randScale = UnityEngine.Random.Range(0, 25) / 10f;
				temp.transform.localScale = new Vector3(randScale, randScale, 0f);
			}
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
