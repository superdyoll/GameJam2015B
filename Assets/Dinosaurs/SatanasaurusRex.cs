using UnityEngine;
using System.Collections;

public class SatanasaurusRex : Dino {
	
	protected override int baseHealth        { get { return 6; } }
	protected override int baseSpeed         { get { return 3;  } }
	protected override int baseSurvivability { get { return 2;  } }
	protected override int baseExplosive     { get { return 10;  } }
	
	protected override Color colour { get { return new Color (1f,1f,0f); } }

}
