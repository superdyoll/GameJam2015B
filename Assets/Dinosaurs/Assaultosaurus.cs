using UnityEngine;
using System.Collections;

public class Assaultosaurus : Dino {
	
	protected override int baseHealth        { get { return 6; } }
	protected override int baseSpeed         { get { return 4;  } }
	protected override int baseSurvivability { get { return 4;  } }
	protected override int baseExplosive     { get { return 8;  } }
	protected override int baseExp           { get { return 2;  } }

	protected override Color colour { get { return new Color (1, 1, 0.5f); } }
}
