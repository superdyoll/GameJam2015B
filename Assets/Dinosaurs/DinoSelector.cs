using UnityEngine;
using System.Collections;

public class DinoSelector {
	public Dino ChooseRandomDino(GameObject currentDinosaur){
		Dino dinosaur = null;
		int num = Random.Range(0, 4);
		//Debug.Log (num + "");

		switch (num) {
		case 0:
			currentDinosaur.AddComponent<Diplodofortress>();
			dinosaur = currentDinosaur.GetComponent<Diplodofortress>();
			break;
		case 1:
			currentDinosaur.AddComponent<Triceratank>();
			dinosaur = currentDinosaur.GetComponent<Triceratank>();
			break;
		case 2:
			currentDinosaur.AddComponent<Assaultosaurus>();
			dinosaur = currentDinosaur.GetComponent<Assaultosaurus>();
			break;
		case 3:
			currentDinosaur.AddComponent<Horroraptor>();
			dinosaur = currentDinosaur.GetComponent<Horroraptor>();
			break;
		case 4:
			currentDinosaur.AddComponent<SatanasaurusRex>();
			dinosaur = currentDinosaur.GetComponent<SatanasaurusRex>();
			break;
		default:
			currentDinosaur.AddComponent<Terrordactyl>();
			dinosaur = currentDinosaur.GetComponent<Terrordactyl>();
			break;
		}

		return dinosaur;
	}
}
