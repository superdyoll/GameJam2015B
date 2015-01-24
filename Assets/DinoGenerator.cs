using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DinoGenerator : MonoBehaviour {

	public int totalDinosaurs = 10;
	protected List<Dino> dinosOnScreen = new List<Dino>();
	protected DinoSelector chooseDino = new DinoSelector();

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		if (dinosOnScreen.Count <= totalDinosaurs){
			System.Random rnd = new System.Random ();
			int x = rnd.Next (50);
			int y = rnd.Next (50);
			Vector3 newDinoVector = new Vector3(x,y);
			Dino newDino = chooseDino.ChooseRandomDino();
			Instantiate(newDino, newDinoVector, Quaternion.identity);
			dinosOnScreen.Add(newDino);
		}
	}
}
