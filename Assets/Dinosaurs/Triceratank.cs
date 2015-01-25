using UnityEngine;
using System.Collections;

public class Triceratank : Dino {
	
	protected override int baseHealth        { get { return 8; } }
	protected override int baseSpeed         { get { return 3;  } }
	protected override int baseSurvivability { get { return 6;  } }
	protected override int baseExplosive     { get { return 2;  } }

	protected override Color colour { get { return new Color (0f,1f,0f); } }

}
