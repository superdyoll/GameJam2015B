using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour {

	abstract protected int baseDamage { get; }
	abstract protected int baseRadius { get; }
	abstract protected int baseRange  { get; }

	public bool playerControlled{ get; set; }

	public int damage { get; set; }
	public int radius { get; set; }
	public int range  { get; set; }
	public int pointsIn { get; set; }

	// Use this for initialization
	void Start () {
		playerControlled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
