using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DinoGenerator : MonoBehaviour {

	public int totalDinosaurs = 10;
	public GameObject dinoPrefab;
	protected List<GameObject> dinosOnScreen = new List<GameObject>();
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
			GameObject newDino = (GameObject)Instantiate(dinoPrefab, newDinoVector, Quaternion.identity);
			chooseDino.ChooseRandomDino(newDino);
			newDino.GetComponent<Dino>().Create();
			dinosOnScreen.Add(newDino);
		}
	}
}
