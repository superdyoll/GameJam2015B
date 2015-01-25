using System;
using UnityEngine;
using System.Collections;

public class Launcher : Weapon {

	protected override int baseDamage { get { return (int) Math.Pow(10, Level.getLevel()); } }
	protected override int baseRadius { get { return Screen.height / 10; } }
	protected override int baseRange  { get { 
			if (playerControlled) {
				pointsIn = 0;
			} else {
				pointsIn = 2; //Reliant upon current game level
			} 
			return Screen.height /50;
		} }
	protected override int baseProjectileCount {get {return 1;}}

	protected override int baseRof { get { return 6; } }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
