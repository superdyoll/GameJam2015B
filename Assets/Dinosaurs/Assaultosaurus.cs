using UnityEngine;
using System.Collections;

public class Assaultosaurus : Dino {
	
	protected override int baseHealth        { get { return 4; } }
	protected override int baseSpeed         { get { return 2;  } }
	protected override int baseSurvivability { get { return 5;  } }
	protected override int baseExplosive     { get { return 6;  } }

	protected override Color colour { get { return new Color (0f, 1f, 1f); } }

}
