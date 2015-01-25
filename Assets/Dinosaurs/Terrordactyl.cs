using UnityEngine;
using System.Collections;

public class Terrordactyl : Dino {
	
	protected override int baseHealth        { get { return 4; } }
	protected override int baseSpeed         { get { return 4;  } }
	protected override int baseSurvivability { get { return 4;  } }
	protected override int baseExplosive     { get { return 6;  } }

	protected override Color colour { get { return new Color (1f,0f,0f); } }

}
