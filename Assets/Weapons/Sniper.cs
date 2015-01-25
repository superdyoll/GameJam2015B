using UnityEngine;
using System.Collections;

public class Sniper : Weapon {
	
	protected override int baseDamage { get { return 10; } }
	protected override int baseRadius { get { return Screen.height / 10; } }
	protected override int baseRange  { get { 
			if (playerControlled) {
				pointsIn = 0;
			} else {
				pointsIn = 2; //Reliant upon current game level
			} 
			return Screen.height /50;
		} }
	protected int baseBounce { get {return 4;}}
	protected override int baseProjectileCount { get { return 1; } }

	protected override int baseRof { get { return 4;} }
}
