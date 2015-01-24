using UnityEngine;
using System.Collections;

public class Diplodofortress : Dino {

	protected override int baseHealth        { get { return 10; } }
	protected override int baseSpeed         { get { return 2;  } }
	protected override int baseSurvivability { get { return 6;  } }
	protected override int baseExplosive     { get { return 4;  } }

	protected override Color colour { get { return new Color (0f,0f,1f); } }

}
