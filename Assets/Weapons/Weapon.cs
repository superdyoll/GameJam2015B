using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour {

	abstract protected int baseDamage { get; }
	abstract protected int baseRadius { get; }
	abstract protected int baseRange  { get; }

	public bool playerControlled{ get; set; }

	protected int damage { get; set; }
	protected int radius { get; set; }
	protected int range  { get; set; }
	protected int pointsIn { get; set; }

	// Use this for initialization
	void Start () {
		playerControlled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
