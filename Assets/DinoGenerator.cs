﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DinoGenerator : MonoBehaviour {

	private int totalDinosaurs = 10;
	public GameObject assaultosaurusPrefab;
	public GameObject diplodofortressPrefab;
	public GameObject horroraptorPrefab;
	public GameObject satanasaurusRexPrefab;
	public GameObject terrordactylPrefab;
	public GameObject triceratankPrefab;
	public List<GameObject> dinosOnScreen = new List<GameObject>();
	protected DinoSelector chooseDino = new DinoSelector();
	public GameObject explosion, bloodSplat;
	
	// Update is called once per frame
	void Update () {
		totalDinosaurs = Level.getLevel () * 4;
		if (dinosOnScreen.Count <= totalDinosaurs){
			int x = Random.Range(0, 50);
			int y = Random.Range(0, 50);
			Vector3 newDinoVector = new Vector3(x,y);
			int num = Random.Range(0, 6);
			GameObject newDino;

			switch (num) {
			case 0:
				newDino = (GameObject)Instantiate(diplodofortressPrefab, newDinoVector, Quaternion.identity);
				break;
			case 1:
				newDino = (GameObject)Instantiate(triceratankPrefab, newDinoVector, Quaternion.identity);
				break;
			case 2:
				newDino = (GameObject)Instantiate(terrordactylPrefab, newDinoVector, Quaternion.identity);
				break;
			case 3:
				newDino = (GameObject)Instantiate(horroraptorPrefab, newDinoVector, Quaternion.identity);
				break;
			case 4:
				newDino = (GameObject)Instantiate(satanasaurusRexPrefab, newDinoVector, Quaternion.identity);
				break;
			case 5:
				newDino = (GameObject)Instantiate(assaultosaurusPrefab, newDinoVector, Quaternion.identity);
				break;
			default:
				newDino = (GameObject)Instantiate(diplodofortressPrefab, newDinoVector, Quaternion.identity);
				break;
			}
			Bounds bounds = newDino.GetComponent<SpriteRenderer>().sprite.bounds;
			float xSize = 15f / bounds.size.x;
			float ySize = 10f / bounds.size.y;
			
			float scaleFl = Random.Range (80, 120) / 600f;
			Vector3 scale = new Vector3(scaleFl * xSize, scaleFl * ySize, 1);

			newDino.transform.localScale = scale;
			chooseDino.ChooseRandomDino(newDino, num);
			newDino.GetComponent<Dino>().Create();
			dinosOnScreen.Add(newDino);
		}
	}

	public void RemoveDinosaur (GameObject dinoRemove){
		Debug.Log ("eeeeeeerghghghgh");
		dinosOnScreen.Remove (dinoRemove);
	}

	public void ClearDinos () {
		foreach (GameObject dino in dinosOnScreen) {
			Destroy(dino);
			RemoveDinosaur (dino);
		}
	}
}
