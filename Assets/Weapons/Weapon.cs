using System;
using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour {

	abstract protected int baseDamage { get; }
	abstract protected int baseRadius { get; }
	abstract protected int baseRange  { get; }

	public bool playerControlled{ get; set; }

	public int damage { get; set; }
	public int radius { get; set; }
	public int range { get; set; }
	public int pointsIn { get; set; }
	public int bounce { get; set; }

	public int getRange() {
		return range;
	}

	public void Create() {
		playerControlled = false;

		range =  baseRange  + pointsIn;
		radius = baseRadius + pointsIn;
		damage = baseDamage + pointsIn;

		Debug.Log ("Hurr: " + range);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
