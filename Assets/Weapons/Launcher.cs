using UnityEngine;
using System.Collections;

public class Launcher : Weapon {

	protected override int baseDamage { get { return 10; } }
	protected override int baseRadius { get { return Screen.height / 10; } }
	protected override int baseRange  { get { 
			if (playerControlled) {
				pointsIn = 0;
			} else {
				pointsIn = 2; //Reliant upon current game level
			} 
			return Screen.height / 2 + (pointsIn * Screen.height/20);
		} }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
