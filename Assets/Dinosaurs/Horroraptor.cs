using UnityEngine;
using System.Collections;

public class Horroraptor : Dino {
	
	protected override int baseHealth        { get { return 2; } }
	protected override int baseSpeed         { get { return 5;  } }
	protected override int baseSurvivability { get { return 3;  } }
	protected override int baseExplosive     { get { return 1;  } }
	
	protected override Color colour { get { return new Color (1f,0f,1f); } }

}
