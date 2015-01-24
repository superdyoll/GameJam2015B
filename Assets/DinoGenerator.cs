using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DinoGenerator : MonoBehaviour {

	public int totalDinosaurs = 10;
	public GameObject dinoPrefab;
	public List<GameObject> dinosOnScreen = new List<GameObject>();
	protected DinoSelector chooseDino = new DinoSelector();
	public GameObject explosion;

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		if (dinosOnScreen.Count <= totalDinosaurs){
			int x = Random.Range(0, 50);
			int y = Random.Range(0, 50);
			Vector3 newDinoVector = new Vector3(x,y);
			GameObject newDino = (GameObject)Instantiate(dinoPrefab, newDinoVector, Quaternion.identity);
			chooseDino.ChooseRandomDino(newDino);
			newDino.GetComponent<Dino>().Create();
			dinosOnScreen.Add(newDino);
		}
	}

	public void RemoveDinosaur (GameObject dinoRemove){
		dinosOnScreen.Remove (dinoRemove);
	}
}
