﻿using UnityEngine;
using System.Collections;

public class Triceratank : Dino {
	
	protected override int baseHealth        { get { return 8; } }
	protected override int baseSpeed         { get { return 4;  } }
	protected override int baseSurvivability { get { return 6;  } }
	protected override int baseExplosive     { get { return 4;  } }
	protected override int baseExp           { get { return 2;  } }

	protected override Color colour { get { return new Color (0.5f, 1, 1); } }
}
