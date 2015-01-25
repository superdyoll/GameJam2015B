using UnityEngine;
using System.Collections;

public class Shotgun : Weapon {

	protected override int baseDamage { get { return 2; } }
	protected override int baseRadius { get { return Screen.height / 30; } }
	protected override int baseRange  { get { 
			if (playerControlled) {
				pointsIn = 0;
			} else {
				pointsIn = 2; //Reliant upon current game level
			} 
			return Screen.height /50;
		} }
	protected int baseBounce { get {return 0;}}
	protected override int baseProjectileCount { get { return Random.Range (2, 6); } }

	protected override int baseRof { get { return 4; } }
}