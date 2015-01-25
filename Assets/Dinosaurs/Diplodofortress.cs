using UnityEngine;
using System.Collections;

public class Diplodofortress : Dino {

	protected override int baseHealth        { get { return 8; } }
	protected override int baseSpeed         { get { return 1;  } }
	protected override int baseSurvivability { get { return 7;  } }
	protected override int baseExplosive     { get { return 7;  } }

	protected override Color colour { get { return new Color (0.12f,0.8f,0.9f); } }

}
